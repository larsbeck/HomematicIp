using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomematicIp.Data.JsonConverters
{
    public class AbstractListConverter<T1, T2> : JsonConverter<List<T1>> where T2 : struct, Enum where T1 : IHaveARawJsonProperty
    {
        private readonly string _typeDefiningProperty;

        public AbstractListConverter(string typeDefiningProperty)
        {
            _typeDefiningProperty = typeDefiningProperty;
        }

        public override void WriteJson(JsonWriter writer, List<T1> value, JsonSerializer serializer)
        {
            writer.WriteValue(JsonConvert.SerializeObject(value));
        }

        public override List<T1> ReadJson(JsonReader reader, Type objectType, List<T1> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var list = new List<T1>();
            var jObject = JObject.Load(reader);
            foreach (var jToken in jObject.Values())
            {
                Enum.TryParse(jToken[_typeDefiningProperty].Value<string>(), out T2 typedEnum);
                var raw = jToken.ToString();
                try
                {
                    var type = EnumToType.GetType(typedEnum, raw);
                    if (JsonConvert.DeserializeObject(raw, type) is T1 typedItem)
                    {
                        typedItem.RawJson = raw;
                        list.Add(typedItem);
                    }
                }
                catch (Exception ex)
                {
                    if(!Services.HomematicService.UnsupportedTypes.ContainsKey(typedEnum.ToString()))
                        Services.HomematicService.UnsupportedTypes.Add(typedEnum.ToString(), raw);

                    System.Diagnostics.Debug.WriteLine($"Error parsing following type {typedEnum.ToString()}, json: {raw}");
                    throw ex;
                }
            }
            return list;
        }
    }
}