namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(Enums.FunctionalChannelType.MAINS_FAILURE_CHANNEL)]
    public class MainsFailureChannel : Channel
    {
        public string GenericAlarmSignal { get; set; }
        public bool? PowerMainsFailure { get; set; }
    }
}