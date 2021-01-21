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

    #region CompanySchedule Exceptions

    public class CompanyScheduleAlreadyInSystemException : Exception
    {
        public string lineNo;

        public CompanyScheduleAlreadyInSystemException(int n) : base() => lineNo = n.ToString();
        public CompanyScheduleAlreadyInSystemException(int n, string msg) : base(msg) { lineNo = n.ToString(); }
        public CompanyScheduleAlreadyInSystemException(int n, string msg, Exception innerException) : base(msg, innerException) { lineNo = n.ToString(); }

        public override string ToString() => base.ToString() + $"{lineNo} already exsists in the company schedule.";
    }
    public class CompanyScheduleMissingFromSystemException : Exception
    {
        public string lineNo;

        public CompanyScheduleMissingFromSystemException(int n) : base() => lineNo = n.ToString();
        public CompanyScheduleMissingFromSystemException(int n, string msg) : base(msg) { lineNo = n.ToString(); }
        public CompanyScheduleMissingFromSystemException(int n, string msg, Exception innerException) : base(msg, innerException) { lineNo = n.ToString(); }

        public override string ToString() => base.ToString() + $"{lineNo} does not exsist in the company schedule.";
    }
    #endregion

    #region ScheduleOfRoutes Exceptions

    [Serializable]
    public class LineLeavingExists : Exception
    {
        public string key;
        public LineLeavingExists(string message, Exception innerException) :
            base(message, innerException) => key = ((DO.InvalidLineLeavingKeyException)innerException).key;
        public override string ToString() => base.ToString() + $", Existing line leaving: {key}.";
    }

    public class BusOnTripExists : Exception
    {
        public string key;
        public BusOnTripExists(string message, Exception innerException) :
            base(message, innerException) => key = ((DO.InvalidLineLeavingKeyException)innerException).key;
        public override string ToString() => base.ToString() + $", Existing bus on trip: {key}.";
    }

    #endregion

    #region Stations Exceptions

    public class BusStationNotInSystem : Exception
    {
        public string stationNo;

        public BusStationNotInSystem(string n) : base() => stationNo = n;
        public BusStationNotInSystem(string n, string msg) : base(msg) { stationNo = n; }
        public BusStationNotInSystem(string n, string msg, Exception innerException) : base(msg, innerException) { stationNo = n; }

        public override string ToString() => base.ToString() + $"{stationNo} does not exist";
    }

    #endregion

    #region UserPortal Exceptions

    [Serializable]
    public class UserExistException : Exception
    {
        public string name;
        public UserExistException(string message, Exception innerException) :
            base(message, innerException) => name = ((DO.ExsistingUserException)innerException).name;
        public override string ToString() => base.ToString() + $", existing user name: {name}.";
    }

    [Serializable]
    public class UserMissingExcpetion : Exception
    {
        public string name;
        public UserMissingExcpetion(string message, Exception innerException) :
            base(message, innerException) => name = ((DO.MissingUserException)innerException).name;
        public override string ToString() => base.ToString() + $"User {name} cannot be found in the system.";
    }

    #endregion

    #region Staff Exceptions

    [Serializable]
    public class StaffMissing : Exception
    {
        public string id;
        public StaffMissing(string message, Exception innerException) :
            base(message, innerException) => id = ((DO.StaffNotInSystemException)innerException).ID;
        public override string ToString() => base.ToString() + $", Staff {id} cannot be found in the system.";
    }
    #endregion

    #region BusRoutes Exceptions
    [Serializable]
    public class BusLineAlreadyInSytemException : Exception
    {
        public string id;
        public BusLineAlreadyInSytemException(string message, Exception innerException) :
            base(message, innerException) => id = ((DO.InvalidBusLineException)innerException).lineId;
        public override string ToString() => base.ToString() + $", Bus line {id} already exists.";
    }

    [Serializable]
    public class BusLineNotInSystem : Exception
    {
        public string id;
        public BusLineNotInSystem(string message, Exception innerException) :
            base(message, innerException) => id = ((DO.InvalidBusLineException)innerException).lineId;
        public override string ToString() => base.ToString() + $", Bus line {id} cannot be found in the system.";
    }

    [Serializable]
    public class LineStationExistsException : Exception
    {
        public string id;
        public LineStationExistsException(string message, Exception innerException) :
            base(message, innerException) => id = ((DO.ExsistingLineStationException)innerException).eKey;
        public override string ToString() => base.ToString() + $", Bus station {id} already exists.";
    }

    [Serializable]
    public class LineStationMissingException : Exception
    {
        public string id;
        public LineStationMissingException(string message, Exception innerException) :
            base(message, innerException) => id = ((DO.MissingLineStationException)innerException).eKey;
        public override string ToString() => base.ToString() + $", Bus station {id} already exists.";
    }

    [Serializable]
    public class BusLineMissingException : Exception
    {
        public string id;
        public BusLineMissingException(string message, Exception innerException) :
            base(message, innerException) => id = ((DO.InvalidBusLineException)innerException).lineId;
        public override string ToString() => base.ToString() + $", Bus line {id} cannot be found.";
    }
    #endregion

    #region Buses Exceptions
    [Serializable]
    public class BusExistsException : Exception
    {
        public string id;
        public BusExistsException(string message, Exception innerException) :
            base(message, innerException) => id = ((DO.InvalidBusLicenseException)innerException).busLicense;
        public override string ToString() => base.ToString() + $", Bus  {id} already exists.";

        #endregion
    }

}