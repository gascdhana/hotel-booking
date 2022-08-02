using HotelBooking.Models.Entity;

namespace HotelBooking.Data.Mongo.Repository
{
    public interface IHotelRepository
    {
        Task<string?> GetHotelByEmail(string email);

        Task<IEnumerable<Hotel>> GetHotelsById(IEnumerable<string> ids);
        Task<IEnumerable<Hotel>> GetHotelsCity(string city);
    }
}
