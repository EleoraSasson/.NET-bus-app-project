using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class UserTrip :ICloneable
    {
        /* PROPERTIES */

        private int travelID;

        public int userTravelID
        {
            get { return travelID; }
            set { travelID = value; }
        }

        private string name;

        public string userName
        {
            get { return name; }
            set { name = value; }
        }

        private int lineID;

        public int userLineID
        {
            get { return lineID; }
            set { lineID = value; }
        }

        private int bordingID;

        public int userBordingID
        {
            get { return bordingID; }
            set { bordingID = value; }
        }

        private int alightingID;

        public int userAlightingID
        {
            get { return alightingID; }
            set { alightingID = value; }
        }

        private TimeSpan bordTime;

        public TimeSpan userBordT
        {
            get { return bordTime; }
            set { bordTime = value; }
        }

        private TimeSpan alightTime;

        public TimeSpan userAlightT
        {
            get { return alightTime; }
            set { alightTime = value; }
        }

        /* OVERRIDE TOSTRING*/

        public override string ToString()
        {
            return "Travel Information: \n [username], travel ID [travelID], is abord line [lineID] from bording station [BordingID] which departed at [time borded] ; to travel to [alightingID] and arrive at [alighting time].\n"; 
        }
    }
}
