using System.Security.Cryptography;
using System.Text;
using Chargily.Pay.Abstractions;
using Chargily.Pay.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Chargily.Pay.Internal;

public class ChargilyPayWebhookValidator(
  IOptions<ChargilyConfig> config,
  ILogger<ChargilyPayWebhookValidator> logger)
  : IWebhookValidator
{
  private readonly HMACSHA256 _hmac = new(Encoding.UTF8.GetBytes(config.Value.ApiSecretKey));

  public bool Validate(string signature, string body)
  {
    logger.LogInformation("Validating body with signature {@signature}...", signature);
    logger.LogDebug("Body:\n{@body}", body);
    var bodyBytes = Encoding.UTF8.GetBytes(body);
    var computed = _hmac.ComputeHash(bodyBytes);

    var computedHex = BitConverter.ToString(computed).Replace("-", "");
    var validationResult = signature.Equals(computedHex, StringComparison.OrdinalIgnoreCase);

    logger.LogInformation("Validation {@validationResult}", validationResult ? "Successful": "Failed");
    return validationResult;
  }

  public bool Validate(string signature, Stream body)
  {
    logger.LogInformation("Validating body with signature {@signature}...", signature);
    var stream = new MemoryStream();
    body.CopyTo(stream);
    var computed = _hmac.ComputeHash(stream.ToArray());
    var computedHex = BitConverter.ToString(computed).Replace("-", "");
    var validationResult = signature.Equals(computedHex, StringComparison.OrdinalIgnoreCase);

    logger.LogInformation("Validation {@validationResult}", validationResult ? "Successful": "Failed");
    return validationResult;
  }
}