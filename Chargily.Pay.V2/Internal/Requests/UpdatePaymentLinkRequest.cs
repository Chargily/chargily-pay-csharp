using System.Text.Json.Serialization;
using Chargily.Pay.V2.Models;
using FluentValidation;

namespace Chargily.Pay.V2.Internal.Requests;

internal record UpdatePaymentLinkRequest
{
  [JsonIgnore] public string Id { get; init; }
  public string? Name { get; init; }
  [JsonPropertyName("active")] public bool? IsActive { get; init; }

  [JsonPropertyName("after_completion_message")]
  public string? CompletionMessage { get; init; }

  [JsonPropertyName("locale")] public string? Language { get; init; }

  [JsonPropertyName("pass_fees_to_customer")]
  public bool? PassFeesToCustomer { get; init; }

  public List<string>? Metadata { get; init; } = new();
}

internal class UpdatePaymentLinkRequestValidator : AbstractValidator<UpdatePaymentLinkRequest>
{
  public UpdatePaymentLinkRequestValidator()
  {
    When(x => x.Name is not null,
         () => RuleFor(x => x.Name)
              .NotEmpty()
              .NotNull());

    When(x => x.Language is not null,
         () => RuleFor(x => x.Language)
              .NotEmpty()
              .NotNull()
              .Must(x => Language.GetLocalType(x!) is not null)
              .WithMessage("Must be one of the following values : 'ar','fr','en'"));
  }
}