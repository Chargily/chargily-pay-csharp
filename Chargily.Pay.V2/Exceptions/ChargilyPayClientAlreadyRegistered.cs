namespace Chargily.Pay.V2.Exceptions;

public class ChargilyPayClientAlreadyRegistered(string? ApiKey = null) : Exception
{
  public override string Message => ApiKey is null
                                      ? "Chargily Pay V2 Client already registered!"
                                      : $"Chargily Pay V2 Client with Api Secret Key: '{ApiKey}' already registered!";
}