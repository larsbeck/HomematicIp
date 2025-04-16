namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.PARTICULATE_MATTER_SENSOR_CHANNEL)]
    public class ParticulateMatterSensorChannel : AbstractDeviceBaseChannel
    {
        public double? ActualTemperature { get; set; }
        public double? Humidity { get; set; }
        public double? ParticulateMassConcentrationTen { get; set; }
        public double? ParticulateMassConcentrationTenAverage { get; set; }
        public double? ParticulateNumberConcentrationTen { get; set; }
        public double? ParticulateMassConcentrationTwoPointFive { get; set; }
        public double? ParticulateMassConcentrationTwoPointFiveAverage { get; set; }
        public double? ParticulateNumberConcentrationTwoPointFive { get; set; }
        public double? ParticulateMassConcentrationOne { get; set; }
        public double? ParticulateMassConcentrationOneAverage { get; set; }
        public double? ParticulateNumberConcentrationOne { get; set; }
        public double? ParticulateTypicalSize { get; set; }
        public double? AirQualityIndexTen { get; set; }
        public double? AirQualityIndexTwoPointFive { get; set; }
    }
}
