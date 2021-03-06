﻿using System;
using Newtonsoft.Json;

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
            return double.Parse((string) reader.Value);
        }
    }
}