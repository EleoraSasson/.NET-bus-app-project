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
    public class BLImp : IBL
    {

        #region Singleton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }
        BLImp() { } //dafualt => private
        public static BLImp Instance { get => instance; }
        #endregion

        IDAL dal = DLFactory.GetDL();

        #region BusFleet
        //create
        public void AddToBusFleet(Bus bus)
        {
            bus.BusStatus = Status.Available; //every new bus added to the fleet is automatically available for travel.
            bus.BusFuel = 1200; //every new bus added to fleet is considered to have a full tank of fuel.
            try { dal.AddBus(bus); }
            catch { throw new BusAlreadyInSystemException(bus.BusLicense, "Bus Already found in Bus Fleet"); }
        }

        //retrieve
        public Bus GetBusFromFleet(string license)
        { 
            Bus bus  = dal.GetBus(license);
            if(bus != null)
                return bus;
            else throw new BusMissingFromSystemException(license, $"Bus {license} cannot be found");
        }

        public BusFleet GetEntireBusFleet()
        {
            BusFleet fleet = new BusFleet();
            fleet.busesInFleet = dal.GetAllBuses();
            return fleet;
        }
        //update
        public void UpdateBusFleet(Bus bus)
        {
            dal.UpdateBus(bus);
        }
        //delete
        public void DeleteFromBusFleet(Bus bus)
        {
            dal.DeleteBus(bus.BusLicense);
        }
        #endregion

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
        public BusRoute GetBusRoute(string lineID)
        {
            BusRoute broute = new BusRoute();//create an instance of BusRoute
            broute.Route = dal.GetBusLine(lineID); //get the BusLine with that ID and place in route
            IEnumerable<LineStation> stations = dal.GetAllLineStations(); //get all stations
            broute.RouteStops = (from st in stations
                                   where st.lineID == lineID
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
        //public IEnumerable<BusRoute> GetStationsInBusRouteWithSelectedFields(Func<BusRoute, object> generate)
        //{
        //    throw new NotImplementedException();
        //}

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

        public IEnumerable<BusStations> getAllBusStops()
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

        #region StationsWithRoutes
        //NOTE: there are no addition or deletion methods in this crud implementation for this class
        // this is because the purpose of this class is retrieval of information only. Updating is allowed only to update
        // the active status of the busStop.

        //retrieve 
        public StationWithRoutes GetStationWithRoute(string stationCode)
        {
            StationWithRoutes swr = new StationWithRoutes();
            List<BusRoute> broutes = GetAllBusRoutes().ToList();
            foreach (BusRoute b in broutes)
            {
                foreach (LineStation l in b.RouteStops)
                {
                    if (l.stationCode == stationCode)
                    {
                        swr.CurrentLines.Add(b);
                    }
                }
            }
            return swr;
        }
        //update:
        public void UpdateStationWithRoutes(StationWithRoutes station)
        {
            //only can update the active status ///check if already on route if already assigned toa route then cannot update. if not assigned no problem
            throw new NotImplementedException();
        }
        #endregion

        //#region BusStations

        //public BusStop getOneBusStop(BusStations stations)
        //{
        //    string code = stations.stop.StopCode;
        //    BusStop b = dal.GetBusStop(code);
        //    return b;
        //}

        //#endregion

        #region ScheduleOfRoute
        //create
        public void AddScheduleOfRoute(ScheduleOfRoute sched)
        {
            string lineID = AddBusRoute(sched.CurrentRoute);
            string staffID = dal.AddStaff(sched.SelectedStaff);
            dal.AddLineLeaving(sched.RouteSchedule, lineID, staffID);
        }
        //retrieve
        public ScheduleOfRoute GetScheduleOfRoute(string lineID)
        {
            ScheduleOfRoute sched = new ScheduleOfRoute();

            sched.CurrentRoute = GetBusRoute(lineID);
            sched.RouteSchedule = dal.GetLineLeaving(lineID);
            sched.SelectedStaff = dal.GetStaff(sched.RouteSchedule.BusDriver);
            return sched;
        }
        public IEnumerable<ScheduleOfRoute> GetAllSchedulesOfRoute()
        {
            throw new NotImplementedException();
        }
        //update
        public void UpdateScheduleOfRoute(ScheduleOfRoute sched)
        {
            throw new NotImplementedException();
        }
        //delete
        public void DeleteScheduleOfRoute(ScheduleOfRoute sched)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
