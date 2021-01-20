using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALApi;
using DO;

namespace BO
{
    //if we really feel the need will add successive stations
    public class LineTrip
    {

        private ScheduleOfRoute schedule;

        public ScheduleOfRoute LTSchedule //lineID
        {
            get { return schedule; }
            set { schedule = value; }
        }

        private BusStations station;

        public BusStations LTStation //stopCOde
        {
            get { return station; }
            set { station = value; }
        }

        private BusOnTrip bus; //roadID

        public BusOnTrip LTbus
        {
            get { return bus; }
            set { bus = value; }
        }
    }
}
