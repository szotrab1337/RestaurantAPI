using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entites;
using RestaurantAPI.Services;

namespace RestaurantAPI.Authorization
{
    public class CreatedMultipleRestaurantsRequirementHandler : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        private readonly RestaurantDbContext _dbContext;

        public CreatedMultipleRestaurantsRequirementHandler(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            CreatedMultipleRestaurantsRequirement requirement)
        {
            var value = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (value == null)
            {
                return Task.CompletedTask;
            }

            var userId = int.Parse(value);
            var quantityOfCreatedRestaurants = _dbContext.Restaurants
                .Count(x => x.CreatedById == userId);

            if (quantityOfCreatedRestaurants >= 2)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
