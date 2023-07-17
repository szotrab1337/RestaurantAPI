using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> _logger;

        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var rawDateOfBirth = context.User.FindFirst(c => c.Type == "DateOfBirth");

            if (rawDateOfBirth is null)
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = DateTime.Parse(rawDateOfBirth.Value);


            var rawUserEmail = context.User.FindFirst(x => x.Type == ClaimTypes.Name);

            if (rawUserEmail is null)
            {
                return Task.CompletedTask;
            }

            var userEmail = rawUserEmail.Value;

            _logger.LogInformation($"User: {userEmail} with date of birth: [{dateOfBirth}]");

            if (dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            {
                _logger.LogInformation("Authorization succedded");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Authorization failed.");
            }

            return Task.CompletedTask;
        }
    }
}
