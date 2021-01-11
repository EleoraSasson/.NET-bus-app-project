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

            //Testing DL:

            //IDAL dal = DLFactory.GetDL();
            //Console.WriteLine("Testing AddBus:");
            //Bus bus = new Bus(); //decaring an instance of type Bus
            ////init: .. happens in the BL in the add of that IBL
            //bus.BusErased = false;
            //bus.BusFuel = 300;
            //bus.BusLicense = "12-241-12";
            //bus.BusMaintenanceDate = DateTime.Now;
            //bus.BusRegDate = DateTime.Now;
            //bus.BusStatus = Status.Available;
            ////adding the bus to the DL:
            //dal.AddBus(bus);
            //Console.WriteLine(bus);
            ////Console.WriteLine("testing delete bus");
            ////dal.DeleteBus(bus.BusLicense);
            ////Console.WriteLine(dal.GetBus(bus.BusLicense));
            //Bus bus2 = new Bus();
            //bus2.BusErased = false;
            //bus2.BusFuel = 200;
            //bus2.BusLicense = "12-200-12";
            //bus2.BusMaintenanceDate = DateTime.Now;
            //bus2.BusRegDate = DateTime.Now;
            //bus2.BusStatus = Status.AtService;
            //dal.AddBus(bus2);

            //Bus bus3 = new Bus();
            //bus3.BusErased = false;
            //bus3.BusFuel = 1200;
            //bus3.BusLicense = "00-200-12";
            //bus3.BusMaintenanceDate = DateTime.Now;
            //bus3.BusRegDate = DateTime.Now;
            //bus3.BusStatus = Status.Available;
            //dal.AddBus(bus3);

            //IEnumerable<Bus> buses = dal.GetAllBuses();
            //foreach (var b in buses)
            //    Console.WriteLine(b);

            ////IEnumerable<Bus> buss = dal.GetBusListWithSelectedFields(Bus, Status)
            ////    {
            ////    var object = Bus.Items
            ////         .Select(c => c.EmployeeFields)     // Select the fields per employee
            ////         .SelectMany(fields => fields)      // Flatten to a single sequence of fields
            ////         .OfType<EmployeeID>()              // Filter to only EmployeeID fields
            ////         .Select(id => id.Item)             // Convert to strings
            ////         .ToList();
            ////}

            //// IEnumerable<Bus> buss = dal.GetBusListWithSelectedFields(dal.GetBus);
            ///
           
            // Testing BL


        }
    }

}
