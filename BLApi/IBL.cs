using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;

namespace BLApi
{
    public interface IBL
    {
        /* IMPLEMENTING CRUD METHODS FOR BO ENTITIES */

        #region BusFleet
        IEnumerable<Bus> GetEntireBusFleet();
        void AddToBusFleet(Bus bus); //create
        Bus GetBusFromFleet(string license); //retrieve
        void UpdateBusFleet(Bus bus); //update
        void DeleteFromBusFleet(Bus bus); //delete
        #endregion

        #region BusRoute
        IEnumerable<BusRoute> GetAllStationsInBusRoute(); 
        IEnumerable<BusRoute> GetStationsInBusRouteWithSelectedFields(Func<BusRoute, object> generate);
        void AddBusRoute(BusRoute broute);//create
        void AddStationToBusRoute(BusRoute broute, LineStation line); //create
        BusRoute GetStationsInBusRoute(); //retrieve
        void UpdateBusRoute(BusRoute broute); //update
        void DeleteFromBusRoute(BusRoute broute,LineStation station); //delete

        #endregion

        #region ScheduleOfRoute
        IEnumerable<ScheduleOfRoute> GetAllSchedulesOfRoute();
        IEnumerable<ScheduleOfRoute> GetStationsInBusRouteWithSelectedFields(Func<ScheduleOfRoute, object> generate);
        void AddScheduleOfRoute(ScheduleOfRoute s, Staff staff); //create
        ScheduleOfRoute GetScheduleOfRoute(); //retrieve
        void UpdateScheduleOfRoute(ScheduleOfRoute s, Staff staff); //update
        void DeleteScheduleOfRoute(ScheduleOfRoute s); //delete
        #endregion

        #region CompanySchedule
        IEnumerable<CompanySchedule> GetAllCompanySchedules();
        IEnumerable<CompanySchedule> GetCompanySchedulesWithSelectedFields(Func<CompanySchedule, object> generate);
        void AddCompanySchedule(BusRoute b, Staff s); //create
        CompanySchedule GetCompanySchedule(); //retrieve
        void UpdateCompanySchedule(BusRoute b, Staff ss); //update
        void DeleteCompanySchedule(BusRoute b, Staff s); //delete
        #endregion

        #region StationWithRoutes
        IEnumerable<StationWithRoutes> GetAllStationsInRoute();
        IEnumerable<StationWithRoutes> GetStationsInRouteWithSelectedFields(Func<StationWithRoutes, object> generate);
        void AddsStationInRoute(BusStop s); //create
        StationWithRoutes GetStationInRoute(); //retrieve
        void UpdateStationInRoute(BusStop s); //update
        void DeleteStationInRoute(BusStop s); //delete
        #endregion

    }
}
