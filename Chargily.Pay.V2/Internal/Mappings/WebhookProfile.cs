using AutoMapper;
using Chargily.Pay.V2.Internal.Responses;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Mappings;

public class WebhookProfile : Profile
{
  public WebhookProfile()
  {
    var languages = new Dictionary<LocaleType, string>()
                    {
                      { LocaleType.Arabic, "ar" },
                      { LocaleType.English, "en" },
                      { LocaleType.French, "fr" },
                    };
    var locales = new Dictionary<string, LocaleType>()
                  {
                    { "ar", LocaleType.Arabic },
                    { "en", LocaleType.English },
                    { "fr", LocaleType.French },
                  };
    CreateMap<WebhookApiResponse, WebhookResponse>()
     .ConstructUsing((res, ctx) => new WebhookResponse()
                                   {
                                     Id = res.Id,
                                     CreatedAt = DateTimeOffset.FromUnixTimeSeconds(res.CreatedAt),
                                     LastUpdatedAt = DateTimeOffset.FromUnixTimeSeconds(res.LastUpdatedAt),
                                     IsLiveMode = res.IsLiveMode,
                                     Data = new WebhookCheckoutResponse()
                                             {
                                               Id = res.Id,
                                               Metadata = res.Data.Metadata,
                                               Amount = res.Data.Amount,
                                               Currency = ctx.Mapper.Map<Currency>(res.Data.Currency),
                                               CheckoutUrl = new Uri(res.Data.CheckoutUrl),
                                               WebhookEndpointUrl = new Uri(res.Data.CheckoutUrl),
                                               OnFailureRedirectUrl = new Uri(res.Data.CheckoutUrl),
                                               OnSuccessRedirectUrl = new Uri(res.Data.CheckoutUrl),
                                               PaymentLinkId = res.Data.PaymentLinkId,
                                               CustomerId = res.Data.CustomerId,
                                               InvoiceId = res.Data.InvoiceId,
                                               Fees = res.Data.Fees,
                                               Status = Enum.Parse<CheckoutStatus>(res.Data.Status),
                                               Language = locales[res.Data.Language],
                                               Description = res.Data.Description,
                                               PaymentMethod = Enum.Parse<PaymentMethod>(res.Data.PaymentMethod),
                                               PassFeesToCustomer = res.Data.PassFeesToCustomer
                                             },
                                   });
  }
}