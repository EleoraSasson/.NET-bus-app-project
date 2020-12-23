using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation :ICloneable
    {
        private int id; //string but can be an int, will see after
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

        //public int entityKey
        //{
        //    get { return lineID + stationCode; }
        //    set { key = lineID + stationCode; }
        //}


        private int stationNum;

        public int stationNumber //number of stations on the line
        {
            get { return stationNum; }
            set { stationNum = value; }
        }

        public LineStation()
        {
            lineID = 0;
            stationCode = 0;
            key = 0;
            stationNum = 0;
        }

        public LineStation(int id, int code, int s)
        {
            lineID = id;
            stationCode = code;
            key = int.Parse(lineID.ToString() + stationCode.ToString());
            stationNum = s;
        }
        //additional information?

        public override string ToString()
        {
            return "Line station details: \n Line ID: " + lineID + "  \n Station code: " + stationCode + " \n Station number: " + stationNum;
        }

    }
}
