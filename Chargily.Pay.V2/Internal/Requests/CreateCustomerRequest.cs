using System.Text.RegularExpressions;
using Chargily.Pay.V2.Models;
using FluentValidation;

namespace Chargily.Pay.V2.Internal.Requests;

internal sealed record AddressRequest
{
    public string Country { get; init; }
    public string State { get; init; }
    public string Address { get; init; }
}

internal sealed record CreateCustomerRequest
{
    public string Name { get; init; } = null!;
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public AddressRequest? Address { get; init; }
    public List<string>? Metadata { get; init; } = new();
}

internal class AddressRequestValidator : AbstractValidator<AddressRequest>
{
    public AddressRequestValidator()
    {
        RuleFor(x => x.Address)
           .NotNull()
           .NotEmpty();

        RuleFor(x => x.State)
           .NotNull()
           .NotEmpty();

        RuleFor(x => x.Country)
           .NotEmpty()
           .NotNull()
           .Must((code) => CountryCode.GetCountry(code) is not null)
           .WithMessage(x => $"'{x}' is not a valid 'ISO 3166-1 alpha-2' country code!");
    }
}

internal class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    private static readonly Regex EmailRegex =
        new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled);

    private static readonly Regex PhoneRegex =
        new Regex(@"^(\+|\d)[1-9][0-9 \-\(\)\.]{7,16}$", RegexOptions.Compiled);

    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .NotNull();

        When(x => x.Email is not null,
             () => RuleFor(x => x.Email)
                  .NotEmpty()
                  .Matches(EmailRegex)
                  .WithMessage(x => $"'{x}' is not a valid email address!"));

        When(x => x.Phone is not null,
             () => RuleFor(x => x.Phone)
                  .NotEmpty()
                  .Matches(PhoneRegex)
                  .WithMessage(x => $"'{x}' is not a valid international/local phone!"));
        
        When(x => x.Address is not null,
             () => RuleFor(x => x.Address)
                .SetValidator(new AddressRequestValidator()!));
    }
}