using RBlazeLabs.Architecture.Infra.Data.MongoDb.Creators;

namespace RBlazeLabs.Architecture.Infra.Data.MongoDb.Abstractions
{

    /// <summary>
    /// Criador de objetos de banco de dados MongoDb (Collections, Indexes, etc)
    /// </summary>
    /// <param name="criators">Creators object list</param>
    public class MongoCollectionCreator(IEnumerable<IDocumentCollectionCreator> criators) : IDatabaseCreator
    {

        #region Local Objects/Variables

        private readonly IEnumerable<IDocumentCollectionCreator> _creators = criators;

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public async Task CreateDatabase()
            => await Task.WhenAll(_creators.Select(creator => creator.CreateCollection()));

        #endregion

    }

}
