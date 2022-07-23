using MongoDB.Driver;
using HotelBooking.Models.Entity.Base;

namespace HotelBooking.Data.Mongo.Base
{
    public interface IEntityRepository<T> where T : Entity
    {
        Task<List<TProjection>> Get<TProjection>(FilterDefinition<T> filter, ProjectionDefinition<T, TProjection> projection, SortDefinition<T> sort,
            int? skip = null,
            int? limit = null);
        Task<T> Get(string id);
        Task<T> Create(T entity);
        Task Update(FilterDefinition<T> filter, UpdateDefinition<T> update);
        Task Delete(string id);
    }
}
