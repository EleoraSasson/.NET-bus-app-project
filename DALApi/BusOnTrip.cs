using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusOnTrip : ICloneable
    {
        /* PROPERTIES */
        // Bus ID on Road (auto-running number):
        private int roadID;

        public int BusRoadID
        {
            get { return roadID; }
            set { roadID = value; }
        }

        //Line ID:
        private string lineID;

        public string BusLineID
        {
            get { return lineID; }
            set { lineID = value; }
        }

        // License No. of Bus:
        private string license;

        public string BusLicense
        {
            get { return license; }
            set { license = value; }
        }

        // Formal Departure Time (DT):
        private TimeSpan formalDT;

        public TimeSpan BusFormalDT
        {
            get { return formalDT; }
            set { formalDT = value; }
        }

        //// Actual Departure Time (DT):
        //private TimeSpan actualDT;

        //public TimeSpan BusActualDT
        //{
        //    get { return actualDT; }
        //    set { actualDT = value; }
        //}

        // Number of passed stops:
        private int passed;

        public int BusPassed
        {
            get { return passed; }
            set { passed = value; }
        }

        // Transit time at aforementioned station:
        //private TimeSpan transitT;

        //public TimeSpan BusTransitT
        //{
        //    get { return transitT; }
        //    set { transitT = value; }
        //}

        // Arrival time to next station:
        private TimeSpan arrivalT;

        public TimeSpan BusArrivalT
        {
            get { return arrivalT; }
            set { arrivalT = value; }
        }

        /* OVERRIDE TOSTRING */
        public override string ToString()
        {
            return "Line" + BusLicense + " - which has a road ID - " + BusRoadID + " - was scheduled to leave the departure station at " + BusFormalDT + "\n It left the departure station at " + BusFormalDT + "\n It is Scheduled to arrive at the next station at " + BusArrivalT + "\n It has passed " + BusPassed + "\n";
        }
    }
}