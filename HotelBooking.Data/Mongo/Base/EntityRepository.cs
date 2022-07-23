using MongoDB.Bson;
using MongoDB.Driver;
using HotelBooking.Models.Entity.Base;

namespace HotelBooking.Data.Mongo.Base
{
    internal abstract class EntityRepositoryBase<T> : IEntityRepository<T> where T : Entity
    {
        #region Properties
        protected readonly IMongoCollection<T> Collection;
        #endregion

        #region Constructor
        protected EntityRepositoryBase(MongoClient mongoClient, string databaseName, string collectionName)
        {
            Collection = mongoClient.GetDatabase(databaseName).GetCollection<T>(collectionName);
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

        public async Task<T> Get(string id)
        {
            var query = new BsonDocument(new BsonElement("_id", BsonValue.Create(new ObjectId(id))));

            return await Collection.Find(query).SingleOrDefaultAsync();
        }

        public async Task<List<TProjection>> Get<TProjection>(FilterDefinition<T> filter, ProjectionDefinition<T, TProjection> projection, SortDefinition<T> sort,
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

        public async Task<T> Create(T entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task Delete(string id)
        {
            var query = new BsonDocument(new BsonElement("_id", BsonValue.Create(new ObjectId(id))));
            await Collection.DeleteOneAsync(query);
        }

        public async Task Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            await Collection.UpdateManyAsync(filter, update);
        } 
        #endregion
    }

}
