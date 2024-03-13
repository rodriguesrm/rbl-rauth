using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using RBlazeLabs.Architecture.Infra.Data.MongoDb.Options;

namespace RBlazeLabs.Architecture.Infra.Data.MongoDb.Abstractions
{

    /// <summary>
    /// Provides database connection for MongoDb
    /// </summary>
    /// <remarks>
    /// Create a new provider instance
    /// </remarks>
    /// <param name="connectionStringOptions">MongoDb connection string</param>
    public class MongoDatabaseProvider(IOptions<MongoDbConnectionStrings> connectionStringOptions)
    {

        #region Local Objects/Variables

        /// <summary>
        /// Connection string options
        /// </summary>
        protected MongoDbConnectionStrings _connectionStringOptions = 
            connectionStringOptions?.Value 
            ?? throw new ArgumentNullException(nameof(MongoDbConnectionStrings));

        #endregion
        
        #region Static methods

        static MongoDatabaseProvider()
        {
            ConventionPack convertionPack =
            [
                new CamelCaseElementNameConvention(),
                new StringObjectIdConvention(),
                new IgnoreExtraElementsConvention(true)
            ];

            ConventionRegistry.Register("conversions", convertionPack, type => true);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get database object
        /// </summary>
        public IMongoDatabase GetDatabase()
        {
            string databaseName = MongoUrl.Create(_connectionStringOptions.MongoDb).DatabaseName;
            MongoClient databaseClient = new(_connectionStringOptions.MongoDb);
            return databaseClient.GetDatabase(databaseName);
        }

        #endregion

    }

}
