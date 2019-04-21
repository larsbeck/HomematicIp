using System;
using System.Collections.Generic;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects;
using HomematicIp.Data.HomematicIpObjects.Channels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomematicIp.Data.JsonConverters
{
    public class FunctionalChannelsConverter : AbstractListConverter<Channel, FunctionalChannelType>
    {
        public FunctionalChannelsConverter(string typeDefiningProperty= "functionalChannelType") : base(typeDefiningProperty)
        {
        }
    }
}