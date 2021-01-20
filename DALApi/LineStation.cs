using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation :ICloneable
    {
        //line id
        private string id; 
        public string lineID
        {
            get { return id; }
            set { id = value; }
        }

        //station Code
        private string station;

        public string stationCode
        {
            get { return station; }
            set { station = value; }
        }

        //number of stations on the line
        private int stationNum;

        public int stationNumber 
        {
            get { return stationNum; }
            set { stationNum = value; }
        }

        // Distance:
        private int distance;

        public int StationDistance
        {
            get { return distance; }
            set { distance = value; }
        }

        // Average Travel Time:
        private TimeSpan aveTravelT;

        public TimeSpan StationAveTravelT
        {
            get { return aveTravelT; }
            set { aveTravelT = value; }
        }

        /* OVERRIDE TOSTRING */
        public override string ToString()
        {
            return "Line station details: \n Line ID: " + lineID + "  \n Station code: " + stationCode + " \n Station number: " + stationNum;
        }

    }
}
