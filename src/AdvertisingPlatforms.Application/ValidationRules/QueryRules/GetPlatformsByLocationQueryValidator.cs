using AdvertisingPlatforms.Application.Queries.GetPlatformsByLocation;
using FluentValidation;

namespace AdvertisingPlatforms.Application.ValidationRules.QueryRules;

public class GetPlatformsByLocationQueryValidator : AbstractValidator<GetPlatformsByLocationQuery>
{
    public GetPlatformsByLocationQueryValidator()
    {
        RuleFor(q => q.Location)
            .NotEmpty()
            .WithMessage("Location cannot be empty");
    }
}
