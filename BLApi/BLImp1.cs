﻿using System;
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
    class BLImp1 : IBL
    {

        #region Singleton
        static readonly BLImp1 instance = new BLImp1();
        static BLImp1() { }
        BLImp1() { } //dafualt => private
        public static BLImp1 Instance { get => instance; }
        #endregion

        IDAL dal = DLFactory.GetDL();

        #region CRUD BusFleet
        public void AddToBusFleet(Bus bus)
        {
            bus.BusStatus = Status.Available; //every new bus added to the fleet is automatically available for travel.
            bus.BusFuel = 1200; //every new bus added to fleet is considered to have a full tank of fuel.
            try { dal.AddBus(bus); }
            catch { throw new BusAlreadyInSystemException(bus.BusLicense, "Bus Already found in Bus Fleet"); }
        }

        public void DeleteFromBusFleet(Bus bus)
        {
            try { dal.DeleteBus(bus.BusLicense); }
            catch { throw new BusMissingFromSystemException(bus.BusLicense, "Bus cannot be found in the BUs Fleet"); }

        }

        public IEnumerable<BusFleet> GetBusFleetWithSelectedFields(Predicate<BusFleet> predicate)
        {
            return from Bus in dal.GetBusListWithSelectedFields(predicate);


            //return from bus in busRoot.Elements()
            //       let b = new Bus()
            //       {
            //           BusLicense = bus.Element("license").Value,
            //           BusRegDate = DateTime.Parse(bus.Element("regDate").Value),
            //           BusMaintenanceDate = DateTime.Parse(bus.Element("maintenanceDate").Value),
            //           BusMileage = int.Parse(bus.Element("mileage").Value),
            //           BusFuel = int.Parse(bus.Element("fuel").Value),
            //           BusStatus = (Status)Enum.Parse(typeof(Status), bus.Element("status").Value),
            //           BusErased = bool.Parse(bus.Element("isErased").Value),
            //       }
            //       where predicate(b)
            //       select b;
        }

        public IEnumerable<BusFleet> GetEntireBusFleet()
        {
            return from Bus in dal.GetAllBuses()
                   select BusFleet; //?
        }

        public BusFleet GetFleet() //???
        {
            var fleet = dal.GetAllBuses();
            return fleet as BusFleet;
        }

        public void UpdateBusFleet(Bus bus)
        {
            try { dal.UpdateBus(bus); }
            catch { throw new BusMissingFromSystemException(bus.BusLicense, "Bus cannot be found in the Bus Fleet"); }
        }

        //public IEnumerable<BO.Course> GetAllCourses()
        //{
        //    return from crsDO in dl.GetAllCourses()
        //           select courseDoBoAdapter(crsDO);
        //}
        #endregion

        #region ScheduleOfRoute
        public void AddScheduleOfRoute(BusRoute s, Staff staff)
        {
            //ScheduleOfRoute schedule = new ScheduleOfRoute();
            //schedule.CurrentRoute = s;
            //schedule.SelectedStaff = staff;
            //try { Add(bus); }
            //catch { throw new BusAlreadyInSystemException(bus.BusLicense, "Bus Already found in Bus Fleet"); }
        }

        public void DeleteScheduleOfRoute(ScheduleOfRoute s)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ScheduleOfRoute> GetAllSchedulesOfRoute()
        {
            throw new NotImplementedException();
            //return new List<ScheduleOfRoute>();
        }


        public ScheduleOfRoute GetScheduleOfRoute()
        {
            throw new NotImplementedException();
        }

        public void UpdateScheduleOfRoute(ScheduleOfRoute s, Staff staff)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ScheduleOfRoute> IBL.GetStationsInBusRouteWithSelectedFields(Func<ScheduleOfRoute, object> generate)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region CRUD CompanySchedule

        public void AddCompanySchedule(ScheduleOfRoute s)
        {
            try
            {
               // addBusRoute(s.CurrentRoute);
                dal.AddStaff(s.SelectedStaff);
            }
            //try { AddScheduleOfRoute(schedule); }
            catch { throw new CompanyScheduleAlreadyInSystemException(s.CurrentRoute.Route.BusLineNo, "Bus route schedule already found in the system."); }
        }

        private void AddScheduleOfRoute(ScheduleOfRoute schedule)
        {
            throw new NotImplementedException();
        }

        public void DeleteCompanySchedule(ScheduleOfRoute s)
        {
            try 
            {
                deleteBusRoute(s.CurrentRoute);
                dal.DeleteStaff(s.SelectedStaff.BusDriverID);
            }
            catch { throw new CompanyScheduleMissingFromSystemException(s.CurrentRoute.Route.BusLineNo, "Bus route schedule cannot be found in the system."); }
        }

        IEnumerable<CompanySchedule> IBL.GetAllCompanySchedules()
        {
            throw new NotImplementedException();
        }

        CompanySchedule IBL.GetCompanySchedule()
        {
            var schedule = ScheduleOfRoute.GetAllSchedule();
            return schedule;
        }

        IEnumerable<CompanySchedule> IBL.GetCompanySchedulesWithSelectedFields(Func<CompanySchedule, object> generate)
        {
            throw new NotImplementedException();
        }

        void IBL.UpdateCompanySchedule(BusRoute b, Staff staff)
        {
            //ScheduleOfRoute schedule = new ScheduleOfRoute();
            //schedule.CurrentRoute = b;
            //schedule.SelectedStaff = staff;
            //try { UpdateSchedule(schedule); }
            //catch { throw new CompanyScheduleMissingFromSystemException(b.Route.BusLineNo, "Bus route schedule cannot be found in the system."); }
            try { dal.UpdateBus(b.)}
            else throw new DO.StaffNotInSystemException(staff.BusDriverID, $"Staff member {staff.BusDriverID} is not listed in the system");
        }

        #endregion


        #region CRUD StationInRoute

        void IBL.AddsStationInRoute(BusStop s)
        {
            throw new NotImplementedException();
        }

        void IBL.DeleteStationInRoute(BusStop s)
        {
            throw new NotImplementedException();
        }

        IEnumerable<StationWithRoutes> IBL.GetAllStationsInRoute()
        {
            throw new NotImplementedException();
        }

        StationWithRoutes IBL.GetStationInRoute()
        {
            throw new NotImplementedException();
        }

        IEnumerable<StationWithRoutes> IBL.GetStationsInRouteWithSelectedFields(Func<StationWithRoutes, object> generate)
        {
            throw new NotImplementedException();
        }

        void IBL.UpdateStationInRoute(BusStop s)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region CRUD BusRoute
        void AddBusRoute(BO.BusRoute bRoute)
        {

            int idLine = dal.AddBusLine(bRoute.Route);   
            //loop or select to go through list of BusStations inside of BusRoute, 
            throw new NotImplementedException();
        }

        #endregion
        void IBL.AddToBusFleet(Bus bus)
        {
            throw new NotImplementedException();
        }


        void IBL.DeleteFromBusFleet(Bus bus)
        {
            throw new NotImplementedException();
        }

        void IBL.DeleteFromBusRoute(LineStation line)
        {
            throw new NotImplementedException();
        }





        IEnumerable<BusRoute> IBL.GetAllStationsInBusRoute()
        {
            throw new NotImplementedException();
        }


        IEnumerable<BusFleet> IBL.GetBusFleetWithSelectedFields(Func<BusFleet, object> generate)
        {
            throw new NotImplementedException();
        }




        IEnumerable<BusFleet> IBL.GetEntireBusFleet()
        {
            throw new NotImplementedException();
        }

        BusFleet IBL.GetFleet()
        {
            throw new NotImplementedException();
        }




        BusRoute IBL.GetStationsInBusRoute()
        {
            throw new NotImplementedException();
        }

        IEnumerable<BusRoute> IBL.GetStationsInBusRouteWithSelectedFields(Func<BusRoute, object> generate)
        {
            throw new NotImplementedException();
        }




        void IBL.UpdateBusFleet(Bus bus)
        {
            throw new NotImplementedException();
        }

        void IBL.UpdateBusRoute(LineStation line)
        {
            throw new NotImplementedException();
        }







    }

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
