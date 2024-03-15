using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using FluentValidation;

namespace Chargily.Pay.Internal.Requests;

internal sealed record UpdateCustomerRequest
{
    [JsonIgnore] public string Id { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public AddressRequest? Address { get; init; }
    public List<string>? Metadata { get; init; }
}

internal class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    private static readonly Regex EmailRegex =
        new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled);

    private static readonly Regex PhoneRegex =
        new Regex(@"^(\+|\d)[1-9][0-9 \-\(\)\.]{7,16}$", RegexOptions.Compiled);

    public UpdateCustomerRequestValidator()
    {
        RuleFor(x => x.Id)
           .NotEmpty()
           .NotNull();

        When(x => x.Name is not null,
             () => RuleFor(x => x.Name)
                  .NotEmpty()
                  .NotNull());

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