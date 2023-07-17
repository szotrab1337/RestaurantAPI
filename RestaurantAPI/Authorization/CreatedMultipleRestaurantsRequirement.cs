using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class CreatedMultipleRestaurantsRequirement : IAuthorizationRequirement
    {
        public int MinimumCreatedRestaurants { get; }

        public CreatedMultipleRestaurantsRequirement(int minimumCreatedRestaurants)
        {
            MinimumCreatedRestaurants = minimumCreatedRestaurants;
        }
    }
}
