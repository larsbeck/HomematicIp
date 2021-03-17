
using HomematicIp.Data.Enums;
using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
    [EnumMap(FunctionalChannelType.MULTI_MODE_INPUT_BLIND_CHANNEL)]
    public class MultiModeInputBlindChannel : Channel
    {
        public double? ShutterLevel { get; set; }
        public double? PreviousShutterLevel { get; set; }
        public double? SlatsLevel { get; set; }
        public double? PreviousSlatsLevel { get; set; }
        [JsonProperty(PropertyName = "processing")]
        public bool? IsProcessing { get; set; }
        [JsonProperty(PropertyName = "selfCalibrationInProgress")]
        public bool? IsSelfCalibrationInProgress { get; set; }
        public double? TopToBottomReferenceTime { get; set; }
        public double? BottomToTopReferenceTime { get; set; }
        public double? ChangeOverDelay { get; set; }
        [JsonProperty(PropertyName = "supportingSelfCalibration")]
        public bool? DoesSupporSelfCalibration { get; set; }
        [JsonProperty(PropertyName = "endpositionAutoDetectionEnabled")]
        public bool? IsEndpositionAutoDetectionEnabled { get; set; }
        [JsonProperty(PropertyName = "supportingEndPositionAutoDetection")]
        public bool? DoesSupportEndPositionAutoDetection { get; set; }
        public double? DelayCompensationValue { get; set; }
        [JsonProperty(PropertyName = "supportingDelayCompensation")]
        public bool? DoesSupportDelayCompensation { get; set; }
        public string ProfileMode { get; set; }
        //"userDesiredProfileMode": "AUTOMATIC",
        public string UserDesiredProfileMode { get; set; }
        public double? FavoritePrimaryShadingPosition { get; set; }
        public double? SlatsReferenceTime { get; set; }
        [JsonProperty(PropertyName = "blindModeActive")]
        public bool? IsBlindModeActive { get; set; }
        public double? FavoriteSecondaryShadingPosition { get; set; }

        public MultiModeInputMode? MultiModeInputMode { get; set; }
        public BinaryBehaviorType? BinaryBehaviorType { get; set; }

        //"supportedOptionalFeatures": {
        //	"IOptionalFeatureSlatsState": true
        //},
    }
}
