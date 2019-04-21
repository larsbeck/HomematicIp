using System;

namespace HomematicIp.Data
{
    public class UnknownHomematicObjectException : Exception
    {
        public UnknownHomematicObjectException(string message) : base(message)
        {
        }
    }
}