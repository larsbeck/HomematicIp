using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace HomematicIp.Data
{
    internal static class EnumToType
    {
        private static Dictionary<object,Type> Map { get; } = Assembly.GetAssembly(typeof(EnumToType)).GetTypes().Where(type => type.GetCustomAttributes<EnumMapAttribute>().Any()).SelectMany(type => type.GetCustomAttribute<EnumMapAttribute>().Enums.Select(o => new Tuple<object,Type>(o,type))).ToDictionary(tuple => tuple.Item1,tuple => tuple.Item2);

        public static Type GetType(object o, string raw)
        {
            Map.TryGetValue(o, out var type);
            if (type == null)
            {
                return typeof(ExpandoObject);
                //throw new UnknownHomematicObjectException($"The HomematicIp Endpoint sent a message about an unknown HomematicIp Object (most likely a yet unsupported device). Please open an issue at https://github.com/larsbeck/HomematicIp to have this device added to the library. We will need the following: {raw}");
            }
            return type;
        }
    }
}