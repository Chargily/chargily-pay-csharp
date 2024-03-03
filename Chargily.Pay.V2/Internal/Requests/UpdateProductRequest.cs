using System.Text.Json.Serialization;
using FluentValidation;

namespace Chargily.Pay.V2.Internal.Requests;

internal sealed record UpdateProductRequest
{
    [JsonIgnore] public string Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    [JsonPropertyName("images")] public string[]? ImagesUrls { get; init; }
    public List<string>? Metadata { get; init; }
}

internal class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .NotNull();

        When(x => x.Name is not null,
             () => RuleFor(x => x.Name)
                .NotEmpty());

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