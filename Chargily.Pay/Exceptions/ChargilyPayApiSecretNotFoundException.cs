﻿namespace Chargily.Pay.Exceptions;

public class ChargilyPayApiSecretNotFoundException : Exception
{
  public override string Message => "Chargily Pay V2 API Secret not found!";
}