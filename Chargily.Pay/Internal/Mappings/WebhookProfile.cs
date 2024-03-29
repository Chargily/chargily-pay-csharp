﻿using AutoMapper;
using Chargily.Pay.Internal.Responses;
using Chargily.Pay.Models;

namespace Chargily.Pay.Internal.Mappings;

public class WebhookProfile : Profile
{
  public WebhookProfile()
  {

    CreateMap<WebhookApiResponse, WebhookRequest>()
     .ConstructUsing((res, ctx) => new WebhookRequest()
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
                                               Language = Language.GetLocalType(res.Data.Language) ?? LocaleType.English,
                                               Description = res.Data.Description,
                                               PaymentMethod = Enum.Parse<PaymentMethod>(res.Data.PaymentMethod),
                                               PassFeesToCustomer = res.Data.PassFeesToCustomer
                                             },
                                   });
  }
}