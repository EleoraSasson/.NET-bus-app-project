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
            Console.WriteLine("Welcome to our Testing Zone!!");

            IBL bl = BLFactory.GetBL(); //creating bl 

            #region Testing BusFleet
            Console.WriteLine("Testing BusFleet");

            DateTime time1 = new DateTime(2017, 1, 18);
            DateTime time2 = new DateTime(1999, 8, 25);
            DateTime time3 = new DateTime(2000, 5, 28);

            Bus bus1 = new Bus()
            {
                BusLicense = "123-23-123",
                BusMaintenanceDate = DateTime.Now,
                BusRegDate = time1,
                BusMileage = 700,
                BusFuel = 650
            };
            // not setting erased and status as done in CRUD

            Bus bus2 = new Bus()
            {
                BusLicense = "345-54-345",
                BusMaintenanceDate = DateTime.Now,
                BusRegDate = time2,
                BusMileage = 300,
                BusFuel = 450
            };

            Bus bus3 = new Bus()
            {
                BusLicense = "256-23-123",
                BusMaintenanceDate = DateTime.Now,
                BusRegDate = time3,
                BusMileage = 600,
                BusFuel = 1200
            };

            bl.AddToBusFleet(bus1);
            bl.AddToBusFleet(bus2);
            bl.AddToBusFleet(bus3);

            Console.WriteLine("getting all buses");

            Console.WriteLine(bl.GetEntireBusFleet());

            Console.WriteLine("getting bus3 - no error");

            Console.WriteLine(bl.GetBusFromFleet("256-23-123"));

            Console.WriteLine("updating bus3 - changing fuel to 0 and mileage to 700");

            bus3.BusFuel = 0;//changing fuel to 0 
            bus3.BusMileage = 700; //chaning mileage to 700
            bl.UpdateBusFleet(bus3); //updating

            Console.WriteLine("getting updated bus3");
            Console.WriteLine(bl.GetBusFromFleet("256-23-123"));

            Console.WriteLine("removing bus 2");
            bl.DeleteFromBusFleet(bus2);//deleting bus2

            Console.WriteLine("reprint fleet without bus 2");
            Console.WriteLine(bl.GetEntireBusFleet());

            //Console.WriteLine("getting one bus that does not exist");

            //Console.WriteLine(bl.GetBusFromFleet("111-11-111"));
            #endregion
            //NOTE TO SELF - for BusRoute check why the program is crashing by the update function and see that update actually updates
            // also change the style of the setting info so that start and end stations are also set in the add and changed accordingly when 
            // new stations are added to the routes
            //maybe edit the running numbers ID patter so it satrst at higher than 1?
            #region Testing BusRoute
            Console.WriteLine("Testing BusRoute");

            BusLine line1 = new BusLine()
            { //line ID not included as that should be put in the dl layer of addBusLine
                BusLineNo = 1,
                BusRegion = Regions.Center_Jerusalem,
                //BusStart = "12345",
                //BusEnd = "67890",
                //not including the start and end stations as this is set in the dl bl layer when we know what lineStations are in the busRoute
            };
            BusLine line2 = new BusLine()
            { //line ID not included as that should be put in the dl layer of addBusLine
                BusLineNo = 2,
                BusRegion = Regions.North_Haifa,
                //BusStart = "54321",
                //BusEnd = "09876",
                //not including the start and end stations as this is set in the dl bl layer when we know what lineStations are in the busRoute
            };

            LineStation sta1 = new LineStation()
            {
                //line ID not included as set later in dl add
                stationCode = "11111",
                stationNumber = 1, //really should be added later for now will leave - how??
            };
            LineStation sta2 = new LineStation()
            {
                //line ID not included as set later in dl add
                stationCode = "22222",
                stationNumber = 2, //really should be added later for now will leave - how??
            };
            LineStation sta3 = new LineStation()
            {
                //line ID not included as set later in dl add
                stationCode = "33333",
                stationNumber = 3, //really should be added later for now will leave - how??
            };
            LineStation sta4 = new LineStation()
            {
                //line ID not included as set later in dl add
                stationCode = "44444",
                stationNumber = 4, //really should be added later for now will leave - how??
            };
            LineStation sta5 = new LineStation()
            {
                //line ID not included as set later in dl add
                stationCode = "55555",
                stationNumber = 5, //really should be added later for now will leave - how??
            };

            List<LineStation> stations1 = new List<LineStation>();
            stations1.Add(sta1);
            stations1.Add(sta2);


            List<LineStation> stations2 = new List<LineStation>();
            stations2.Add(sta4);
            stations2.Add(sta5);

            BusRoute bRoute1 = new BusRoute()
            {

                Route = line1,
                RouteStops = stations1,
            };


            BusRoute bRoute2 = new BusRoute()
            {

                Route = line2,
                RouteStops = stations2,
            };

            Console.WriteLine("adding bRoute1 and bRoute2");
            bl.AddBusRoute(bRoute1);
            bl.AddBusRoute(bRoute2);

            Console.WriteLine("getting all busRoutes");
            Console.WriteLine(bl.GetBusRoute(bRoute1.Route.BusLineID));
            Console.WriteLine(bl.GetBusRoute(bRoute2.Route.BusLineID)); 

            Console.WriteLine("adding a station to busRoute 1");
            bl.AddStationToBusRoute(bRoute1, sta3);
            Console.WriteLine(bl.GetBusRoute(bRoute1.Route.BusLineID)); 

            Console.WriteLine("getting all the stations in bRoute2");
            Console.WriteLine(bl.GetAllStationsInBusRoute(bRoute2.Route.BusLineID.ToString()));

            Console.WriteLine("updating the region of BusRoute2");
            bRoute2.Route.BusRegion = Regions.National;
            bl.UpdateBusRoute(bRoute2); //check here for error!!!

            Console.WriteLine(bl.GetBusRoute(bRoute2.Route.BusLineNo));

            Console.WriteLine("deleting a busStop from bRoute1");
            List<LineStation>stations = bRoute1.RouteStops.ToList();
            stations.Remove(sta2);
            bRoute1.RouteStops = stations;
            Console.WriteLine(bl.GetAllStationsInBusRoute(bRoute1.Route.BusLineID.ToString()));
            #endregion

            #region Testing StationWithRoutes
            #endregion
            //write and implement check for StationsWithRoutes
        }

    }
}


