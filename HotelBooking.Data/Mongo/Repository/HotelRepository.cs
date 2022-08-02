using HotelBooking.Data.Mongo.Base;
using HotelBooking.Data.Mongo.Constants;
using HotelBooking.Models.Configuration;
using HotelBooking.Models.Entity;
using MongoDB.Driver;

namespace HotelBooking.Data.Mongo.Repository
{
    internal class HotelRepository : EntityRepositoryBase<Hotel>, IHotelRepository
    {
        public HotelRepository(IMongoClient mongoClient, MongoSettings mongoSettings) : base(mongoClient, mongoSettings, Collections.Hotel)
        {
        }

        public async Task<string?> GetHotelByEmail(string email)
        {
            var filter = Builders<Hotel>.Filter.Where(x => x.Admins.Contains(email));
            var projection = Builders<Hotel>.Projection.Include(x => x.Id);
            var result =  await FindMany<Hotel>(filter, projection);

            return result.FirstOrDefault()?.Id;
        }
        
        public async Task<IEnumerable<Hotel>> GetHotelsById(IEnumerable<string> ids)
        {
            var filter = Builders<Hotel>.Filter.In(x => x.Id, ids);
            var projection = Builders<Hotel>.Projection
                .Include(x => x.Id)
                .Include(x => x.Name)
                .Include(x => x.Address);

            return await FindMany<Hotel>(filter, projection);
        }

        public async Task<IEnumerable<Hotel>> GetHotelsCity(string city)
        {
            var filter = Builders<Hotel>.Filter.Where(x=> x.City == city.ToLower());
            var projection = Builders<Hotel>.Projection
                .Include(x => x.Id)
                .Include(x => x.Name)
                .Include(x => x.City)
                .Include(x => x.Address);

            return await FindMany<Hotel>(filter, projection);
        }


    }
}
