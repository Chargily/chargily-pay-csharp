using System.Text.Json;
using System.Text.Json.Serialization;

namespace Chargily.Pay.Internal.JsonConverters
{
    internal class ShippingAddressToStringConverter : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) return null;

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                string? state = null;
                string? address = null;
                string country = string.Empty;

                while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string propertyName = reader.GetString();
                        reader.Read();
                        switch (propertyName)
                        {
                            case "country":
                                country = reader.GetString();
                                break;
                            case "state":
                                state = reader.GetString();
                                break;
                            case "address":
                                address = reader.GetString();
                                break;
                            
                        }
                    }
                }
                string result = country;
                result += !string.IsNullOrEmpty(state) ? $", {state}" : string.Empty;
                result += !string.IsNullOrEmpty(address) ? $", {address}" : string.Empty;

                return result;
            }
            throw new JsonException("Expected a JSON object or null");
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
