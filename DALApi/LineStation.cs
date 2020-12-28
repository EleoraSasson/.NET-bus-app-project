using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation :ICloneable
    {
        private int id; 
        public int lineID
        {
            get { return id; }
            set { id = value; }
        }

        private int station;

        public int stationCode
        {
            get { return station; }
            set { station = value; }
        }

        private int key;

        public int entityKey
        {
            get { return lineID + stationCode; }
            set { key = lineID + stationCode; }
        }


        private int stationNum;

        public int stationNumber //number of stations on the line
        {
            get { return stationNum; }
            set { stationNum = value; }
        }
        public override string ToString()
        {
            return "Line station details: \n Line ID: " + lineID + "  \n Station code: " + stationCode + " \n Station number: " + stationNum;
        }

    }
}
