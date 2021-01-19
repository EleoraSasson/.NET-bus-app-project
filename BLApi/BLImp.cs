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
        public void AddBusRoute(BO.BusRoute broute)
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
        public BusRoute GetBusRoute(BusRoute route)
        {
            BusRoute broute = new BusRoute();//create an instance of BusRoute
            broute.Route = dal.GetBusLine(route.Route.BusLineID); //get the BusLine with that ID and place in route
            IEnumerable<LineStation> stations = dal.GetAllLineStations(); //get all stations
            broute.RouteStops = (from st in stations
                                   where st.lineID == route.Route.BusLineID.ToString()
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

        public IEnumerable<ScheduleOfRoute> GetStationsInBusRouteWithSelectedFields(Func<ScheduleOfRoute, object> generate)
        {
            throw new NotImplementedException();
        }
        public void DeleteScheduleOfRoute(ScheduleOfRoute s)
        {
            throw new NotImplementedException();
        }
        public void AddScheduleOfRoute(ScheduleOfRoute s, Staff staff)
        {
            throw new NotImplementedException();
        }
        public ScheduleOfRoute GetScheduleOfRoute()
        {
            throw new NotImplementedException();
        }
        public void UpdateScheduleOfRoute(ScheduleOfRoute s, Staff staff)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ScheduleOfRoute> GetAllSchedulesOfRoute()
        {
            throw new NotImplementedException();
        }


     

        


        public IEnumerable<CompanySchedule> GetAllCompanySchedules()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CompanySchedule> GetCompanySchedulesWithSelectedFields(Func<CompanySchedule, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddCompanySchedule(BusRoute b, Staff s)
        {
            throw new NotImplementedException();
        }

        public CompanySchedule GetCompanySchedule()
        {
            throw new NotImplementedException();
        }

        public void UpdateCompanySchedule(BusRoute b, Staff ss)
        {
            throw new NotImplementedException();
        }

        public void DeleteCompanySchedule(BusRoute b, Staff s)
        {
            throw new NotImplementedException();
        }

      


    }

    //    #region CRUD BusFleet
    //    public void AddToBusFleet(Bus bus)
    //    {
    //        bus.BusStatus = Status.Available; //every new bus added to the fleet is automatically available for travel.
    //        bus.BusFuel = 1200; //every new bus added to fleet is considered to have a full tank of fuel.
    //        try { dal.AddBus(bus); }
    //        catch { throw new BusAlreadyInSystemException(bus.BusLicense, "Bus Already found in Bus Fleet"); }
    //    }

    //    public void DeleteFromBusFleet(Bus bus)
    //    {
    //        try { dal.DeleteBus(bus.BusLicense); }
    //        catch { throw new BusMissingFromSystemException(bus.BusLicense, "Bus cannot be found in the BUs Fleet"); }

    //    }

    //    public IEnumerable<BusFleet> GetBusFleetWithSelectedFields(Predicate<BusFleet> predicate)
    //    {
    //        return from Bus in dal.GetBusListWithSelectedFields(predicate);


    //        //return from bus in busRoot.Elements()
    //        //       let b = new Bus()
    //        //       {
    //        //           BusLicense = bus.Element("license").Value,
    //        //           BusRegDate = DateTime.Parse(bus.Element("regDate").Value),
    //        //           BusMaintenanceDate = DateTime.Parse(bus.Element("maintenanceDate").Value),
    //        //           BusMileage = int.Parse(bus.Element("mileage").Value),
    //        //           BusFuel = int.Parse(bus.Element("fuel").Value),
    //        //           BusStatus = (Status)Enum.Parse(typeof(Status), bus.Element("status").Value),
    //        //           BusErased = bool.Parse(bus.Element("isErased").Value),
    //        //       }
    //        //       where predicate(b)
    //        //       select b;
    //    }

    //    public IEnumerable<BusFleet> GetEntireBusFleet()
    //    {
    //        return from Bus in dal.GetAllBuses()
    //               select BusFleet; //?
    //    }

    //    public BusFleet GetFleet() //???
    //    {
    //        var fleet = dal.GetAllBuses();
    //        return fleet as BusFleet;
    //    }

    //    public void UpdateBusFleet(Bus bus)
    //    {
    //        try { dal.UpdateBus(bus); }
    //        catch { throw new BusMissingFromSystemException(bus.BusLicense, "Bus cannot be found in the Bus Fleet"); }
    //    }

    //    //public IEnumerable<BO.Course> GetAllCourses()
    //    //{
    //    //    return from crsDO in dl.GetAllCourses()
    //    //           select courseDoBoAdapter(crsDO);
    //    //}
    //    #endregion

    //    #region ScheduleOfRoute
    //    public void AddScheduleOfRoute(BusRoute s, Staff staff)
    //    {
    //        //ScheduleOfRoute schedule = new ScheduleOfRoute();
    //        //schedule.CurrentRoute = s;
    //        //schedule.SelectedStaff = staff;
    //        //try { Add(bus); }
    //        //catch { throw new BusAlreadyInSystemException(bus.BusLicense, "Bus Already found in Bus Fleet"); }
    //    }

    //    public void DeleteScheduleOfRoute(ScheduleOfRoute s)
    //    {
    //        throw new NotImplementedException();
    //    }


    //    public IEnumerable<ScheduleOfRoute> IBL.GetAllSchedulesOfRoute()
    //    {
    //        throw new NotImplementedException();
    //    }


    //    public ScheduleOfRoute GetScheduleOfRoute()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void UpdateScheduleOfRoute(ScheduleOfRoute s, Staff staff)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IEnumerable<ScheduleOfRoute> IBL.GetStationsInBusRouteWithSelectedFields(Func<ScheduleOfRoute, object> generate)
    //    {
    //        throw new NotImplementedException();
    //    }


    //    #endregion

    //    #region CRUD CompanySchedule

    //    public void AddCompanySchedule(ScheduleOfRoute s)
    //    {
    //        try
    //        {
    //            addBusRoute(s.CurrentRoute);
    //            dal.AddStaff(s.SelectedStaff);
    //        }
    //        //try { AddScheduleOfRoute(schedule); }
    //        catch { throw new CompanyScheduleAlreadyInSystemException(s.CurrentRoute.Route.BusLineNo, "Bus route schedule already found in the system."); }
    //    }

    //    private void AddScheduleOfRoute(ScheduleOfRoute schedule)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void DeleteCompanySchedule(ScheduleOfRoute s)
    //    {
    //        try 
    //        {
    //            deleteBusRoute(s.CurrentRoute);
    //            dal.DeleteStaff(s.SelectedStaff.BusDriverID);
    //        }
    //        catch { throw new CompanyScheduleMissingFromSystemException(s.CurrentRoute.Route.BusLineNo, "Bus route schedule cannot be found in the system."); }
    //    }

    //    IEnumerable<CompanySchedule> IBL.GetAllCompanySchedules()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    CompanySchedule IBL.GetCompanySchedule()
    //    {
    //        var schedule = ScheduleOfRoute.GetAllSchedule();
    //        return schedule;
    //    }

    //    IEnumerable<CompanySchedule> IBL.GetCompanySchedulesWithSelectedFields(Func<CompanySchedule, object> generate)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    void IBL.UpdateCompanySchedule(BusRoute b, Staff staff)
    //    {
    //        //ScheduleOfRoute schedule = new ScheduleOfRoute();
    //        //schedule.CurrentRoute = b;
    //        //schedule.SelectedStaff = staff;
    //        //try { UpdateSchedule(schedule); }
    //        //catch { throw new CompanyScheduleMissingFromSystemException(b.Route.BusLineNo, "Bus route schedule cannot be found in the system."); }
    //        try { dal.UpdateBus(b.)}
    //        else throw new DO.StaffNotInSystemException(staff.BusDriverID, $"Staff member {staff.BusDriverID} is not listed in the system");
    //    }

    //    #endregion


    //    #region CRUD StationInRoute

    //    void IBL.AddsStationInRoute(BusStop s)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    void IBL.DeleteStationInRoute(BusStop s)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    IEnumerable<StationWithRoutes> IBL.GetAllStationsInRoute()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    StationWithRoutes IBL.GetStationInRoute()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    IEnumerable<StationWithRoutes> IBL.GetStationsInRouteWithSelectedFields(Func<StationWithRoutes, object> generate)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    void IBL.UpdateStationInRoute(BusStop s)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion



    //    void IBL.AddStationToBusRoute(LineStation line)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    void IBL.AddToBusFleet(Bus bus)
    //    {
    //        throw new NotImplementedException();
    //    }


    //    void IBL.DeleteFromBusFleet(Bus bus)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    void IBL.DeleteFromBusRoute(LineStation line)
    //    {
    //        throw new NotImplementedException();
    //    }





    //    IEnumerable<BusRoute> IBL.GetAllStationsInBusRoute()
    //    {
    //        throw new NotImplementedException();
    //    }


    //    IEnumerable<BusFleet> IBL.GetBusFleetWithSelectedFields(Func<BusFleet, object> generate)
    //    {
    //        throw new NotImplementedException();
    //    }




    //    IEnumerable<BusFleet> IBL.GetEntireBusFleet()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    BusFleet IBL.GetFleet()
    //    {
    //        throw new NotImplementedException();
    //    }




    //    BusRoute IBL.GetStationsInBusRoute()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    IEnumerable<BusRoute> IBL.GetStationsInBusRouteWithSelectedFields(Func<BusRoute, object> generate)
    //    {
    //        throw new NotImplementedException();
    //    }




    //    void IBL.UpdateBusFleet(Bus bus)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    void IBL.UpdateBusRoute(LineStation line)
    //    {
    //        throw new NotImplementedException();
    //    }







    //}

    //public class dal
    //{
    //    class Busline
    //    {
    //        string lineID;
    //        int lineNO;
    //    }

    //    class LineStation
    //    {
    //        string lineID;
    //        string code;
    //    }
    //    class LineLeaving
    //    {
    //        string lineID;
    //        int numofStops;
    //    }
    //    class Staff
    //    {
    //        string name;
    //        int id;
    //    }

    //}

    //public class bl : dal
    //{
    //    class BusRoute
    //    {
    //        BusLine route;
    //        IEnumerable<LineStation> routeStops;
    //    }

    //    class RouteSchedule
    //    {
    //        BusRoute currentRoute;
    //        LineLeaving currentTimes;
    //        Staff currentStaff;
    //    }

    //    class CompanySchedule
    //    {
    //        IEnumerable<RouteSchedule> schedules;
    //    }
    //}

    //public interface ibl
    //{
    //    public void AddBusLine();



        

    //}

    //public interface idal
    //{

    //}

    //public class implementation
    //{ 
    
    
    
    //}
}
