﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusOnTrip :ICloneable
    {
        /* create a static "running number" that will increment at each 
        instantiation of this class: 
        started it at 1 so that first bus on trip id starts at 1*/

        public static int RunNumBus = 1; 
       

        /* PROPERTIES */
        // Bus ID on Road (auto-running number):
        private int roadID;

        public int BusRoadID
        {
            get { return roadID; }
            set { roadID = RunNumBus; }
        }

        // License No. of Bus:
        private string license;

        public string BusLicense
        {
            get { return license; }
            set { license = value; }
        }

        // ID of Currently Running Line:
        private int running;

        public int BusRunning
        {
            get { return running; }
            set { running = value; }
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

        // Drivers' ID Number //??Type Staff??
        /* CONSTRUCTORS */
    }
}