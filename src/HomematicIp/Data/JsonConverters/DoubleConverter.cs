using Newtonsoft.Json;
using System;

namespace HomematicIp.Data.JsonConverters
{
    public class DoubleConverter : JsonConverter<double>
    {
        public override void WriteJson(JsonWriter writer, double value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }

        public override double ReadJson(JsonReader reader, Type objectType, double existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            try
            {
                return double.Parse((string)reader.Value);
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#endif
#pragma warning disable CS0162 // Unreachable code detected
                return 0;
#pragma warning restore CS0162 // Unreachable code detected
            }
        }
    }
}