using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DALApi;
using System.Device.Location;

namespace DS
{
    public static class DataSource
    {
        public static List<Bus> busList;
        public static List<BusLine> busLineList;
        public static List<BusOnTrip> busOnTripList;
        public static List<BusStop> busStopList;
        public static List<LineLeaving> lineLeavingList;
        public static List<LineStation> lineStationList;
        public static List<Staff> staffList;
        public static List<SuccessiveStations> succStationsList;
        public static List<User> usersList;
        public static List<UserTrip> userTripList;
        static DataSource() //add aan init to start off witha basic bunch of buses
        {
            //Defines and a new list of Buses and adds an object of type Bus to the List:
            busList = new List<Bus>(); 
         

            //Defines and a new list of BusLines and adds an object of type BusLine to the List:
            busLineList = new List<BusLine>();

            //Defines and a new list of BusOnTrip and adds an object of type BusOnTrip to the List:
            busOnTripList = new List<BusOnTrip>();

            //Defines and a new list of BusStops and adds an object of type BusStop to the List:
            busStopList = new List<BusStop>();
            Random lat = new Random();
            Random lon = new Random();

            var randLat = lat.NextDouble() * (33.30 - 31.30) + 31.30;
            var randLong = lon.NextDouble() * (35.50 - 34.30) + 34.30;

            BusStop stop1 = new BusStop()
            {
                StopLocation = new GeoCoordinate() { Longitude = randLong, Latitude = randLat },
                StopCode = "11111",
                StopName = "Cherry - Sugar Cane",
                StopActive = true
             };

            BusStop stop2 = new BusStop()
            {
                StopLocation = new GeoCoordinate() { Longitude = randLong, Latitude = randLat },
                StopCode = "22222",
                StopName = "Strawberry - Cream",
                StopActive = true
            };
            BusStop stop3 = new BusStop()
            {
                StopLocation = new GeoCoordinate() { Longitude = randLong, Latitude = randLat },
                StopCode = "33333",
                StopName = "Peanut - Butter",
                StopActive = true
             };
                
            BusStop stop4 = new BusStop()
            {
                StopLocation = new GeoCoordinate() { Longitude = randLong, Latitude = randLat },
                StopCode = "44444",
                StopName = "Orange - Lemonade",
                StopActive = true
            };

            BusStop stop5 = new BusStop()
            {
                StopLocation = new GeoCoordinate() { Longitude = randLong, Latitude = randLat },
                StopCode = "55555",
                StopName = "Apricot-Litchi",
                StopActive = true
            };

            busStopList.Add(stop1);
            busStopList.Add(stop2);
            busStopList.Add(stop3);
            busStopList.Add(stop4);
            busStopList.Add(stop5);

			//Defines and a new list of LineLeaving and adds an object of type LineLeaving to the List:
			lineLeavingList = new List<LineLeaving>();

            //Defines and a new list of LineStations and adds an object of type LineStation to the List:
            lineStationList = new List<LineStation>();

            //Defines and a new list of Staff and adds an object of type Staff to the List:
            staffList = new List<Staff>();

            //Defines and a new list of SuccessiveStations and adds an object of type SuccessiceStations to the List:
            succStationsList = new List<SuccessiveStations>();

            //Defines and a new list of User and adds an object of type User to the List:
            usersList = new List<User>();

            //Defines and a new list of UserTrip and adds an object of type UserTrip to the List:
            userTripList = new List<UserTrip>();
        }
    }

}
