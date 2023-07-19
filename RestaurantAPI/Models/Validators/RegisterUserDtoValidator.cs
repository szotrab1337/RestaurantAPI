using FluentValidation;
using RestaurantAPI.Entites;

namespace RestaurantAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        private readonly RestaurantDbContext _dbContext;

        public RegisterUserDtoValidator(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(z => z.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(x => x.Email == value);

                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken.");
                    }
                });
        }
    }
}
