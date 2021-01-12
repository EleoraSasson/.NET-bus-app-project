using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALApi;
using DO;
using BO;

namespace BLApi
{
    class BLImp1 : IBL
    {
        #region CRUD BusFleet
        IDAL dal = DLFactory.GetDL();
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

        public IEnumerable<BusFleet> GetBusFleetWithSelectedFields(Func<BusFleet, object> generate)
        {
            throw new NotImplementedException();
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
        //void IBL.AddScheduleOfRoute(BusRoute s, Staff staff)
        //{
        //    ScheduleOfRoute schedule = new ScheduleOfRoute();
        //    schedule.CurrentRoute = s;
        //    schedule.SelectedStaff = staff;
        //    try { Add(bus); }
        //    catch { throw new BusAlreadyInSystemException(bus.BusLicense, "Bus Already found in Bus Fleet"); }
        //}

        //void IBL.DeleteScheduleOfRoute(ScheduleOfRoute s)
        //{
        //    throw new NotImplementedException();
        //}


        //IEnumerable<ScheduleOfRoute> IBL.GetAllSchedulesOfRoute()
        //{
        //    throw new NotImplementedException();
        //}


        //ScheduleOfRoute IBL.GetScheduleOfRoute()
        //{
        //    throw new NotImplementedException();
        //}

        //void IBL.UpdateScheduleOfRoute(ScheduleOfRoute s, Staff staff)
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<ScheduleOfRoute> IBL.GetStationsInBusRouteWithSelectedFields(Func<ScheduleOfRoute, object> generate)
        //{
        //    throw new NotImplementedException();
        //}


        #endregion

        #region CRUD CompanySchedule

        void IBL.AddCompanySchedule(BusRoute b, Staff staff)
        {
            ScheduleOfRoute schedule = new ScheduleOfRoute();
            schedule.CurrentRoute = b;   
            schedule.SelectedStaff = staff;
            try { AddSchedule(schedule); }
            catch { throw new CompanyScheduleAlreadyInSystemException(b.Route.BusLineNo, "Bus route schedule already found in the system."); }
        }

        void IBL.DeleteCompanySchedule(BusRoute b, Staff staff)
        {
            ScheduleOfRoute schedule = new ScheduleOfRoute();
            schedule.CurrentRoute = b;
            schedule.SelectedStaff = staff;
            try {deleteSchedule(schedule); }
            catch { throw new CompanyScheduleMissingFromSystemException(b.Route.BusLineNo, "Bus route schedule cannot be found in the system."); }
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
            ScheduleOfRoute schedule = new ScheduleOfRoute();
            schedule.CurrentRoute = b;
            schedule.SelectedStaff = staff;
            try { UpdateSchedule(schedule); }
            catch { throw new CompanyScheduleMissingFromSystemException(b.Route.BusLineNo, "Bus route schedule cannot be found in the system."); }
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



        void IBL.AddStationToBusRoute(LineStation line)
        {
            throw new NotImplementedException();
        }

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
}
