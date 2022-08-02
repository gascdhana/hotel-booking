using HotelBooking.Data.Mongo.Base;
using HotelBooking.Data.Mongo.Constants;
using HotelBooking.Models.Configuration;
using HotelBooking.Models.Entity;
using MongoDB.Driver;

namespace HotelBooking.Data.Mongo.Repository
{
    internal class BookingRepository : EntityRepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(IMongoClient mongoClient, MongoSettings mongoSettings) : base(mongoClient, mongoSettings, Collections.Booking)
        {

        }

        public async Task<List<Booking>> MyOpenBookings(string emailId, bool isHistory = false)
        {
            var filter = Builders<Booking>.Filter.Where(x=> x.BookingEmaiId == emailId && x.IsDeleted == false);

            if (isHistory)
                filter &= Builders<Booking>.Filter.Where(x => x.StartDate < DateTime.UtcNow.Date);
            else
                filter &= Builders<Booking>.Filter.Where(x => x.StartDate >= DateTime.UtcNow.Date);

            var projection = Builders<Booking>.Projection
                .Exclude(x => x.IsDeleted)
                .Exclude(x => x.CreatedBy)
                .Exclude(x => x.UpdatedBy);

            var result = await FindMany<Booking>(filter, projection);

            return result;
        }

        public async Task CancelBooking(string id, string reason, string emailId)
        {
            var filter = Builders<Booking>.Filter.Where(x => x.Id == id);

            var update = Builders<Booking>.Update
                .Set(x => x.CancelReason, reason)
                .Set(x => x.IsDeleted, true)
                .Set(x => x.UpdatedBy, emailId)
                .Set(x => x.UpdatedOn, DateTime.UtcNow);

            await Collection.UpdateOneAsync(filter, update);
        }

        public async Task<List<Booking>> GetBookings(DateTime startDate, DateTime endDate)
        {
            var filter = Builders<Booking>.Filter.Where(x => x.IsDeleted == false && x.StartDate <= endDate.Date && x.EndDate >= startDate.Date);
            var projection = Builders<Booking>.Projection
                .Include(x => x.HotelId)
                .Include(x => x.RoomId);

            var result = await FindMany<Booking>(filter, projection);

            return result;
        }

    }
}
