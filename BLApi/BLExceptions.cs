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

    #region CompanyScehdule Exceptions

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

}
