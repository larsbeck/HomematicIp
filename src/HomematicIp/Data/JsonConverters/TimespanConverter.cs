using Newtonsoft.Json;
using System;

namespace HomematicIp.Data.JsonConverters
{
    public class TimespanConverter : JsonConverter<TimeSpan>
    {
        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Ticks);
        }

        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return TimeSpan.FromTicks(-1);

            return TimeSpan.FromTicks((long)reader.Value);
        }
    }
}