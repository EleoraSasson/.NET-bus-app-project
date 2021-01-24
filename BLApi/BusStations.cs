using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO
{
    public class BusStations
    {
        private BusStop bstop;

        public BusStop Stop
        {
            get { return bstop; }
            set { bstop = value; }
        }

        public override string ToString()
        {
            return "Stop ID: " + Stop.StopCode + " stop address: " + Stop.StopAddress + " location: " + Stop.StopLocation.Latitude + "," + Stop.StopLocation.Longitude;
        }
    }
}
