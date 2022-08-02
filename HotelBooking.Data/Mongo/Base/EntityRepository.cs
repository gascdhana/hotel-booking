using MongoDB.Bson;
using MongoDB.Driver;
using HotelBooking.Models.Entity.Base;
using HotelBooking.Models.Configuration;

namespace HotelBooking.Data.Mongo.Base
{
    internal abstract class EntityRepositoryBase<T> : IEntityRepository<T> where T : Entity
    {
        #region Properties
        protected readonly IMongoCollection<T> Collection;
        #endregion

        #region Constructor
        protected EntityRepositoryBase(IMongoClient mongoClient, MongoSettings mongoSettings, string collectionName)
        {
            Collection = mongoClient.GetDatabase(mongoSettings.Database).GetCollection<T>(collectionName);
        }

        #endregion

        #region Private Methods
        private async Task<List<TProjection>> FindManyCursor<TProjection>(FilterDefinition<T> filter, FindOptions<T, TProjection> options)
        {
            var tProjection = new List<TProjection>();
            using (var cursor = await Collection.FindAsync(filter, options))
            {
                while (await cursor.MoveNextAsync())
                {
                    if (cursor.Current?.Count() > 0)
                        tProjection.AddRange(cursor.Current);
                }
            }
            return tProjection;
        }
        #endregion

        #region Implementation of IEntityRepository
        public async Task<List<TProjection>> FindMany<TProjection>(FilterDefinition<T> filter, ProjectionDefinition<T, TProjection> projection, 
            SortDefinition<T>? sort = null,
            int? skip = null,
            int? limit = null)
        {
            var findOptions = new FindOptions<T, TProjection>
            {
                Projection = projection,
                Limit = limit,
                Skip = skip,
                Sort = sort
            };

            return await FindManyCursor(filter, findOptions);
        }

        #endregion
    }

}
