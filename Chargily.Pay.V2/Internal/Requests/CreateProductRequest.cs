using System.Text.Json.Serialization;
using FluentValidation;

namespace Chargily.Pay.V2.Internal.Requests;

internal sealed record CreateProductRequest
{
    public string Name { get; init; }
    public string? Description { get; init; }
    [JsonPropertyName("images")]
    public string[] ImagesUrls { get; init; }
    public List<string> Metadata { get; init; } = new();
}

internal class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .NotNull();

        When(x => x.Description is not null,
             () => RuleFor(x => x.Description)
                .NotEmpty());

        When(x => x.ImagesUrls is not { Length: 0 },
             () => RuleForEach(x => x.ImagesUrls)
                  .NotEmpty()
                  .NotNull()
                  .Must((uri) => Uri.TryCreate(uri, UriKind.Absolute, out _))
                  .WithMessage((uri) => $"'{uri}' is not a valid URL!"));
    }
}