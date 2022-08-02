using HotelBooking.Data.Mongo.Base;
using HotelBooking.Data.Mongo.Constants;
using HotelBooking.Models.Configuration;
using HotelBooking.Models.Entity;
using MongoDB.Driver;

namespace HotelBooking.Data.Mongo.Repository
{
    internal class RoomRepository : EntityRepositoryBase<Room>, IRoomManagementRepository, IRoomRepository
    {
        #region Fields
        public RoomRepository(IMongoClient mongoClient, MongoSettings mongoSettings) : base(mongoClient, mongoSettings, Collections.Room)
        {
        }
        #endregion

        #region Constructor
        public async Task<Room> AddRoom(string hotelId, Room room, string userEmailId)
        {
            room.HotelId = hotelId;
            room.CreatedBy = userEmailId;
            room.CreatedOn = DateTime.UtcNow;

            await Collection.InsertOneAsync(room);

            return room;
        }
        #endregion

        #region Implementation of IRoomRepository
        public async Task UpdateRoom(string hotelId, Room room, string userEmailId)
        {
            //Do NOT Use ! operator in query. It will be translated as $ne by the driver. $ne is slower than ==
            var filter = Builders<Room>.Filter.Where(x => x.HotelId == hotelId && x.Id == room.Id && x.IsDeleted == false);
            var update = Builders<Room>.Update
                .Set(x => x.Name, room.Name)
                .Set(x => x.Description, room.Description)
                .Set(x => x.BasePrice, room.BasePrice)
                .Set(x => x.Capacity, room.Capacity)
                .Set(x => x.UpdatedBy, userEmailId)
                .Set(x => x.UpdatedOn, DateTime.UtcNow);

            await Collection.UpdateOneAsync(filter, update);

        }

        public async Task RemoveRoom(string hotelId, string roomId, string userEmailId)
        {
            //Do NOT Use ! operator in query. It will be translated as $ne by the driver. $ne is slower than ==
            var filter = Builders<Room>.Filter.Where(x => x.HotelId == hotelId && x.Id == roomId && x.IsDeleted == false);
            var update = Builders<Room>.Update
                .Set(x => x.IsDeleted, true)
                .Set(x => x.UpdatedBy, userEmailId)
                .Set(x => x.UpdatedOn, DateTime.UtcNow);
            await Collection.UpdateOneAsync(filter, update);
        }

        public async Task<Room> GetRoom(string hotelId, string roomId)
        {
            //Do NOT Use ! operator in query. It will be translated as $ne by the driver. $ne is slower than ==
            var filter = Builders<Room>.Filter.Where(x => x.HotelId == hotelId && x.Id == roomId && x.IsDeleted == false);

            return await Collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Room>> GetRooms(string hotelId, int skip = 0, int take = 10)
        {
            //Do NOT Use ! operator in query. It will be translated as $ne by the driver. $ne is slower than ==
            var filter = Builders<Room>.Filter.Where(x => x.HotelId == hotelId && x.IsDeleted == false);
            var projection = Builders<Room>.Projection
                .Exclude(x => x.HotelId)
                .Exclude(x => x.IsDeleted)
                .Exclude(x => x.CreatedBy)
                .Exclude(x => x.UpdatedBy);
            var result = await FindMany<Room>(filter, projection, skip: skip, limit: take);
            return result;
        }

        public async Task<IEnumerable<Room>> GetRoomsByHotel(IEnumerable<string> hotelIds)
        {
            var filter = Builders<Room>.Filter.In(x => x.HotelId, hotelIds);
            filter &= Builders<Room>.Filter.Where(x => x.IsDeleted == false);

            var projection = Builders<Room>.Projection
               .Exclude(x => x.IsDeleted)
               .Exclude(x => x.CreatedOn)
               .Exclude(x => x.UpdatedOn)
               .Exclude(x => x.CreatedBy)
               .Exclude(x => x.UpdatedBy);

            return await FindMany<Room>(filter, projection);
        }
        #endregion

    }
}
