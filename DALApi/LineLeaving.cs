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


        /* CONSTRUCTORS */

        // Default Ctor:
        public LineLeaving()
        {
            BusLineID = 0;
            TimeSpan StartTime = new TimeSpan(5,00,00); //default start time of all lines is 5am
            BusFirstLine = StartTime;
            TimeSpan EndTime = new TimeSpan(23, 00, 00); //default end time is 11pm
            BusLastLine = EndTime;
        }

        // Ctor:
        public LineLeaving(int param1, bool param2)//chnage param
        {

        }

        /* OVERRIDE TOSTRING */
        public override string ToString()
        {
            return "Bus Line ID: " + BusLineID + "\n Starting Time of Bus Line:" + BusFirstLine + "\n Ending Time of Bus Line:" + BusLastLine + "Number of Lines Leaving Today:" + BusNumLines; 
        }
    }
}
