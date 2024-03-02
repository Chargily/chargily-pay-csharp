namespace Chargily.Pay.V2.Exceptions;

public class ChargilyPayApiSecretNotFoundException : Exception
{
  public override string Message => "Chargily Pay V2 API Secret not found!";
}