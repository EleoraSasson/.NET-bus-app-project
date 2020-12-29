using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineLeaving :ICloneable
    {
        /* PROPERTIES */

        // Bus Line ID:
        private int lineID;

        public int BusLineID
        {
            get { return lineID; }
            set { lineID = value; }
        }

        // Start Time [First Line]:
        private TimeSpan firstLine;

        public TimeSpan BusFirstLine
        {
            get { return firstLine; }
            set { firstLine = value; }
        }

        private string entityKey;

        public string leavingEntityKey
        {
            get { return BusLineID.ToString() + BusFirstLine.ToString(); }
            set { entityKey = BusLineID.ToString() + BusFirstLine.ToString(); }

        }


        // Frequency of Departure [Number of Lines per day]:
        //Note: 0 implies a single line only
        private int numLines;

        public int BusNumLines
        {
            get { return numLines; }
            set { numLines = value; }
        }

        // End Time of Travel (per 24hrs) [Last Line]:
        // Note: only applicable if at least 1 in BusNumLines
        private TimeSpan lastLine;

        public TimeSpan BusLastLine
        {
            get { return lastLine; }
            set { lastLine = value; }
        }

        /* OVERRIDE TOSTRING */
        public override string ToString()
        {
            return "Bus Line ID: " + BusLineID + "\n Starting Time of Bus Line:" + BusFirstLine + "\n Ending Time of Bus Line:" + BusLastLine + "Number of Lines Leaving Today:" + BusNumLines; 
        }
    }
}
