using Microsoft.AspNetCore.Mvc;
using HotelBooking.Models.Entity;

namespace HotelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        /// <summary>
        /// Returns all <see cref="Room"/> in a hotel
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> GetRooms([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return Ok();
        }

        /// <summary>
        /// Get details of a given roomId
        /// </summary>
        /// <param name="id">Unique identifier of a <see cref="Room"/></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom([FromRoute] string id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] Room room)
        {
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom([FromRoute] string id, [FromBody] Room room)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] string id)
        {
            return Ok();
        }

    }
}
