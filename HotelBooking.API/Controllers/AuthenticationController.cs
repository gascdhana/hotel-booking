using HotelBooking.API.Constants;
using HotelBooking.Data.Mongo.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        #region Fields
        private readonly IHotelRepository hotelRepository;
        #endregion

        #region Constructor
        public AuthenticationController(IHotelRepository hotelRepository)
        {
            this.hotelRepository = hotelRepository;
        }
        #endregion

        #region Private Methods
        private void TryAddOrUpdateClaim(List<Claim> claims, Claim claim)
        {
            var existingClaim = claims.FirstOrDefault(c => c.Type == claim.Type);

            if (existingClaim != null)
                claims.Remove(existingClaim);

            claims.Add(claim);
        }
        #endregion

        #region Actions

        [HttpGet("login")]
        public async Task LogIn()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                //After successfull authentication, redirect to below action to complete login
                RedirectUri = "https://localhost:7179/api/Authentication/google-login"
            });
        }

        [HttpGet("google-login")]
        public async Task<IActionResult> GoogleLogin()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal?.Identities
               .FirstOrDefault()?.Claims?.ToList();

            if (claims == null)
                return BadRequest("Unable to complete login");


            //For admin's Add hotelId in claims
            var emailId = claims.First(x => x.Type == ClaimTypes.Email).Value;

            var hotelId = await hotelRepository.GetHotelByEmail(emailId);
            if (!string.IsNullOrEmpty(hotelId))
            {
                TryAddOrUpdateClaim(claims, new Claim(ClaimsIdentity.DefaultRoleClaimType, Roles.Admin));
                TryAddOrUpdateClaim(claims, new Claim(Claims.HotelId, hotelId));
            }
            else
                TryAddOrUpdateClaim(claims, new Claim(ClaimsIdentity.DefaultRoleClaimType, Roles.User));

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

            return Ok();
        }

        [HttpGet("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        } 
        #endregion
    }
}
