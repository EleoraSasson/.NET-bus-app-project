using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{

    /// <summary>
    /// This class keeps track of the start values for every 
    /// running number used in the various entity keys throught the DAL 
    /// layer in the project
    /// </summary>
    class RunningNumbers
    {
        /// <summary>
        /// This is a 4 digit running number that acts as the buses road ID and is used to identify the specific bus that is currently 
        /// on a trip.
        /// </summary>
        public static int runNum_Bus = 1111;

        /// <summary>
        /// This is a 2 digit running number that acts as the bus line ID and is used to identify the specific bus line.
        /// </summary>
        public static int runNum_Line = 01;

        /// <summary>
        /// This is a 6 digit running number that acts as the users travel ID and is used to identify the specific user travel information 
        /// on a trip.
        /// </summary>
        public static int runNum_userT = 111111;

        //private statc int myVar; //no set //increment in get num++

        //public static int MyProperty
        //{
        //    get { return myVar; }
        //    set { myVar = value; }
        //}

        ////Bus on Trip Running Number:
        //private int runNumBus;

        //public int BusRunNum
        //{
        //    get { return runNumBus; }
        //    set { runNumBus = value; }
        //}

        ////Bus Line Running Number

        //private int runNumLine;

        //public int LineRunNum
        //{
        //    get { return runNumLine; }
        //    set { runNumLine = value; }
        //}

        ////User Trip Running Number
        //private int runNumUser;

        //public int UserRunNum
        //{
        //    get { return runNumUser; }
        //    set { runNumUser = value; }
        //}

    }
}
