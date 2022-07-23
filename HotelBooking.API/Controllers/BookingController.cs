using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            if (startDate == null || startDate == DateTime.MinValue) startDate = DateTime.Today;

            if (endDate == null || endDate == DateTime.MinValue || endDate > startDate) endDate = DateTime.Today.AddDays(1);

            return Ok();
        }

        [HttpGet("myBookings")] 
        public async Task<IActionResult> GetMyBookings()
        {
            return Ok();
        }

        [HttpDelete("myBookings/{id}/cancel")]
        public async Task<IActionResult> CancelBooking([FromQuery] string id)
        {
            return Ok();
        }

    }
}
