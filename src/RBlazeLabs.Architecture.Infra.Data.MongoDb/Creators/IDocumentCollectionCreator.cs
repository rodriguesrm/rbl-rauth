namespace RBlazeLabs.Architecture.Infra.Data.MongoDb.Creators
{

    /// <summary>
    /// Document collection creator interface contract
    /// </summary>
    public interface IDocumentCollectionCreator
    {

        /// <summary>
        /// Perform a create collection
        /// </summary>

        Task CreateCollection();

    }

}
