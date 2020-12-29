using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation :ICloneable
    {
        private string id; 
        public string lineID
        {
            get { return id; }
            set { id = value; }
        }

        private string station;

        public string stationCode
        {
            get { return station; }
            set { station = value; }
        }


        private int stationNum;

        public int stationNumber //number of stations on the line
        {
            get { return stationNum; }
            set { stationNum = value; }
        }

        /* OVERRIDE TOSTRING */
        public override string ToString()
        {
            return "Line station details: \n Line ID: " + lineID + "  \n Station code: " + stationCode + " \n Station number: " + stationNum;
        }

    }
}
