﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusOnTrip :ICloneable
    { 
        /* PROPERTIES */
        // Bus ID on Road (auto-running number):
        private int roadID;

        public int BusRoadID
        {
            get { return roadID; }
            set { roadID = value; }
        }

        // License No. of Bus:
        private string license;

        public string BusLicense
        {
            get { return license; }
            set { license = value; }
        }

        // Formal Departure Time (DT):
        private DateTime formalDT;

        public DateTime BusFormalDT
        {
            get { return formalDT; }
            set { formalDT = value; }
        }

        // Actual Departure Time (DT):
        private DateTime actualDT;

        public DateTime BusActualDT
        {
            get { return actualDT; }
            set { actualDT = value; }
        }

        // Number of passed stops:
        private int passed;

        public int BusPassed
        {
            get { return passed; }
            set { passed = value; }
        }

        // Transit time at aforementioned station:
        private DateTime transitT;

        public DateTime BusTransitT
        {
            get { return transitT; }
            set { transitT = value; }
        }

        // Arrival time to next station:
        private DateTime arrivalT;

        public DateTime BusArrivalT
        {
            get { return arrivalT; }
            set { arrivalT = value; }
        }

        /* OVERRIDE TOSTRING */
        public override string ToString()
        {
            return "Line" + BusLicense + " - which has a road ID - " + BusRoadID + " - was scheduled to leave the departure station at " + BusFormalDT + "\n It left the departure station at " + BusActualDT + "\n It is Scheduled to arrive at the next station at " + BusArrivalT + "\n It has passed " + BusPassed + "stations and it's total transit time is " + BusTransitT + ".\n"; 
        }
    }
}
