using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{ 
    
    #region BusFleet Exceptions
        public class BusAlreadyInSystemException : Exception
        {
            public string busLicense;

            public BusAlreadyInSystemException(string license) : base() => busLicense = license;
            public BusAlreadyInSystemException(string license, string msg) : base(msg) { busLicense = license; }
            public BusAlreadyInSystemException(string license, string msg, Exception innerException) : base(msg, innerException) { busLicense = license; }

            public override string ToString() => base.ToString() + $"{busLicense} already exsists in the Bus Fleet.";
        }

    public class BusMissingFromSystemException : Exception
    {
        public string busLicense;

        public BusMissingFromSystemException(string license) : base() => busLicense = license;
        public BusMissingFromSystemException(string license, string msg) : base(msg) { busLicense = license; }
        public BusMissingFromSystemException(string license, string msg, Exception innerException) : base(msg, innerException) { busLicense = license; }

        public override string ToString() => base.ToString() + $"{busLicense} does not exsist in the Bus Fleet.";
    }
    
    #endregion

}
