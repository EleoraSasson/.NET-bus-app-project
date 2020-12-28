using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALApi
{
    class ExceptionHandling
    {

    }

    public class InvalidBusLicenseException : Exception
    {
        public string busLicense;

        public InvalidBusLicenseException(string license) : base() => busLicense = license;
        public InvalidBusLicenseException(string license, string msg) : base(msg) { busLicense = license; }
        public InvalidBusLicenseException(string license, string msg, Exception innerException) : base(msg, innerException) { busLicense = license; }

        public override string ToString() => base.ToString() + $", invalid bus license: {busLicense}";
    }

}
