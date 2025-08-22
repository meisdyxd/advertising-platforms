using AdvertisingPlatforms.Application.Services;
using MediatR;

namespace AdvertisingPlatforms.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ValidatorService _validator;

    public ValidationBehavior(ValidatorService validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await _validator.ValidateOrThrowAsync(request);
        return await next();
    }
}
