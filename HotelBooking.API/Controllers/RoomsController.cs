using Microsoft.AspNetCore.Mvc;
using HotelBooking.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using HotelBooking.API.Models;
using HotelBooking.Data.Mongo.Repository;
using HotelBooking.API.Constants;

namespace HotelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = Roles.Admin)]
    public class RoomsController : ControllerBase
    {
        #region Fields
        private readonly IRoomManagementRepository roomRepository;
        #endregion

        #region Constructor
        public RoomsController(IRoomManagementRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }
        #endregion

        #region Action

        /// <summary>
        /// Returns all <see cref="Room"/> in a hotel
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> GetRooms([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var user = UserInfo.FromClaimsPrincipal(User);
            var rooms = await roomRepository.GetRooms(user.HotelId, skip, take);
            return Ok(rooms);
        }

        /// <summary>
        /// Get details of a given roomId
        /// </summary>
        /// <param name="id">Unique identifier of a <see cref="Room"/></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom([FromRoute] string id)
        {
            var user = UserInfo.FromClaimsPrincipal(User);
            var room = await roomRepository.GetRoom(user.HotelId, id);
            return Ok(room);
        }

        /// <summary>
        /// Add new room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] Room room)
        {
            var user = UserInfo.FromClaimsPrincipal(User);
            var createdRoom = await roomRepository.AddRoom(user.HotelId, room, user.Email);
            return CreatedAtAction(nameof(GetRooms), createdRoom.Id);
        }

        /// <summary>
        /// Update existing room
        /// </summary>
        /// <param name="id"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom([FromRoute] string id, [FromBody] Room room)
        {
            var user = UserInfo.FromClaimsPrincipal(User);

            //Update roomId in the object
            room.Id = id;

            await roomRepository.UpdateRoom(user.HotelId, room, user.Email);

            return Ok();
        }

        /// <summary>
        /// Delete a room
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] string id)
        {
            var user = UserInfo.FromClaimsPrincipal(User);
            await roomRepository.RemoveRoom(user.HotelId, id, user.Email);
            return NoContent();
        } 
        #endregion

    }
}
