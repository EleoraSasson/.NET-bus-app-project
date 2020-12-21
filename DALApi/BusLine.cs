using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum Regions { Unknown, North_Golan, North_Haifa, Center_TelAviv, Center_Jerusalem, South_BeerSheva, South_Eilat, National };

    public class BusLine : ICloneable
    {
        /* PROPERTIES */

        // Bus Line ID (auto-running number):
        private int lineID;

        public int BusLineID
        {
            get { return lineID; }
            set { lineID = value; }
        }

        // Line Number:
        private int lineNo;

        public int BusLineNo
        {
            get { return lineNo; }
            set { lineNo = value; }
        }

        // Region In Israel:
        private Regions region;

        public Regions BusRegion
        {
            get { return region; }
            set { region = value; }
        }

        // First Stop No. (aka start stop):
        private BusStop start;

        public BusStop BusStart
        {
            get { return start; }
            set { start = value; }
        }

        // Last Stop No. (aka end stop):
        private BusStop end;

        public BusStop BusEnd
        {
            get { return end; }
            set { end = value; }
        }

        //addtional information:

        // Bus Line Leaving:
        private LineLeaving leaving;

        public LineLeaving  BusLeaving
        {
            get { return leaving; }
            set { leaving = value; }
        }

        /* CONSTRUCTORS */

        // Default ctor:
        BusLine()
        {
            BusLineID = 0; //auto running num;
            Random rand = new Random();
            var lineRand = rand.Next(1,80); //get a random number between between 1 and 80 to be your line number
            //create some check that lineNo does not already exsist??
            BusLineNo = lineRand;
            BusRegion = Regions.Unknown;
            BusStop startStop = new BusStop();
            BusStart = startStop;
            BusStop endStop = new BusStop();
            BusEnd = endStop;
            LineLeaving lineLeaving = new LineLeaving();
            BusLeaving = lineLeaving;
        }

        //ctor:
        BusLine(int idLine, int noLine, Regions regionLine, BusStop startLine, BusStop endLine, LineLeaving leavingLine)
        {
            BusLineID = idLine;
            BusLineNo = noLine;
            BusRegion = regionLine;
            BusStart = startLine;
            BusEnd = endLine;
            BusLeaving = leavingLine;
        }

        /* OVERRIDE TOSTRING */
        public override string ToString()
        {
            return "Bus Lien Information: \n Line ID:" + BusLineID + "\n Line No.:" + BusLineNo + "\n Region of Line:" + BusRegion + "\n Start Station:" + BusStart + "\n End Station:" + BusEnd + " Line Schedule:" + BusLeaving; 
        }
    }
}
