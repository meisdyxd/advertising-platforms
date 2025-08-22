using AdvertisingPlatforms.Application.UseCases.UploadPlatforms;
using FluentValidation;

namespace AdvertisingPlatforms.Application.ValidationRules.CommandRules;

public class UploadPlatformsCommandValidator : AbstractValidator<UploadPlatformsCommand>
{
    public UploadPlatformsCommandValidator()
    {
        RuleFor(c => c.File.FileName)
            .NotEmpty()
            .WithMessage("Filename is empty");

        RuleFor(c => c.File.FileName)
            .Must(f => Path.GetExtension(f) == ".txt")
            .WithMessage("Extension must be txt");

        RuleFor(c => c.File.Stream)
            .Must(s => s.Length > 0)
            .WithMessage("File is empty");
    }
}
