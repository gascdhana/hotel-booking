using HotelBooking.API.Models;
using HotelBooking.Data.Mongo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        #region Fields
        private readonly IBookingRepository bookingRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly IRoomRepository roomRepository;
        #endregion

        #region Constructor
        public BookingController(IBookingRepository bookingRepository, IHotelRepository hotelRepository, IRoomRepository roomRepository)
        {
            this.bookingRepository = bookingRepository;
            this.hotelRepository = hotelRepository;
            this.roomRepository = roomRepository;
        }
        #endregion

        #region Action
        [HttpGet("rooms"), Authorize(Roles = "User")]
        public async Task<IActionResult> GetRooms([FromQuery] string city, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            if (startDate == null || startDate == DateTime.MinValue) startDate = DateTime.Today;

            if (endDate == null || endDate == DateTime.MinValue || endDate > startDate)
                endDate = DateTime.Today.AddDays(1);

            //Get hotel from given location
            var hotels = await hotelRepository.GetHotelsCity(city);

            if (!hotels.Any())
                return NoContent();
            
            //Get rooms from given hotels
            var rooms = await roomRepository.GetRoomsByHotel(hotels.Select(x => x.Id).ToList());
            
            //Get current booking details from given datye
            var currentBookings = await bookingRepository.GetBookings(startDate.Value.Date, endDate.Value.Date);

            //Get avaialbe rooms 
            var availableRoomIds = rooms.Select(x => x.Id).Except(currentBookings.Select(x => x.RoomId).Distinct());

            if (!availableRoomIds.Any())
                return NoContent();

            var avaialbeRooms = from r in rooms join a in availableRoomIds on r.Id equals a select r;

            var result = avaialbeRooms.Select(x => new
            {
                HotelId = x.HotelId,
                HotelName = hotels.First(h => h.Id == x.HotelId).Name,
                Address = hotels.First(h => h.Id == x.HotelId).Address,
                RoomName = x.Name,
                x.Description,
                x.Capacity,
                x.BasePrice
            });

            return Ok(result);
        }

        [HttpGet("myBookings")]
        public async Task<IActionResult> GetMyBookings([FromQuery] bool isHistory = false)
        {
            var user = UserInfo.FromClaimsPrincipal(User);
            var myOpenBookings = await bookingRepository.MyOpenBookings(user.Email, isHistory);
            return Ok(myOpenBookings);
        }

        [HttpDelete("myBookings/{id}/cancel")]
        public async Task<IActionResult> CancelBooking([FromQuery] string id, [FromBody] string cancelReason = null)
        {
            var user = UserInfo.FromClaimsPrincipal(User);
            await bookingRepository.CancelBooking(id, cancelReason, user.Email);
            return NoContent();
        } 
        #endregion

    }
}
