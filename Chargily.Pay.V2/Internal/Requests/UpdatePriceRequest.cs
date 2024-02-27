using System.Text.Json.Serialization;
using Chargily.Pay.V2.Models;
using FluentValidation;

namespace Chargily.Pay.V2.Internal.Requests;



internal sealed record UpdatePriceRequest
{
    [JsonIgnore] public string Id { get; init; }
    public decimal? Amount { get; init; }
    public string? Currency { get; init; }
    [JsonPropertyName("product_id")] public string? ProductId { get; init; }
    public List<object> Metadata { get; init; } = new();
}

internal class UpdatePriceRequestValidator : AbstractValidator<UpdatePriceRequest>
{
    public UpdatePriceRequestValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .NotNull();

        When(x => x.Amount is not null,
             () => RuleFor(x => x.Amount)
                  .GreaterThanOrEqualTo(0)
                  .NotNull());

        When(x => x.Currency is not null,
             () => RuleFor(x => x.Currency)
                  .NotEmpty()
                  .NotNull()
                  .IsEnumName(typeof(Currency), caseSensitive: false)
                  .WithMessage(x => $"'{x}' is not a valid 'ISO 4217 3-letter' currency code!"));
        When(x => x.ProductId is not null,
             () => RuleFor(x => x.ProductId)
                  .NotEmpty()
                  .NotNull());
    }
}