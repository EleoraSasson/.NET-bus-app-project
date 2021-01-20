using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Buses
    {

        private Bus theBus;

        public Bus bus
        {
            get { return theBus; }
            set { theBus = value; }
        }

        public override string ToString()
        {
            return "Bus Information:\n License Number:" + bus.BusLicense + "\n Registration Date:" + bus.BusRegDate + "\n Last Maintenance Date:" + bus.BusMaintenanceDate + "\n Mileage Reading:" + bus.BusMileage + "\n Fuel Amount:" + bus.BusFuel + "\n Status of Bus:" + bus.BusStatus + "\n";

        }
    }
}
