using FluentValidation.Results;

namespace AdvertisingPlatforms.Domain.Shared;

public class ValidationErrorException : Exception
{
    public IReadOnlyCollection<string> Errors { get; set; } = [];
}
