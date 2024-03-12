using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Chargily.Pay.V2.Internal.JsonConverters;

public class IntegerToBooleanConverter : JsonConverter<bool>
{
  public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var value = Encoding.UTF8.GetString(reader.ValueSpan);
    return value switch
           {
             "0" => false,
             _ => true
           };
  }

  public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
  {
    writer.WriteNumberValue(value ? 1 : 0);
  }
}