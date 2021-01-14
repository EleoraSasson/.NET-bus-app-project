using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALApi;
using DO;
using BL;
using BLApi;
using BO;

namespace PLConsole
{ 
    class Program
    { 
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to our Tetsting Zone!!");

            IBL bl = BLFactory.GetBL();

            Console.WriteLine("Testing BusFleet");
            DateTime time = new DateTime(2017, 1, 18);

            Bus bus1 = new Bus()
            {
                BusLicense = "123-23-123",
                BusMaintenanceDate = DateTime.Now,
                BusRegDate = time,
                BusMileage = 725,
                BusFuel = 785
            };
            // not setting erased and status as done in CRUD

            Bus bus2 = new Bus()
            {
                BusLicense = "345-54-345",
                BusMaintenanceDate = DateTime.Now,
                BusRegDate = time,
                BusMileage = 75,
                BusFuel = 753
            };

            Bus bus3 = new Bus()
            {
                BusLicense = "256-23-123",
                BusMaintenanceDate = DateTime.Now,
                BusRegDate = time,
                BusMileage = 650,
                BusFuel = 1200
            };

            bl.AddToBusFleet(bus1);
            bl.AddToBusFleet(bus2);
            bl.AddToBusFleet(bus3);

            Console.WriteLine("getting all buses");
            Console.WriteLine(bl.GetEntireBusFleet());
            Console.WriteLine("getting one bus - no error");
            Console.WriteLine(bl.GetBusFromFleet("256-23-123"));
            Console.WriteLine("update bus that you got");
            bus3.BusFuel = 0;
            bl.UpdateBusFleet(bus3);
            Console.WriteLine("getting one bus - no error");
            Console.WriteLine(bl.GetBusFromFleet("256-23-123"));
            bl.DeleteFromBusFleet(bus2);
            Console.WriteLine("reprint fleet without bus 2");
            Console.WriteLine(bl.GetEntireBusFleet());
            Console.WriteLine("getting one bus that does not exist");
            Console.WriteLine(bl.GetBusFromFleet("test"));

        }
            
    }
}

//namespace PLConsole
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {


//            Console.WriteLine("Welcome to our Testing Zone!");

//            IBL bl = BLFactory.GetBL();

//            BusLine line = new BusLine() { BusLineNo = 1, BusLineID = 123456, BusRegion = Regions.Center_Jerusalem, BusEnd = "cherry", BusStart = "berry", LineErased = false; }
//            BO.BusRoute route = new BO.BusRoute()
//            {
//                route.Route =line;
//            }
//             bl.AddBusRoute(route);
//        }
  
            //IDAL dal = DLFactory.GetDL();
            //int n;
            //Console.WriteLine(n);
            ////Console.WriteLine("Testing AddBus:");
            ////Bus bus = new Bus(); //decaring an instance of type Bus
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
