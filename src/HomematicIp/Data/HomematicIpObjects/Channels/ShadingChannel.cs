using Newtonsoft.Json;

namespace HomematicIp.Data.HomematicIpObjects.Channels
{
	[EnumMap(Enums.FunctionalChannelType.SHADING_CHANNEL)]
	public class ShadingChannel : AbstractDeviceBaseChannel
	{
		public double? PrimaryShadingLevel { get; set; }
		public string PrimaryShadingStateType { get; set; }
		[JsonProperty(PropertyName = "processing")]
		public bool IsProcessing { get; set; }

		public double? SecondaryShadingLevel { get; set; }
		public double? PreviousSecondaryShadingLevel { get; set; }
		public string SecondaryShadingStateType { get; set; }

		public string ProfileMode { get; set; }
		public string UserDesiredProfileMode { get; set; }
		public string ShadingPackagePosition { get; set; }

		[JsonProperty(PropertyName = "primaryOpenAdjustable")]
		public bool? IsPrimaryOpenAdjustable { get; set; }
		[JsonProperty(PropertyName = "primaryCloseAdjustable")]
		public bool? IsPrimaryCloseAdjustable { get; set; }
		[JsonProperty(PropertyName = "secondaryOpenAdjustable")]
		public bool? IsSecondaryOpenAdjustable { get; set; }
		[JsonProperty(PropertyName = "secondaryCloseAdjustable")]
		public bool? IsSecondaryCloseAdjustable { get; set; }
		[JsonProperty(PropertyName = "shadingPositionAdjustmentActive")]
		public bool? IsShadingPositionAdjustmentActive { get; set; }

		public string ShadingPositionAdjustmentClientId { get; set; }

		public double? FavoritePrimaryShadingPosition { get; set; }
		public double? FavoriteSecondaryShadingPosition { get; set; }
		public string ShadingDriveVersion { get; set; }
		public string ManualDriveSpeed { get; set; }
		public string AutomationDriveSpeed { get; set; }

		//"primaryShadingLevel": 0.0,
		//"previousPrimaryShadingLevel": null,
		//"primaryShadingStateType": "POSITION_USED",
		//"processing": false,
		//"secondaryShadingLevel": null,
		//"previousSecondaryShadingLevel": null,
		//"secondaryShadingStateType": "NOT_EXISTENT",
		//"profileMode": "AUTOMATIC",
		//"userDesiredProfileMode": "AUTOMATIC",
		//"shadingPackagePosition": "TOP",
		//"primaryOpenAdjustable": true,
		//"primaryCloseAdjustable": true,
		//"secondaryOpenAdjustable": false,
		//"secondaryCloseAdjustable": false,
		//"shadingPositionAdjustmentActive": false,
		//"shadingPositionAdjustmentClientId": null,
		//"favoritePrimaryShadingPosition": 0.5,
		//"favoriteSecondaryShadingPosition": 0.5,
		//"productId": 10,
		//"identifyOemSupported": true,
		//"shadingDriveVersion": null,
		//"manualDriveSpeed": "NOMINAL_SPEED",
		//"automationDriveSpeed": "SLOW_SPEED"
	}
}
