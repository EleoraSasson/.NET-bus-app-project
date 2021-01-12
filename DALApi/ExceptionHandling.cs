using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    #region Bus Exceptions
    public class InvalidBusLicenseException : Exception
    {
        public string busLicense;

        public InvalidBusLicenseException(string license) : base() => busLicense = license;
        public InvalidBusLicenseException(string license, string msg) : base(msg) { busLicense = license; }
        public InvalidBusLicenseException(string license, string msg, Exception innerException) : base(msg, innerException) { busLicense = license; }

        public override string ToString() => base.ToString() + $", invalid bus license: {busLicense}";
    }
    #endregion

    #region LineStation Exceptions
    public class MissingLineStationException : Exception
    {
        public string eKey;

        public MissingLineStationException(string entityKey) : base() => eKey = entityKey;
        public MissingLineStationException(string entityKey, string msg) : base(msg) { eKey = entityKey; }
        public MissingLineStationException(string entityKey, string msg, Exception innerException) : base(msg, innerException) { eKey = entityKey; }

        public override string ToString() => base.ToString() + $", missing line station: {eKey}";
    }

    public class ExsistingLineStationException : Exception
    {

        public string eKey;

        public ExsistingLineStationException(string entityKey) : base() => eKey = entityKey;
        public ExsistingLineStationException(string entityKey, string msg) : base(msg) { eKey = entityKey; }
        public ExsistingLineStationException(string entityKey, string msg, Exception innerException) : base(msg, innerException) { eKey = entityKey; }

        public override string ToString() => base.ToString() + $", line station already exsists: {eKey}";
    }
    #endregion

    #region Staff Exceptions
    public class StaffAlreadyInSystemException : Exception
    {

        public string ID;

        public StaffAlreadyInSystemException(string id) : base() => ID = id;
        public StaffAlreadyInSystemException(string id, string msg) : base(msg) { ID = id; }
        public StaffAlreadyInSystemException(string id, string msg, Exception innerException) : base(msg, innerException) { ID = id; }

        public override string ToString() => base.ToString() + $", missing line station: {ID}";
    }
    #endregion

    #region BusLine Exceptions
    public class InvalidBusLineException : Exception
    {
        public string lineId;

        public InvalidBusLineException(string id) : base() => lineId = id;
        public InvalidBusLineException(string id, string msg) : base(msg) { lineId = id; }
        public InvalidBusLineException(string id, string msg, Exception innerException) : base(msg, innerException) { lineId = id; }

        public override string ToString() => base.ToString() + $", invalid line ID: {lineId}";
    }
    #endregion

    #region BusOnTrip Exceptions
    public class InvalidBusOnTripIDException : Exception
    {
        public string busOnTripID;

        public InvalidBusOnTripIDException(string id) : base() => busOnTripID = id;
        public InvalidBusOnTripIDException(string id, string msg) : base(msg) { busOnTripID = id; }
        public InvalidBusOnTripIDException(string id, string msg, Exception innerException) : base(msg, innerException) { busOnTripID = id; }

        public override string ToString() => base.ToString() + $", invalid bus license: {busOnTripID}";
    }
    #endregion

    #region Stop Exceptions
    public class InvalidStopCodeException : Exception
    {
        public string stopCode;

        public InvalidStopCodeException(string code) : base() => stopCode = code;
        public InvalidStopCodeException(string code, string msg) : base(msg) { stopCode = code; }
        public InvalidStopCodeException(string code, string msg, Exception innerException) : base(msg, innerException) { stopCode = code; }

        public override string ToString() => base.ToString() + $", invalid bus license: {stopCode}";
    }
    #endregion

    #region LineLeaving Exceptions
    public class InvalidLineLeavingKeyException : Exception
    {
        public string key;

        public InvalidLineLeavingKeyException(string lineKey) : base() => key = lineKey;
        public InvalidLineLeavingKeyException(string lineKey, string msg) : base(msg) { key = lineKey; }
        public InvalidLineLeavingKeyException(string lineKey, string msg, Exception innerException) : base(msg, innerException) { key = lineKey; }

        public override string ToString() => base.ToString() + $", invalid bus license: {key}";
    }
    #endregion

    #region Staff Exceptions
    public class StaffNotInSystemException : Exception
    {

        public string ID;

        public StaffNotInSystemException(string id) : base() => ID = id;
        public StaffNotInSystemException(string id, string msg) : base(msg) { ID = id; }
        public StaffNotInSystemException(string id, string msg, Exception innerException) : base(msg, innerException) { ID = id; }

        public override string ToString() => base.ToString() + $", missing line station: {ID}";
    }
    #endregion

    #region SuccessiveStations Exceptions
    public class MissingSuccessiveStationsException : Exception
    {

        public string eKey;

        public MissingSuccessiveStationsException(string entityKey) : base() => eKey = entityKey;
        public MissingSuccessiveStationsException(string entityKey, string msg) : base(msg) { eKey = entityKey; }
        public MissingSuccessiveStationsException(string entityKey, string msg, Exception innerException) : base(msg, innerException) { eKey = entityKey; }

        public override string ToString() => base.ToString() + $", missing successive station: {eKey}";
    }

    public class ExsistingSuccessiveStationsException : Exception
    {

        public string eKey;

        public ExsistingSuccessiveStationsException(string entityKey) : base() => eKey = entityKey;
        public ExsistingSuccessiveStationsException(string entityKey, string msg) : base(msg) { eKey = entityKey; }
        public ExsistingSuccessiveStationsException(string entityKey, string msg, Exception innerException) : base(msg, innerException) { eKey = entityKey; }

        public override string ToString() => base.ToString() + $", successive station already exsists: {eKey}";
    }
    #endregion

    #region User Exceptions
    public class MissingUserException : Exception
    {

        public string name;

        public MissingUserException(string userName) : base() => name = userName;
        public MissingUserException(string userName, string msg) : base(msg) { name = userName; }
        public MissingUserException(string userName, string msg, Exception innerException) : base(msg, innerException) { name = userName; }

        public override string ToString() => base.ToString() + $", missing user: {name}";
    }

    public class ExsistingUserException : Exception
    {

        public string name;

        public ExsistingUserException(string userName) : base() => name = userName;
        public ExsistingUserException(string userName, string msg) : base(msg) { name = userName; }
        public ExsistingUserException(string userName, string msg, Exception innerException) : base(msg, innerException) { name = userName; }

        public override string ToString() => base.ToString() + $", user already exsists: {name}";
    }
    #endregion

    #region UserTrip Exceptions
    public class MissingUserTripException : Exception
    {

        public string ID;

        public MissingUserTripException(string id) : base() => ID = id;
        public MissingUserTripException(string id, string msg) : base(msg) { ID = id; }
        public MissingUserTripException(string id, string msg, Exception innerException) : base(msg, innerException) { ID = id; }

        public override string ToString() => base.ToString() + $", missing user trip information: {ID}";
    }

    public class ExsistingUserTripException : Exception
    {

        public string ID;

        public ExsistingUserTripException(string id) : base() => ID = id;
        public ExsistingUserTripException(string id, string msg) : base(msg) { ID = id; }
        public ExsistingUserTripException(string id, string msg, Exception innerException) : base(msg, innerException) { ID = id; }

        public override string ToString() => base.ToString() + $", user trip information already in system: {ID}";
    }
    #endregion

    #region XML Exceptions
    public class XMLFileLoadCreateException : Exception
    {

        public string ID;

        public XMLFileLoadCreateException(string id) : base() => ID = id;
        public XMLFileLoadCreateException(string id, string msg) : base(msg) { ID = id; }
        public XMLFileLoadCreateException(string id, string msg, Exception innerException) : base(msg, innerException) { ID = id; }

        public override string ToString() => base.ToString() + $", missing user trip information: {ID}";
    }
    #endregion

}
