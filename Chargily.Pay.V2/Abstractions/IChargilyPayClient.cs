using Chargily.Pay.V2.Models;

namespace Chargily.Pay.V2.Abstractions;

public interface IChargilyPayClient : IDisposable
{
  IWebhookValidator WebhookValidator { get; }
  bool IsLiveMode { get; }
  IReadOnlyList<Wallet> Balance { get; }
  DateTimeOffset? BalanceRefreshedAt { get; }
  Task<IReadOnlyList<Wallet>?> GetBalance();
  Task<Response<Product>> AddProduct(CreateProduct product);
  Task<Response<Product>> UpdateProduct(UpdateCustomer update);
  Task<Response<Product>?> GetProduct(string id);
  Task<PagedResponse<Product>> GetProducts(int page = 1, int pageSize = 50);
  Task<List<Price>?> GetProductPrices(string productId);
  IAsyncEnumerable<Product> Products();
  Task<Response<Customer>> AddCustomer(CreateCustomer customer);
  Task<Response<Customer>> UpdateCustomer(UpdateCustomer update);
  Task<PagedResponse<Customer>> GetCustomers(int page = 1, int pageSize = 50);
  Task<Response<Customer>?> GetCustomer(string id);
  IAsyncEnumerable<Customer?> Customers();
  Task<Response<PaymentLinkResponse>> CreatePaymentLink(CreatePaymentLink paymentLink);
  Task<Response<PaymentLinkResponse>> UpdatePaymentLink(UpdatePaymentLink update);
  Task<PagedResponse<PaymentLinkResponse>> GetPaymentLinks(int page = 1, int pageSize = 50);
  Task<List<PaymentLinkItem>?> GetPaymentLinkItems(string paymentLinkId);
  IAsyncEnumerable<PaymentLinkResponse?> PaymentLinks();
  Task<Response<PaymentLinkResponse>?> GetPaymentLink(string id);
  Task<Response<CheckoutResponse>> CreateCheckout(Checkout checkout);
  Task<CheckoutResponse?> CancelCheckout(string checkoutId);
  Task<PagedResponse<CheckoutResponse>> GetCheckouts(int page = 1, int pageSize = 50);
  Task<List<CheckoutItem>?> GetCheckoutItems(string checkoutId);
  Task<Response<CheckoutResponse>?> GetCheckout(string id);
  IAsyncEnumerable<CheckoutResponse?> Checkouts();
  Task<Response<Price>> AddPrice(CreatePrice price);
  Task<Response<Price>> UpdatePrice(UpdatePrice update);
  Task<PagedResponse<Price>> GetPrices(int page = 1, int pageSize = 50);
  IAsyncEnumerable<Price?> Prices();
  Task<Response<Price>?> GetPrice(string id);
}