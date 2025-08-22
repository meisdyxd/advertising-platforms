using Microsoft.Extensions.DependencyInjection;
using AdvertisingPlatforms.Domain.Shared;
using FluentValidation.Results;
using FluentValidation;

namespace AdvertisingPlatforms.Application.Services;

public class ValidatorService
{
    private readonly IServiceProvider _serviceProvider;

    public ValidatorService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ValidateOrThrowAsync(object entity)
    {
        var validator = GetValidator(entity);
        var validationResult = await ValidateInternalAsync(validator, entity);
        var isValid = GetValidationExceptions(validationResult, out var exceptions);
        if (!isValid)
            throw new ValidationErrorException
            {
                Errors = exceptions
            };
    }

    public async Task<ValidationResult> ValidateAsync(object entity)
    {
        var validator = GetValidator(entity);
        return await ValidateInternalAsync(validator, entity);
    }

    public IValidator GetValidator(object entity)
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(entity.GetType());
        var validators = _serviceProvider.GetServices(validatorType).ToArray();

        if (validators.Length == 0)
            throw new InvalidOperationException($"Unable to resolve " +
                                                $"validator for type '{entity.GetType().Name}' in DI");

        if (validators.Length > 1)
            throw new InvalidOperationException($"More than one validator found " +
                                                $"for type '{entity.GetType().Name}' in DI");

        return (IValidator)validators[0]!;
    }

    private static async Task<ValidationResult> ValidateInternalAsync(
        IValidator validator, 
        object entity)
    {
        var validationContext = new ValidationContext<object>(entity);
        var validationResult = await validator.ValidateAsync(validationContext);

        return validationResult;
    }

    private static bool GetValidationExceptions(
        ValidationResult validationResult, 
        out IReadOnlyCollection<string> exceptions)
    {
        exceptions = [.. validationResult.Errors.Select(e => e.ErrorMessage)];

        return validationResult.IsValid;
    }
}
