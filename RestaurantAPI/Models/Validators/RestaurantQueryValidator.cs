using FluentValidation;
using RestaurantAPI.Entites;

namespace RestaurantAPI.Models.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private readonly int[] _allowedPageSizes = { 5, 10, 15 };

        private readonly string[] _allowedSortByColumnNames =
            { nameof(Restaurant.Name), nameof(Restaurant.Description), nameof(Restaurant.Category) };

        public RestaurantQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .Custom((value, context) =>
                {
                    if (!_allowedPageSizes.Contains(value))
                    {
                        context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", _allowedPageSizes)}]");
                    }
                });

            RuleFor(x => x.SortBy)
                .Must(x => string.IsNullOrEmpty(x) || _allowedSortByColumnNames.Contains(x))
                .WithMessage($"Sort by is optional or must be in [{string.Join(",", _allowedSortByColumnNames)}]");

            RuleFor(x => x.SortDirection)
                .IsInEnum();
        }
    }
}
