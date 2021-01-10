using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DALApi;

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
           // busList.Add(new Bus());

            //Defines and a new list of BusLines and adds an object of type BusLine to the List:
            busLineList = new List<BusLine>();
            busLineList.Add(new BusLine());

            //Defines and a new list of BusOnTrip and adds an object of type BusOnTrip to the List:
            busOnTripList = new List<BusOnTrip>();
            busOnTripList.Add(new BusOnTrip());

            //Defines and a new list of BusStops and adds an object of type BusStop to the List:
            busStopList = new List<BusStop>();
            busStopList.Add(new BusStop());

            //Defines and a new list of LineLeaving and adds an object of type LineLeaving to the List:
            lineLeavingList = new List<LineLeaving>();
            lineLeavingList.Add(new LineLeaving());

            //Defines and a new list of LineStations and adds an object of type LineStation to the List:
            lineStationList = new List<LineStation>();
            lineStationList.Add(new LineStation());

            //Defines and a new list of Staff and adds an object of type Staff to the List:
            staffList = new List<Staff>();
            staffList.Add(new Staff());

            //Defines and a new list of SuccessiveStations and adds an object of type SuccessiceStations to the List:
            succStationsList = new List<SuccessiveStations>();
            succStationsList.Add(new SuccessiveStations());

            //Defines and a new list of User and adds an object of type User to the List:
            usersList = new List<User>();
            usersList.Add(new User());

            //Defines and a new list of UserTrip and adds an object of type UserTrip to the List:
            userTripList = new List<UserTrip>();
            userTripList.Add(new UserTrip());
        }
    }

}
