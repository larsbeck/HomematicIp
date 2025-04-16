﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DoorCommandType
    {
        OPEN,
        STOP,
        CLOSE,
        PARTIAL_OPEN
    }
}
