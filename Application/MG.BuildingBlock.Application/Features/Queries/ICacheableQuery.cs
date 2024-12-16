namespace MG.BuildingBlock.Application.Features.Queries
{
    /// <summary>
    /// to mark queries that we want to cache.
    /// </summary>
    public interface ICacheableQuery
    {
        /// <summary>
        /// Gets or sets a value indicating whether for enable caching set it to true beafor sending the query to bus like below:
        /// var query=new GetSomeQuery()
        /// query.EnableCache = false;
        /// _inMemoryBus.SendQuery(query).
        /// </summary>
        bool EnableCache { get; set; }
    }
}
