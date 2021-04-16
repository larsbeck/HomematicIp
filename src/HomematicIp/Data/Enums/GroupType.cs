﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GroupType
    {
        GROUP,
        EXTENDED_LINKED_SHUTTER,
        SHUTTER_WIND_PROTECTION_RULE,
        LOCK_OUT_PROTECTION_RULE,
        SMOKE_ALARM_DETECTION_RULE,
        OVER_HEAT_PROTECTION_RULE,
        SWITCHING_PROFILE,
        HEATING_COOLING_DEMAND,
        HEATING_COOLING_DEMAND_PUMP,
        HEATING_COOLING_DEMAND_BOILER,
        HEATING_DEHUMIDIFIER,
        HEATING_EXTERNAL_CLOCK,
        HEATING_FAILURE_ALERT_RULE_GROUP,
        HEATING,
        HOT_WATER,
        HUMIDITY_WARNING_RULE_GROUP,
        SECURITY_ZONE,
        INBOX,
        HEATING_CHANGEOVER,
        HEATING_TEMPERATURE_LIMITER,
        HEATING_HUMIDITY_LIMITER,
        ALARM_SWITCHING,
        LINKED_SWITCHING,
        EXTENDED_LINKED_SWITCHING,
        SWITCHING,
        SECURITY,
        ENVIRONMENT,
        SECURITY_BACKUP_ALARM_SWITCHING,
        META,
        SHUTTER_PROFILE,
        HEAT_DEMAND_RULE,
        HEAT_DEMAND_RULE_WITH_LEAD_ROOM,
        NO_HEAT_DEMAND_RULE,
        LINKED_RAIN_PROTECTION,
        ACCESS_CONTROL,
        AUTO_RELOCK_PROFILE,
        ACCESS_AUTHORIZATION_PROFILE,
        LOCK_PROFILE
    }
}