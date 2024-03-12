using AutoMapper;
using Chargily.Pay.V2.Internal.Requests;
using Chargily.Pay.V2.Internal.Responses;
using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Internal.Mappings;

public class CustomerProfile : Profile
{
  public CustomerProfile()
  {
    CreateMap<CustomerAddress, AddressRequest>()
     .ConstructUsing((req, ctx) => new AddressRequest()
                                   {
                                     Address = req.Address,
                                     Country = CountryCode.GetCountryCode(req.Country)!,
                                     State = req.State
                                   })
     .ReverseMap()
     .ConstructUsing((res, ctx) => new CustomerAddress()
                                   {
                                     Address = res.Address,
                                     Country = (Country)CountryCode.GetCountry(res.Country)!,
                                     State = res.State
                                   });

    CreateMap<CreateCustomer, CreateCustomerRequest>()
     .ConstructUsing((req, ctx) => new CreateCustomerRequest()
                                   {
                                     Metadata = req.Metadata,
                                     Name = req.Name,
                                     Address = ctx.Mapper.Map<AddressRequest>(req.Address),
                                     Email = req.Email,
                                     Phone = req.Phone
                                   });

    CreateMap<UpdateCustomer, UpdateCustomerRequest>()
     .ConstructUsing((req, ctx) => new UpdateCustomerRequest()
                                   {
                                     Id = req.Id,
                                     Metadata = req.Metadata,
                                     Name = req.Name,
                                     Address = ctx.Mapper.Map<AddressRequest>(req.Address),
                                     Email = req.Email,
                                     Phone = req.Phone
                                   });

    CreateMap<CustomerApiResponse, Response<Customer>>()
     .ConstructUsing((res, ctx) => new Response<Customer>()
                                   {
                                     Id = res.Id,
                                     CreatedAt = DateTimeOffset.FromUnixTimeSeconds(res.CreatedAt),
                                     LastUpdatedAt = DateTimeOffset.FromUnixTimeSeconds(res.LastUpdatedAt),
                                     IsLiveMode = res.IsLiveMode,
                                     Value = new Customer()
                                             {
                                               Id = res.Id,
                                               Metadata = res.Metadata,
                                               Name = res.Name,
                                               Address = ctx.Mapper.Map<CustomerAddress>(res.Address),
                                               Email = res.Email,
                                               Phone = res.Phone
                                             }
                                   });

    CreateMap<CustomerApiResponse, Customer>()
     .ConstructUsing((res, ctx) => new Customer()
                                   {
                                     Id = res.Id,
                                     Metadata = res.Metadata,
                                     Name = res.Name,
                                     Address = ctx.Mapper.Map<CustomerAddress>(res.Address),
                                     Email = res.Email,
                                     Phone = res.Phone
                                   });
    CreateMap<PagedApiResponse<CustomerApiResponse>, PagedResponse<Customer>>()
     .ConstructUsing((res, ctx) => new PagedResponse<Customer>()
                                   {
                                     Data = ctx.Mapper.Map<List<Customer>>(res.Data),
                                     CurrentPage = res.CurrentPage,
                                     FirstPage = res.FirstPageUrl.GetPageOrDefault(1),
                                     LastPage = res.GetTotalPages(),
                                     NextPage = res.NextPageUrl?.GetPage(),
                                     PreviousPage = res.PreviousPageUrl?.GetPage(),
                                     PageSize = res.PageSize,
                                     Total = res.Total
                                   });
    
  }
}