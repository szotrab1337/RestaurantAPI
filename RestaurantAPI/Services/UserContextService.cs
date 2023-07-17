using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal? User { get; }
        int? GetUserId { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
        public int? GetUserId
        {
            get
            {
                var value = User?.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                return value is null
                    ? null
                    : int.Parse(value);
            }
        }
    }
}
