using HotelBooking.Data.Mongo.Base;
using HotelBooking.Data.Mongo.Constants;
using HotelBooking.Models.Entity;
using MongoDB.Driver;

namespace HotelBooking.Data.Mongo.Repository
{
    internal class RoomRepository : EntityRepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(MongoClient mongoClient, string databaseName) : base(mongoClient, databaseName, Collections.Room)
        {
        }
    }
}
