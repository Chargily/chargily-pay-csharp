using System.Text.Json.Serialization;
using Chargily.Pay.V2.Models;
using FluentValidation;

namespace Chargily.Pay.V2.Internal.Requests;

internal sealed record CreatePriceRequest
{
    public decimal Amount { get; init; }
    public string Currency { get; init; } = null!;
    [JsonPropertyName("product_id")] public string ProductId { get; init; } = null!;
    public List<string>? Metadata { get; init; } = new();
}

internal class CreatePriceRequestValidator : AbstractValidator<CreatePriceRequest>
{
    public CreatePriceRequestValidator()
    {
        RuleFor(x => x.Amount)
           .GreaterThanOrEqualTo(0)
           .NotNull();

        RuleFor(x => x.Currency)
           .NotEmpty()
           .NotNull()
           .IsEnumName(typeof(Currency), caseSensitive: false)
           .WithMessage(x => $"'{x}' is not a valid 'ISO 4217 3-letter' currency code!");

        RuleFor(x => x.ProductId)
           .NotEmpty()
           .NotNull();
    }
}