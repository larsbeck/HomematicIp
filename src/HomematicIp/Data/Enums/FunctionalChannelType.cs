﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FunctionalChannelType
    {
        FUNCTIONAL_CHANNEL,
        ALARM_SIREN_CHANNEL,
        ANALOG_OUTPUT_CHANNEL,
        BLIND_CHANNEL,
        CHANGE_OVER_CHANNEL,
        CLIMATE_SENSOR_CHANNEL,
        DEHUMIDIFIER_DEMAND_CHANNEL,
        DEVICE_BASE,
        DEVICE_BASE_FLOOR_HEATING,
        DEVICE_GLOBAL_PUMP_CONTROL,
        DEVICE_INCORRECT_POSITIONED,
        DEVICE_OPERATIONLOCK,
        DEVICE_PERMANENT_FULL_RX,
        DEVICE_SABOTAGE,
        DIMMER_CHANNEL,
        FLOOR_TERMINAL_BLOCK_LOCAL_PUMP_CHANNEL,
        FLOOR_TERMINAL_BLOCK_CHANNEL,
        FLOOR_TERMINAL_BLOCK_MECHANIC_CHANNEL,
        GENERIC_INPUT_CHANNEL,
        HEAT_DEMAND_CHANNEL,
        HEATING_THERMOSTAT_CHANNEL,
        INTERNAL_SWITCH_CHANNEL,
        LIGHT_SENSOR_CHANNEL,
        MOTION_DETECTION_CHANNEL,
        MULTI_MODE_INPUT_CHANNEL,
        NOTIFICATION_LIGHT_CHANNEL,
        PASSAGE_DETECTOR_CHANNEL,
        PRESENCE_DETECTION_CHANNEL,
        ROTARY_HANDLE_CHANNEL,
        SHUTTER_CHANNEL,
        SHUTTER_CONTACT_CHANNEL,
        SINGLE_KEY_CHANNEL,
        SMOKE_DETECTOR_CHANNEL,
        SWITCH_CHANNEL,
        SWITCH_MEASURING_CHANNEL,
        WALL_MOUNTED_THERMOSTAT_PRO_CHANNEL,
        WALL_MOUNTED_THERMOSTAT_WITHOUT_DISPLAY_CHANNEL,
        WATER_SENSOR_CHANNEL,
        WEATHER_SENSOR_CHANNEL,
        WEATHER_SENSOR_PRO_CHANNEL,
        WEATHER_SENSOR_PLUS_CHANNEL,
        ACCELERATION_SENSOR_CHANNEL,
        CONTACT_INTERFACE_CHANNEL,
        MAINS_FAILURE_CHANNEL,
        DEVICE_RECHARGEABLE_WITH_SABOTAGE,
        DOOR_CHANNEL,
        ANALOG_ROOM_CONTROL_CHANNEL,
        MULTI_MODE_INPUT_SWITCH_CHANNEL,
        MULTI_MODE_INPUT_BLIND_CHANNEL,
        ROTARY_WHEEL_CHANNEL,
        WALL_MOUNTED_THERMOSTAT_BASIC_CHANNEL,
        MULTI_MODE_INPUT_DIMMER_CHANNEL,
        WALL_MOUNTED_THERMOSTAT_CHANNEL
    }
}