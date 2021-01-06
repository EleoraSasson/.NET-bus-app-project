using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALApi;
using DO;

namespace PLConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to our Testing Zone!");
            
            IDAL dal = DLFactory.GetDL();
            Console.WriteLine("Testing AddBus:");
            Bus bus = new Bus(); //decaring an instance of type Bus
            //init: .. happens in the BL in the add of that IBL
            bus.BusErased = false;
            bus.BusFuel = 300;
            bus.BusLicense = "12-241-12";
            bus.BusMaintenanceDate = DateTime.Now;
            bus.BusRegDate = DateTime.Now;
            bus.BusStatus = Status.Available;
            //adding the bus to the DL:
            dal.AddBus(bus);
        }
    }

}
