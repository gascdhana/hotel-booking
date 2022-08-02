using MongoDB.Driver;
using HotelBooking.Models.Entity.Base;

namespace HotelBooking.Data.Mongo.Base
{
    public interface IEntityRepository<T> where T : Entity
    {
        Task<List<TProjection>> FindMany<TProjection>(FilterDefinition<T> filter, ProjectionDefinition<T, TProjection> projection, 
            SortDefinition<T>? sort = null,
            int? skip = null,
            int? limit = null);
    }
}
