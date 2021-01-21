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
using System.Device.Location;

namespace PLConsole
{ 
    class Program 
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Welcome to our Testing Zone!!");

            IBL bl = BLFactory.GetBL(); //creating bl 
            //IDAL dal = DLFactory.GetDL();

            List<UserPortal> users = bl.GetAllUsers().ToList();
            foreach (var u in users)
            {
                Console.WriteLine(u.ToString());
            }
            //BusLine line = dal.GetBusLine("11");


            //LineStation lineStation1 = new LineStation();

            //List<LineStation> lineSlist = new List<LineStation>();
            //lineSlist.Add(lineStation1);

            //Staff staff = new Staff()
            //{
            //    BusDriverID = "01010",
            //    isAdmin = false,
            //    BusDriverFirst = DriverFirstName.Debbie,
            //    BusDriverLast = DriverLastName.Cohen,
            //    BusDriverAge = 43,
            //    StaffPosition = Position.Driver,
            //    StaffYrsWorked = 3,
            //    BusDriverCellNo = "072-322-1322",
            //    LoginPassword = "xyz"
            //};

            //TimeSpan startT = new TimeSpan(08,30,00);
            //TimeSpan endT = new TimeSpan(16,00,00);

            //LineLeaving lineL = new LineLeaving()
            //{
            //    BusFirstLine = startT,
            //    BusLastLine = endT,
            //    BusNumLines = 4
            //};

            //BusRoute route = new BusRoute()
            //{
            //    Route = line,
            //    RouteStops = lineSlist
            //};

            //DateTime time2 = new DateTime(1999, 8, 25);

            //Bus actualBus = new Bus()
            //{
            //    BusLicense = "122-12-122",
            //    BusMaintenanceDate = DateTime.Now,
            //    BusRegDate = time2,
            //    BusMileage = 300,
            //    BusFuel = 450
            //};

            //DateTime FDT = new DateTime(200, DateTimeKind.Local);
            //DateTime DT = new DateTime(300, DateTimeKind.Local);
            //DateTime AT = new DateTime(400, DateTimeKind.Local);
            //DateTime TT = new DateTime(600, DateTimeKind.Local);

            //BusOnTrip bus = new BusOnTrip()
            //{
            //    BusFormalDT = FDT,
            //    BusActualDT = DT,
            //    BusArrivalT = AT,
            //    BusTransitT = TT,
            //    BusLicense = actualBus.BusLicense,

            //};

            //ScheduleOfRoute scheduleR = new ScheduleOfRoute() 
            //{ 
            //  CurrentRoute = route,
            //  SelectedStaff = staff,
            //  RouteSchedule = lineL,
            //  BusOnRoute = bus
            //};

            //bl.AddScheduleOfRoute(scheduleR);

            //Console.WriteLine(   bl.GetScheduleOfRoute("19"));


            //Stations stations = new Stations()
            //{
            //    StatStation = dal.GetLineStation("1139024"),
            //};

            //BusStations bStat = new BusStations()
            //{
            //    Stop = dal.GetBusStop("39024"),
            //};
            //List<BusRoute> listRoute = bl.GetRoutesofStation(bStat).ToList();
            //foreach (var route in listRoute)
            //{
            //    Console.WriteLine(route);
            //}

            //BusFleet Bfleet = bl.GetEntireBusFleet();
            ////foreach (var bus in Bfleet.busesInFleet.ToList())
            ////{
            ////    Console.WriteLine(bus);
            ////}

            ////List<BusRoute> routes = bl.GetAllBusRoutes().ToList();
            ////foreach (var r in routes)
            ////{
            ////    Console.WriteLine(r);
            ////}


            //List<BusStations> iamtired = bl.getAllBusStops().ToList();
            //foreach(var r in iamtired)
            //{
            //   // r.Stop.StopLocation.
            //  //  Console.WriteLine(r);
            //}

            //List<Buses> notagain = bl.GetAllBuses().ToList();
            //foreach (var r in notagain)
            //{

            //    Console.WriteLine(r);
            //}
            ////#region Testing BusFleet
            ////Console.WriteLine("Testing BusFleet");

            ////DateTime time1 = new DateTime(2017, 1, 18);
            ////DateTime time2 = new DateTime(1999, 8, 25);
            ////DateTime time3 = new DateTime(2000, 5, 28);

            ////Bus bus1 = new Bus()
            ////{
            ////    BusLicense = "123-23-123",
            ////    BusMaintenanceDate = DateTime.Now,
            ////    BusRegDate = time1,
            ////    BusMileage = 700,
            ////    BusFuel = 650
            ////};
            ////// not setting erased and status as done in CRUD

            ////Bus bus2 = new Bus()
            ////{
            ////    BusLicense = "345-54-345",
            ////    BusMaintenanceDate = DateTime.Now,
            ////    BusRegDate = time2,
            ////    BusMileage = 300,
            ////    BusFuel = 450
            ////};

            ////Bus bus3 = new Bus()
            ////{
            ////    BusLicense = "256-23-123",
            ////    BusMaintenanceDate = DateTime.Now,
            ////    BusRegDate = time3,
            ////    BusMileage = 600,
            ////    BusFuel = 1200
            ////};

            ////bl.AddToBusFleet(bus1);
            ////bl.AddToBusFleet(bus2);
            ////bl.AddToBusFleet(bus3);

            ////Console.WriteLine("getting all buses");

            ////List<Bus> fleet = new List<Bus>();

            ////fleet = bl.GetEntireBusFleet().ToList(); 

            ////foreach (Bus b in fleet)
            ////    Console.WriteLine(b);

            ////Console.WriteLine("getting bus3 - no error");

            ////Console.WriteLine(bl.GetBusFromFleet("256-23-123"));

            ////Console.WriteLine("updating bus3 - changing fuel to 0 and mileage to 700");

            ////bus3.BusFuel = 0;//changing fuel to 0 
            ////bus3.BusMileage = 700; //chaning mileage to 700
            ////bl.UpdateBusFleet(bus3); //updating

            ////Console.WriteLine("getting updated bus3");
            ////Console.WriteLine(bl.GetBusFromFleet("256-23-123"));

            ////Console.WriteLine("removing bus 2");
            ////bl.DeleteFromBusFleet(bus2);//deleting bus2

            ////Console.WriteLine("reprint fleet without bus 2");
            ////fleet = bl.GetEntireBusFleet().ToList();
            ////foreach (Bus b in fleet)
            ////    Console.WriteLine(b);

            //////Console.WriteLine("getting one bus that does not exist");

            //////Console.WriteLine(bl.GetBusFromFleet("111-11-111"));
            ////#endregion
            //////NOTE TO SELF - for BusRoute check why the program is crashing by the update function and see that update actually updates
            ////// also change the style of the setting info so that start and end stations are also set in the add and changed accordingly when 
            ////// new stations are added to the routes
            //////maybe edit the running numbers ID patter so it satrst at higher than 1?
            ////#region Testing BusRoute
            ////Console.WriteLine("Testing BusRoute");

            ////BusLine line1 = new BusLine()
            ////{ //line ID not included as that should be put in the dl layer of addBusLine
            ////    BusLineNo = 1,
            ////    BusRegion = Regions.Center_Jerusalem,
            ////    //BusStart = "12345",
            ////    //BusEnd = "67890",
            ////    //not including the start and end stations as this is set in the dl bl layer when we know what lineStations are in the busRoute
            ////};
            ////BusLine line2 = new BusLine()
            ////{ //line ID not included as that should be put in the dl layer of addBusLine
            ////    BusLineNo = 2,
            ////    BusRegion = Regions.North_Haifa,
            ////    //BusStart = "54321",
            ////    //BusEnd = "09876",
            ////    //not including the start and end stations as this is set in the dl bl layer when we know what lineStations are in the busRoute
            ////};

            ////LineStation sta1 = new LineStation()
            ////{
            ////    //line ID not included as set later in dl add
            ////    stationCode = "11111",
            ////    stationNumber = 1, //really should be added later for now will leave - how??
            ////};
            ////LineStation sta2 = new LineStation()
            ////{
            ////    //line ID not included as set later in dl add
            ////    stationCode = "22222",
            ////    stationNumber = 2, //really should be added later for now will leave - how??
            ////};
            ////LineStation sta3 = new LineStation()
            ////{
            ////    //line ID not included as set later in dl add
            ////    stationCode = "33333",
            ////    stationNumber = 3, //really should be added later for now will leave - how??
            ////};
            ////LineStation sta4 = new LineStation()
            ////{
            ////    //line ID not included as set later in dl add
            ////    stationCode = "44444",
            ////    stationNumber = 4, //really should be added later for now will leave - how??
            ////};
            ////LineStation sta5 = new LineStation()
            //{
            //    //line ID not included as set later in dl add
            //    stationCode = "55555",
            //    stationNumber = 5, //really should be added later for now will leave - how??
            //};

            //List<LineStation> stations1 = new List<LineStation>();
            //stations1.Add(sta1);
            //stations1.Add(sta2);


            //List<LineStation> stations2 = new List<LineStation>();
            //stations2.Add(sta4);
            //stations2.Add(sta5);

            //BusRoute bRoute1 = new BusRoute()
            //{

            //    Route = line1,
            //    RouteStops = stations1,
            //};


            //BusRoute bRoute2 = new BusRoute()
            //{

            //    Route = line2,
            //    RouteStops = stations2,
            //};

            //Console.WriteLine("adding bRoute1 and bRoute2");
            //bl.AddBusRoute(bRoute1);
            //bl.AddBusRoute(bRoute2);

            //Console.WriteLine("getting all busRoutes");
            //Console.WriteLine(bl.GetBusRoute(bRoute1.Route.BusLineID));
            //Console.WriteLine(bl.GetBusRoute(bRoute2.Route.BusLineID));

            //Console.WriteLine("adding a station to busRoute 1");
            //bl.AddStationToBusRoute(bRoute1, sta3);
            //Console.WriteLine(bl.GetBusRoute(bRoute1.Route.BusLineID));

            //Console.WriteLine("getting all the stations in bRoute2");
            //List<LineStation> stationList = bl.GetAllStationsInBusRoute(bRoute2.Route.BusLineID.ToString()).ToList();
            //foreach (LineStation ls in stationList)
            //    Console.WriteLine(ls);

            //Console.WriteLine("updating the region of BusRoute2");
            //bRoute2.Route.BusRegion = Regions.National;
            //bl.UpdateBusRoute(bRoute2);

            //Console.WriteLine(bl.GetBusRoute(bRoute2.Route.BusLineID)); //check here for error!!! & check deletion!

            //Console.WriteLine("deleting a busStop from bRoute1");
            //List<LineStation> stations = bRoute1.RouteStops.ToList();
            //stations.Remove(sta2);
            //bRoute1.RouteStops = stations;

            //stationList = bl.GetAllStationsInBusRoute(bRoute2.Route.BusLineID.ToString()).ToList();
            //foreach (LineStation ls in stationList)
            //    Console.WriteLine(ls);


            //#endregion

            //#region Testing StationWithRoutes

            //Random lat = new Random();
            //Random lon = new Random();

            //var randLat = lat.NextDouble() * (33.30 - 31.30) + 31.30;
            //var randLong = lon.NextDouble() * (35.50 - 34.30) + 34.30;

            //BusStop stop1 = new BusStop()
            //{
            //    StopLocation = new GeoCoordinate() { Longitude = randLong, Latitude = randLat },
            //    StopCode = "11111",
            //    StopName = "Cherry - Sugar Cane"
            //    //stopActive set in the dl automatically to be active
            //};
            //BusStop stop2 = new BusStop()
            //{
            //    StopLocation = new GeoCoordinate() { Longitude = randLong, Latitude = randLat },
            //    StopCode = "22222",
            //    StopName = "Strawberry - Cream"
            //    //stopActive set in the dl automatically to be active
            //};
            //BusStop stop3 = new BusStop()
            //{
            //    StopLocation = new GeoCoordinate() { Longitude = randLong, Latitude = randLat },
            //    StopCode = "33333",
            //    StopName = "Peanut - Butter"
            //    //stopActive set in the dl automatically to be active
            //};
            //BusStop stop4 = new BusStop()
            //{
            //    StopLocation = new GeoCoordinate() { Longitude = randLong, Latitude = randLat },
            //    StopCode = "44444",
            //    StopName = "Orange - Lemonade"
            //    //stopActive set in the dl automatically to be active
            //};
            //BusStop stop5 = new BusStop()
            //{
            //    StopLocation = new GeoCoordinate() { Longitude = randLong, Latitude = randLat },
            //    StopCode = "55555",
            //    StopName = "Apricot-Litchi"
            //    //stopActive set in the dl automatically to be active
            //};

            //StationWithRoutes RStat1 = new StationWithRoutes()
            //{
            //    CurrentStation = stop1,
            //    ///CurrentLines set in the bl layer 
            //};

            //StationWithRoutes RStat2 = new StationWithRoutes()
            //{
            //    CurrentStation = stop2,
            //    ///CurrentLines set in the bl layer 
            //};

            //StationWithRoutes RStat3 = new StationWithRoutes()
            //{
            //    CurrentStation = stop3,
            //    ///CurrentLines set in the bl layer 
            //};

            //StationWithRoutes RStat4 = new StationWithRoutes()
            //{
            //    CurrentStation = stop4,
            //    ///CurrentLines set in the bl layer 
            //};

            //StationWithRoutes RStat5 = new StationWithRoutes()
            //{
            //    CurrentStation = stop5,
            //    ///CurrentLines set in the bl layer 
            //};



            //#endregion
            ////write and implement check for StationsWithRoutes
        }

    }
}


