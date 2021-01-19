using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using DO;
using DALApi;


namespace BO
{
    public class Stations
    {
        private LineStation station;

        public LineStation StatStation
        {
            get { return station; }
            set { station = value; }
        }

        public override string ToString()
        {
            return "(" + StatStation.stationNumber + ") Station Code: " + StatStation.stationCode;
        }

    }
}
