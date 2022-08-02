using HotelBooking.Models.Entity;

namespace HotelBooking.Data.Mongo.Repository
{
    public interface IRoomManagementRepository
    {
        /// <summary>
        /// Insert new <see cref="Room"/> in MongoDB
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="room"></param>
        /// <param name="userEmailId"></param>
        /// <returns></returns>
        Task<Room> AddRoom(string hotelId, Room room, string userEmailId);
        
        /// <summary>
        /// Update Specified <see cref="Room"/>
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="room"></param>
        /// <param name="userEmailId"></param>
        /// <returns></returns>
        Task UpdateRoom(string hotelId, Room room, string userEmailId);
        
        /// <summary>
        /// Soft-delete specified <see cref="Room"/>
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomId"></param>
        /// <param name="userEmailId"></param>
        /// <returns></returns>
        Task RemoveRoom(string hotelId, string roomId, string userEmailId);
        
        /// <summary>
        /// Get <see cref="Room"/> with given Id
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task<Room> GetRoom(string hotelId, string roomId);

        /// <summary>
        /// Get all <see cref="Room"/> from given <see cref="Hotel"/> Id
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IEnumerable<Room>> GetRooms(string hotelId, int skip = 0, int take = 10);
    }
}
