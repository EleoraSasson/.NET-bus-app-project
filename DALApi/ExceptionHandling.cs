using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{

    public class InvalidBusLicenseException : Exception
    {
        public string busLicense;

        public InvalidBusLicenseException(string license) : base() => busLicense = license;
        public InvalidBusLicenseException(string license, string msg) : base(msg) { busLicense = license; }
        public InvalidBusLicenseException(string license, string msg, Exception innerException) : base(msg, innerException) { busLicense = license; }

        public override string ToString() => base.ToString() + $", invalid bus license: {busLicense}";
    }

    public class InvalidBusLineException : Exception
    {
        public string lineId;

        public InvalidBusLineException(string id) : base() => lineId = id;
        public InvalidBusLineException(string id, string msg) : base(msg) { lineId = id; }
        public InvalidBusLineException(string id, string msg, Exception innerException) : base(msg, innerException) { lineId = id; }

        public override string ToString() => base.ToString() + $", invalid line ID: {lineId}";
    }

    public class InvalidBusOnTripIDException : Exception
    {
        public string busOnTripID;

        public InvalidBusOnTripIDException(string id) : base() => busOnTripID = id;
        public InvalidBusOnTripIDException(string id, string msg) : base(msg) { busOnTripID = id; }
        public InvalidBusOnTripIDException(string id, string msg, Exception innerException) : base(msg, innerException) { busOnTripID = id; }

        public override string ToString() => base.ToString() + $", invalid bus license: {busOnTripID}";
    }

    public class InvalidStopCodeException : Exception
    {
        public string stopCode;

        public InvalidStopCodeException(string code) : base() => stopCode = code;
        public InvalidStopCodeException(string code, string msg) : base(msg) { stopCode = code; }
        public InvalidStopCodeException(string code, string msg, Exception innerException) : base(msg, innerException) { stopCode = code; }

        public override string ToString() => base.ToString() + $", invalid bus license: {stopCode}";
    }

    public class InvalidLineLeavingKeyException : Exception
    {
        public string key;

        public InvalidLineLeavingKeyException(string lineKey) : base() => key = lineKey;
        public InvalidLineLeavingKeyException(string lineKey, string msg) : base(msg) { key = lineKey; }
        public InvalidLineLeavingKeyException(string lineKey, string msg, Exception innerException) : base(msg, innerException) { key = lineKey; }

        public override string ToString() => base.ToString() + $", invalid bus license: {key}";
    }

}
