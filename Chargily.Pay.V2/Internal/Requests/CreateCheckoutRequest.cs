using System.Text.Json.Serialization;
using Chargily.Pay.V2.Models;
using FluentValidation;

namespace Chargily.Pay.V2.Internal.Requests;

internal record CheckoutItemRequest
{
  public string Price { get; init; }
  public int Quantity { get; init; }
}

internal record CreateCheckoutRequest
{
  public CheckoutItemRequest[]? Items { get; internal set; } = null;
  public decimal? Amount { get; init; }
  public string? Currency { get; init; }
  [JsonPropertyName("customer_id")] public string? CustomerId { get; init; }
  [JsonPropertyName("payment_method")] public string PaymentMethod { get; init; }
  [JsonPropertyName("success_url")] public string OnSuccessRedirectUrl { get; init; }
  [JsonPropertyName("failure_url")] public string OnFailureRedirectUrl { get; init; }
  [JsonPropertyName("webhook_endpoint")] public string WebhookEndpointUrl { get; init; }
  public string Description { get; init; }
  [JsonPropertyName("locale")] public string Language { get; init; }

  [JsonPropertyName("pass_fees_to_customer")]
  public bool PassFeesToCustomer { get; init; }

  public List<string>? Metadata { get; init; } = new();
}

internal class CreateCheckoutRequestValidator : AbstractValidator<CreateCheckoutRequest>
{
  public CreateCheckoutRequestValidator()
  {
    When(model => model.Items is null or { Length: 0 },
         () =>
         {
           RuleFor(x => x.Amount)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(0);

           RuleFor(x => x.Currency)
            .NotEmpty()
            .NotNull()
            .IsEnumName(typeof(Currency), caseSensitive: false)
            .WithMessage(x => $"'{x}' is not a valid 'ISO 4217 3-letter' currency code!");
         })
     .Otherwise(() => RuleForEach(x => x.Items)
                 .ChildRules(item =>
                             {
                               item.RuleFor(x => x.Price)
                                   .NotEmpty()
                                   .NotNull()
                                   .Must(x => decimal.TryParse(x, out _))
                                   .WithMessage(x => $"'{x}' is not a valid price!");
                               item.RuleFor(x => x.Quantity)
                                   .NotNull()
                                   .GreaterThanOrEqualTo(1);
                             }));

    RuleFor(x => x.OnFailureRedirectUrl)
     .NotEmpty()
     .NotNull()
     .Must((uri) => Uri.TryCreate(uri, UriKind.Absolute, out _))
     .WithMessage((uri) => $"'{uri}' is not a valid URL!");

    RuleFor(x => x.OnSuccessRedirectUrl)
     .NotEmpty()
     .NotNull()
     .Must((uri) => Uri.TryCreate(uri, UriKind.Absolute, out _))
     .WithMessage((uri) => $"'{uri}' is not a valid URL!");

    RuleFor(x => x.WebhookEndpointUrl)
     .NotEmpty()
     .NotNull()
     .Must((uri) => Uri.TryCreate(uri, UriKind.Absolute, out _))
     .WithMessage((uri) => $"'{uri}' is not a valid URL!");

    RuleFor(x => x.Language)
     .NotEmpty()
     .NotNull()
     .Must(x => Language.GetLocalType(x) is not null)
     .WithMessage("Must be one of the following values : 'ar','fr','en'");

    RuleFor(x => x.PaymentMethod)
     .NotEmpty()
     .NotNull()
     .Must(x => Enum.TryParse<PaymentMethod>(x, out _))
     .WithMessage("Must be one of the following values : 'CIB','EDAHABIA'");
  }
}