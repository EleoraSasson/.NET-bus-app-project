using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALApi;
using DO;
using BO;
using DS; 

namespace BLApi
{
    //adding try and catch to all qualities NOte there is a bl exception class
    public class BLImp : IBL
    {

        #region Singleton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }
        BLImp() { } //dafualt => private
        public static BLImp Instance { get => instance; }
        #endregion

        IDAL dal = DLFactory.GetDL();
        #region BusRoutes
        //create
        public string AddBusRoute(BO.BusRoute broute)
        {
            var RouteID = dal.AddBusLine(broute.Route);

            foreach ( var lineS in broute.RouteStops)
            { 
                int stationCount = dal.AddLineStation(lineS, RouteID);
                if (stationCount == 1) //it is the first station 
                {
                    broute.Route.BusStart = lineS.stationCode;
                    broute.Route.BusEnd = lineS.stationCode;
                }
                else // this is not the first station to be added
                {
                    broute.Route.BusEnd = lineS.stationCode; //but it is currently the last station
                }
            }

            dal.UpdateBusLine(broute.Route); //update busLine so that it has the corrent starting and end stations

            return RouteID; //returned RouteID so can add this route to a schedule
        }
        public void AddStationToBusRoute(BO.BusRoute broute, DO.LineStation station)
        {
           int stationCount = dal.AddLineStation(station, broute.Route.BusLineID);
            if (stationCount == 1) //it is the first station 
            {
                broute.Route.BusStart = station.stationCode;
                broute.Route.BusEnd = station.stationCode;
            }
            else // this is not the first station to be added
            {
                broute.Route.BusEnd = station.stationCode; //but it is currently the last station
            }
            dal.UpdateBusLine(broute.Route); //update busLine so that it has the corrent starting and end stations
        }
        //retrieve
        public BusRoute GetBusRoute(BusRoute bRoute)
        {
            BusRoute broute = new BusRoute();//create an instance of BusRoute
            broute.Route = dal.GetBusLine(bRoute.Route.BusLineID); //get the BusLine with that ID and place in route
            IEnumerable<LineStation> stations = dal.GetAllLineStations(); //get all stations
            broute.RouteStops = (from st in stations
                                   where st.lineID == bRoute.Route.BusLineID
                                 select st); //select all the LineStations that have that ID and place in routeStops
            return broute;
        }

        public IEnumerable<Stations> GetAllStationsInBusRoute(BusRoute broute)
        {
            List<Stations> listStations = new List<Stations>();
            foreach (var st in dal.GetAllLineStations())
            {
                if (st.lineID == broute.Route.BusLineID.ToString())
                {
                    Stations station = new Stations();
                    station.StatStation = st;
                    listStations.Add(station);
                }
            }

            return listStations;
        }

        public IEnumerable<BusRoute> GetAllBusRoutes()
        {
            List<BusRoute> routeList = new List<BusRoute>();
            IEnumerable<BusLine> lines = dal.GetAllBusLines();
            IEnumerable<LineStation> stations = dal.GetAllLineStations();

            foreach (var line in lines)
            {
                BusRoute bRoute = new BusRoute();
                bRoute.Route = line;
                bRoute.RouteStops = (from ls in stations
                                            where line.BusLineID.ToString() == ls.lineID
                                            select ls);
                routeList.Add(bRoute);
            }

            return routeList;
        }

        //update
        public void UpdateBusRoute(BO.BusRoute broute)
        {
            dal.UpdateBusLine(broute.Route);

            foreach (var lineS in broute.RouteStops)
            {
                dal.UpdateLineStation(broute.Route.BusLineID + lineS.stationCode);
            }
        }
        //delete
        public void DeleteFromBusRoute(BO.BusRoute broute, DO.LineStation station)
        {
            foreach (var lineS in broute.RouteStops)
            {
                if ((station.lineID + station.stationCode) == (lineS.lineID + lineS.stationCode))
                {
                    dal.DeleteLineStation(lineS.lineID + lineS.stationCode);
                    UpdateBusRoute(broute);//update line
                }
                else throw new MissingLineStationException(lineS.lineID + lineS.stationCode, $"Line Station {lineS.lineID + lineS.stationCode} cannot be deleted as it is missing from the system");
            }
        }

        #endregion

        #region Stations
        // public void AddBusStation(string code, )

        public string GetBusStationsCode(BusStations bs)
        {
            List<BusStop> bus = dal.GetAllBusStops().ToList();
            foreach (BusStop b in bus)
            {
                if (b.StopCode == bs.Stop.StopCode)
                {
                    return b.StopCode;
                }
            }
            throw new  BusStationNotInSystem(bs.Stop.StopCode, $"Station {bs.Stop.StopCode} does not exist.");
        }

        public IEnumerable<BusStations> GetAllBusStops()
        {
            List<BusStations> stationList = new List<BusStations>();
            IEnumerable<BusStop> stops = dal.GetAllBusStops();

            foreach (var stopp in stops)
            {
                BusStations bs = new BusStations();
                bs.Stop = new BusStop();
                bs.Stop.StopName = stopp.StopName; 
                bs.Stop.StopLocation = stopp.StopLocation;
                bs.Stop.StopCode = stopp.StopCode;
                bs.Stop.StopAddress = stopp.StopAddress;
                bs.Stop.StopActive = stopp.StopActive;
                stationList.Add(bs);
            }

            return stationList;
        }
        #endregion

        #region Making Busses Accessable in PO layer
        public IEnumerable<Buses> GetAllBuses()
        {
            List<Buses> busList = new List<Buses>();
            IEnumerable<Bus> bus = dal.GetAllBus();

            foreach (var buss in bus)
            {
                Buses bs = new Buses();
                bs.bus = new Bus();
                bs.bus.BusLicense = buss.BusLicense;
                bs.bus.BusFuel = buss.BusFuel;
                bs.bus.BusErased = buss.BusErased;
                bs.bus.BusMaintenanceDate = buss.BusMaintenanceDate;
                bs.bus.BusMileage = buss.BusMileage;
                bs.bus.BusRegDate = buss.BusRegDate;
                bs.bus.BusStatus = buss.BusStatus;

                busList.Add(bs);
            }

            return busList;
        }
        #endregion

        #region Getting all the Routes that visit a Given Station
        //NOTE: This is a retireival method which returns a list of BusRoutes that go through a given BusStation
        public IEnumerable<BusRoute> GetRoutesofStation(BusStations station)
        {
            List<BusRoute> routesOfStation = new List<BusRoute>();
            
            string code = station.Stop.StopCode;

            List<LineStation> listStations = (from ls in dal.GetAllLineStations()
                                             where station.Stop.StopCode == ls.stationCode
                                             select ls).ToList(); //find all the lineStations that have this station Code.

            List<BusRoute> listRoutes = GetAllBusRoutes().ToList(); //get all the BusRoutes

            foreach (LineStation lineS in listStations) 
            {
                BusRoute route = listRoutes.Find(r => r.Route.BusLineID == lineS.lineID); //find all the routes that correspond to those LineStations
                routesOfStation.Add(route); //add the found route
            }

            return routesOfStation;
        }
        #endregion

        #region ScheduleOfRoute
        //create
        public void AddScheduleOfRoute(ScheduleOfRoute sched)
        {
            string lineID = AddBusRoute(sched.CurrentRoute);
            string staffID = dal.AddStaff(sched.SelectedStaff);
            dal.AddLineLeaving(sched.RouteSchedule, lineID, staffID);
            dal.AddBusOnTrip(sched.BusOnRoute,lineID);
        }
        //retrieve
        public ScheduleOfRoute GetScheduleOfRoute(BusRoute route)
        {
            string lineID = route.Route.BusLineID;
            ScheduleOfRoute sched = new ScheduleOfRoute();

            List<BusRoute> broutes = GetAllBusRoutes().ToList();
            sched.CurrentRoute = broutes.Find(r => r.Route.BusLineID == lineID); //setting route
            List<BusOnTrip> busesOnTrip = dal.GetAllBusOnTrip().ToList();
            sched.BusOnRoute = busesOnTrip.Find(b => b.BusLineID == lineID); //setting bus on route
            sched.RouteSchedule = dal.GetLineLeaving(lineID); //setting schedule of route
            sched.SelectedStaff = dal.GetStaff(sched.RouteSchedule.BusDriver); //setting driver for route

            return sched;
        }
        //update
        public void UpdateScheduleOfRoute(ScheduleOfRoute sched)
        { 
            UpdateBusRoute(sched.CurrentRoute); //update route
            dal.UpdateBusOnTrip(sched.BusOnRoute); //update bus
            dal.UpdateLineLeaving(sched.RouteSchedule); //update schedule
            dal.UpdateStaff(sched.SelectedStaff.BusDriverID); //update staff of trip
        }
        //delete
        public void DeleteScheduleOfRoute(ScheduleOfRoute sched)
        {
            dal.DeleteLineLeaving(sched.RouteSchedule.BusLineID, sched.RouteSchedule.BusFirstLine);
        }
        #endregion

        #region UserPortal

        public IEnumerable<UserPortal> GetAllUsers()
        {
            List<UserPortal> userList = new List<UserPortal>();
            IEnumerable<User> users = dal.GetAllUsers();

            foreach (var user in users)
            {
                UserPortal up = new UserPortal();
                up.Users = new User();
                up.Users.adminPermission = user.adminPermission;
                up.Users.userName = user.userName;
                up.Users.userPassword = user.userPassword;
                up.Users.userFirst = user.userFirst;
                up.Users.userLast = user.userLast;
                up.Users.userId = user.userId;
                userList.Add(up);
            }

            return userList;
        }

        #endregion
    }
}
