using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALApi
{
    /// <summary>
    /// This class keeps track of the start values for every 
    /// running number used in the various entity keys throught the DAL 
    /// layer in the project
    /// </summary>
    class RunningNumbers
    {
        //Bus on Trip Running Number:
        private int runNumBus;

        public int BusRunNum
        {
            get { return runNumBus; }
            set { runNumBus = value; }
        }

        //Bus Line Running Number

        private int runNumLine;

        public int LineRunNum
        {
            get { return runNumLine; }
            set { runNumLine = value; }
        }

        //User Trip Running Number
        private int runNumUser;

        public int UserRunNum
        {
            get { return runNumUser; }
            set { runNumUser = value; }
        }

    }
}
