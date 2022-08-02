using HotelBooking.API.Constants;
using System.Security.Claims;

namespace HotelBooking.API.Models
{
    public class UserInfo
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string HotelId { get; private set; }

        public bool IsAuthenticated { get; }

        public static UserInfo FromClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            if (!(claimsPrincipal?.Identity?.IsAuthenticated ?? false))
                return new UserInfo(false);

            return new UserInfo(true)
            {
                Email = claimsPrincipal.FindFirstValue(ClaimTypes.Email),
                Name = claimsPrincipal.FindFirstValue(ClaimTypes.Name),
                HotelId = claimsPrincipal.FindFirstValue(Claims.HotelId),
            };
        }

        private UserInfo(bool isAuthenticated) 
        { 
            IsAuthenticated = isAuthenticated;
        }
    }
}
