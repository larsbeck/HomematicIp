using System;
using System.Collections.Generic;
using HomematicIp.Data.Enums;
using HomematicIp.Data.HomematicIpObjects.Channels;
using HomematicIp.Data.HomematicIpObjects.Home;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomematicIp.Data.JsonConverters
{
    public class FunctionalHomesConverter : AbstractListConverter<FunctionalHome, FunctionalHomeType>
    {
        public FunctionalHomesConverter(string typeDefiningProperty= "solution") : base(typeDefiningProperty)
        {
        }
    }
}