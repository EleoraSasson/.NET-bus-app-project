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
        private string start; 

        public string BusStart
        {
            get { return start; }
            set { start = value; }
        }

        // Last Stop No. (aka end stop):
        private string end;

        public string BusEnd
        {
            get { return end; }
            set { end = value; }
        }

        //addtional information:

        
        // took out because not so necessary
        //// Bus Line Leaving: n
        //private string leaving;

        //public string  BusLeaving
        //{
        //    get { return leaving; }
        //    set { leaving = value; }
        //}

        // Line Erased [if a line is no longer in use then erased is true, else false]:
        private bool erased;

        public bool LineErased
        {
            get { return erased; }
            set { erased = value; }
        }


        /* OVERRIDE TOSTRING */
        public override string ToString()
        {
            return "Bus Line Information: \n Line ID:" + BusLineID + "\n Line No.:" + BusLineNo + "\n Region of Line:" + BusRegion + "\n Start Station:" + BusStart + "\n End Station:" + BusEnd + " Line Schedule:" + BusLeaving; 
        }
    }
}
