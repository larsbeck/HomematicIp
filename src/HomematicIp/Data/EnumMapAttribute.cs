﻿using System;
using System.Text;
using HomematicIp.Data.HomematicIpObjects;

namespace HomematicIp.Data
{
    public class EnumMapAttribute:Attribute
    {
        public object[] Enums { get; }

        public EnumMapAttribute(params object[] @enums)
        {
            Enums = @enums;
        }
    }
}

