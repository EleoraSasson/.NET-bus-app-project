using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALApi;

namespace DO
{
    /// <summary>
    /// This class keeps track of the start values for every 
    /// running number used in the various entity keys throughtout the DAL 
    /// layer in the project
    /// </summary>
    public static class RunningNumbers
    {
        #region Running Number Line - LineID
        /// <summary>
        ///  Description: This is a 2 digit running number that acts as the bus line ID and is used to identify the specific bus line.
        ///  Type: Private Property.
        /// </summary>
        private static int runNum_line = 11;

        /// <summary>
        /// Description: This is a 2 digit running number that acts as the bus line ID and is used to identify the specific bus line. 
        /// Type: Public Property. 
        /// </summary>
        public static int LineRunNum { get { return runNum_line++; } }
        #endregion

        #region Running Number Bus - RoadID
        /// <summary>
        /// Description: This is a 3 digit running number that acts as the buses road ID and is used to identify the specific bus that is currently 
        /// on a trip.
        /// Type: Private Property.
        /// </summary>
        private static int runNum_bus = 111;
        /// <summary>
        /// Description: This is a 3 digit running number that acts as the buses road ID and is used to identify the specific bus that is currently 
        /// on a trip.
        /// Type: Public Property.
        /// </summary>
        public static int BusRunNum { get { return runNum_bus++; } }
        #endregion

        #region Running Number User - UserID for travel info
        /// <summary>
        /// Description: This is a 6 digit running number that acts as the users travel ID and is used to identify the specific user travel information 
        /// on a trip.
        /// Type: Private Property.
        /// </summary>
        private static int runNum_user = 111111;
        /// <summary>
        /// Description: This is a 6 digit running number that acts as the users travel ID and is used to identify the specific user travel information 
        /// on a trip.
        /// Type: Public Property.
        /// </summary>
        public static int UserRunNum { get { return runNum_user++; } }
        #endregion

        #region StaffID
        private static int runNum_staffID = 111111;

        public static int StaffRunNum
        {
            get { return runNum_staffID++; }
        }

        private static int runNum_ManagerID = 1111111;

        public static int ManagerRunNum
        {
            get { return runNum_ManagerID++; }
        }

        #endregion
    }
}
