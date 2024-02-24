using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using RBlazeLabs.Common.Contracts.Entities;
using RBlazeLabs.Common.Notifications;
using System.Linq.Expressions;

namespace RBlazeLabs.Architecture.Infra.Data
{

    /// <summary>
    /// Auth database context
    /// </summary>
    /// <remarks>
    /// Create a new dbcontext instance
    /// </remarks>
    /// <param name="options">Context options settings</param>
    public abstract class DbContextBase(DbContextOptions options) : DbContext(options)
    {

        #region Local objects/variables

        private const string createdOn = nameof(IAudit.CreatedOn);
        private const string changedOn = nameof(IAudit.ChangedOn);
        private const string createdBy = nameof(IAudit.CreatedBy);
        private const string changedBy = nameof(IAudit.ChangedBy);

        #endregion

        #region Overrides

        /// <summary>
        /// Configures the context template
        /// </summary>
        /// <param name="modelBuilder">Model builder object</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Tables configuration
            SetTableConfiguration(modelBuilder);

            // Applying context model settings based on interface implementation
            SetSoftDeletionConfiguration(modelBuilder);
            SetAuditConfiguration(modelBuilder);
            SetActiveConfiguration(modelBuilder);
            SetIgnoreConfiguration(modelBuilder);

            // Logical exclusion filter
            SoftDeletionFilter(modelBuilder);

            // Entities to ignore
            modelBuilder.Ignore<Notification>();

            base.OnModelCreating(modelBuilder);

        }

        /// <summary>
        /// Saves all changes made in that context to the database.
        /// </summary>
        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        /// <summary>
        /// Saves all changes made in that context to the database.
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region Local methods

        /// <summary>
        /// Set tables (entities) configurations
        /// </summary>
        /// <param name="modelBuilder">Model builder object</param>
        protected abstract void SetTableConfiguration(ModelBuilder modelBuilder);

        /// <summary>
        /// Convert filter expression to lambda expression
        /// </summary>
        /// <typeparam name="TInterface">Interface type</typeparam>
        /// <param name="filterExpression">Filter expression</param>
        /// <param name="entityType">Entity type</param>
        private static LambdaExpression ConvertFilterExpression<TInterface>(Expression<Func<TInterface, bool>> filterExpression, Type entityType)
        {
            ParameterExpression param = Expression.Parameter(entityType);
            Expression request = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), param, filterExpression.Body);
            return Expression.Lambda(request, param);
        }

        /// <summary>
        /// Filters records with logical exclusion
        /// </summary>
        private static void SoftDeletionFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes()
                .Where(f => typeof(ISoftDeletion).IsAssignableFrom(f.ClrType))
                .ToList()
                .ForEach(e =>
                {
                    modelBuilder.Entity(e.ClrType)
                        .HasQueryFilter(ConvertFilterExpression<ISoftDeletion>(x => !x.IsDeleted, e.ClrType));
                });
        }

        /// <summary>
        /// Configure entity fields that implement ISoftDeletion
        /// </summary>
        /// <param name="modelBuilder">Model builder object</param>
        private static void SetSoftDeletionConfiguration(ModelBuilder modelBuilder)
        {

            modelBuilder.Model.GetEntityTypes()
           .Where(f => typeof(ISoftDeletion).IsAssignableFrom(f.ClrType))
           .ToList()
           .ForEach(e =>
           {
               modelBuilder.Entity(e.ClrType)
                   .Property<bool>(nameof(ISoftDeletion.IsDeleted))
                   .HasColumnType("bit")
                   .HasDefaultValue(false)
                   .IsRequired();
           });

        }

        /// <summary>
        /// Configure entity fields that implement IActive
        /// </summary>
        /// <param name="modelBuilder">Model builder object</param>
        private static void SetActiveConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes()
           .Where(f => typeof(IActive).IsAssignableFrom(f.ClrType))
           .ToList()
           .ForEach(e =>
           {
               modelBuilder.Entity(e.ClrType)
                   .Property<bool>(nameof(IActive.IsActive))
                   .HasColumnType("bit")
                   .IsRequired();
           });
        }

        /// <summary>
        /// Configure entity fields that implement IAudit
        /// </summary>
        /// <param name="modelBuilder">Model builder object</param>
        private static void SetAuditConfiguration(ModelBuilder modelBuilder)
        {

            modelBuilder.Model.GetEntityTypes()
            .Where(f => typeof(IAudit).IsAssignableFrom(f.ClrType) || typeof(ICreatedAuthor).IsAssignableFrom(f.ClrType))
            .ToList()
            .ForEach(e =>
            {
                modelBuilder.Entity(e.ClrType)
                    .Property<DateTime>(createdOn)
                    .IsRequired();

                modelBuilder.Entity(e.ClrType)
                    .Property<int>(createdBy)
                    .IsRequired();

                modelBuilder.Entity(e.ClrType)
                    .HasIndex(createdOn)
                    .HasDatabaseName($"IX_{e.ShortName()}_{createdOn}");

                modelBuilder.Entity(e.ClrType)
                    .HasIndex(createdBy)
                    .HasDatabaseName($"IX_{e.ShortName()}_{createdBy}");

                if (typeof(IAudit).IsAssignableFrom(e.ClrType))
                {

                    modelBuilder.Entity(e.ClrType)
                        .Property<int?>(changedBy);

                    modelBuilder.Entity(e.ClrType)
                        .Property<DateTime?>(changedOn);

                    modelBuilder.Entity(e.ClrType)
                        .HasIndex(changedOn)
                        .HasDatabaseName($"IX_{e.ShortName()}_{changedOn}");

                    modelBuilder.Entity(e.ClrType)
                        .HasIndex(changedBy)
                        .HasDatabaseName($"IX_{e.ShortName()}_{changedBy}");

                }

            });
        }

        /// <summary>
        /// Define the items (entities/fields) to be ignored when creating the model
        /// </summary>
        /// <param name="modelBuilder">Model builder object</param>
        private static void SetIgnoreConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes()
                .Where(f => f is Notifiable)
                .ToList()
                .ForEach(e =>
                {
                    modelBuilder.Entity(e.ClrType)
                        .Ignore(nameof(Notifiable.Notifications))
                        .Ignore(nameof(Notifiable.IsValid))
            ;
                });
        }

        /// <summary>
        /// Event launched before information in the database persists
        /// </summary>
        private void OnBeforeSaving()
        {

            List<EntityEntry> entities = ChangeTracker.Entries().ToList();
            DateTime now = DateTime.UtcNow;

            // Logical exclusion
            foreach (EntityEntry e in entities.Where(entry => entry.Entity is ISoftDeletion))
            {
                switch (e.State)
                {
                    case EntityState.Added:
                        e.Property(nameof(ISoftDeletion.IsDeleted)).CurrentValue = false;
                        break;

                    case EntityState.Deleted:
                        e.Property(nameof(ISoftDeletion.IsDeleted)).CurrentValue = true;
                        e.State = EntityState.Modified;
                        break;
                }
            }

            // Audit
            foreach (EntityEntry e in entities.Where(f => f.Entity is IAudit))
            {

                switch (e.State)
                {
                    case EntityState.Added:
                        e.Property(createdOn).CurrentValue = now;
                        e.Property(changedOn).CurrentValue = null;
                        break;

                    case EntityState.Modified:
                        e.Property(changedOn).CurrentValue = now;
                        e.State = EntityState.Modified;
                        break;
                }
            }

        }

        #endregion

    }

}
