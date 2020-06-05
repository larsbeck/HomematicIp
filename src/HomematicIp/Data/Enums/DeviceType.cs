﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HomematicIp.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DeviceType
    {
        DEVICE,
        ALARM_SIREN_INDOOR,
        BRAND_DIMMER,
        BRAND_PUSH_BUTTON,
        BRAND_SHUTTER,
        BRAND_BLIND,
        BRAND_SWITCH_MEASURING,
        BRAND_SWITCH_NOTIFICATION_LIGHT,
        BRAND_WALL_MOUNTED_THERMOSTAT,
        FLOOR_TERMINAL_BLOCK_6,
        FLOOR_TERMINAL_BLOCK_12,
        FULL_FLUSH_BLIND,
        FULL_FLUSH_CONTACT_INTERFACE,
        FULL_FLUSH_DIMMER,
        FULL_FLUSH_SHUTTER,
        FULL_FLUSH_SWITCH_MEASURING,
        HEATING_THERMOSTAT,
        HEATING_THERMOSTAT_COMPACT,
        KEY_REMOTE_CONTROL_4,
        KEY_REMOTE_CONTROL_ALARM,
        LIGHT_SENSOR,
        MOTION_DETECTOR_INDOOR,
        MOTION_DETECTOR_OUTDOOR,
        MOTION_DETECTOR_PUSH_BUTTON,
        MULTI_IO_BOX,
        OPEN_COLLECTOR_8_MODULE,
        PASSAGE_DETECTOR,
        PLUGABLE_SWITCH,
        PLUGABLE_SWITCH_MEASURING,
        PLUGGABLE_DIMMER,
        PRESENCE_DETECTOR_INDOOR,
        PRINTED_CIRCUIT_BOARD_SWITCH_BATTERY,
        PUSH_BUTTON,
        PUSH_BUTTON_6,
        REMOTE_CONTROL_8,
        ROOM_CONTROL_DEVICE,
        ROTARY_HANDLE_SENSOR,
        SHUTTER_CONTACT,
        SHUTTER_CONTACT_INVISIBLE,
        SHUTTER_CONTACT_MAGNETIC,
        SMOKE_DETECTOR,
        TEMPERATURE_HUMIDITY_SENSOR,
        TEMPERATURE_HUMIDITY_SENSOR_DISPLAY,
        TEMPERATURE_HUMIDITY_SENSOR_OUTDOOR,
        WALL_MOUNTED_THERMOSTAT_PRO,
        WATER_SENSOR,
        WEATHER_SENSOR,
        WEATHER_SENSOR_PLUS,
        WEATHER_SENSOR_PRO,
        ACCELERATION_SENSOR,
        REMOTE_CONTROL_8_MODULE,
        SHUTTER_CONTACT_INTERFACE,
        FULL_FLUSH_CONTACT_INTERFACE_6,
        ALARM_SIREN_OUTDOOR,
        PLUGGABLE_MAINS_FAILURE_SURVEILLANCE,
        PRINTED_CIRCUIT_BOARD_SWITCH_2,
        FLOOR_TERMINAL_BLOCK_10,
        HOERMANN_DRIVES_MODULE,
        HEATING_SWITCH_2,
        TORMATIC_MODULE,
        WALL_MOUNTED_THERMOSTAT_BASIC_HUMIDITY,
        ROOM_CONTROL_DEVICE_ANALOG,
        SHUTTER_CONTACT_OPTICAL_PLUS,
        DIN_RAIL_SWITCH_4,
        FULL_FLUSH_INPUT_SWITCH,
        WIRED_SWITCH_8,
        WIRED_SWITCH_4,
        DIN_RAIL_BLIND_4,
        WALL_MOUNTED_REMOTE_CONTROL_ROTARY_BUTTON,
        WIRED_WALL_MOUNTED_THERMOSTAT,
        WIRED_FLOOR_TERMINAL_BLOCK_6, //Wired Fußbodenheizungsaktor – 6-fach (HmIPW-FAL230-C6),
        WIRED_BLIND_4, //Wired Jalousieaktor – 4-fach (HmIPW-DRBL4)
        WIRED_INPUT_32, // Wired Eingangsmodul – 32-fach (HmIPW-DRI32)
    }
}