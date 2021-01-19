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
        BusFleet GetEntireBusFleet();
        void AddToBusFleet(Bus bus); //create
        Bus GetBusFromFleet(string license); //retrieve
        void UpdateBusFleet(Bus bus); //update
        void DeleteFromBusFleet(Bus bus); //delete
        #endregion

        #region BusRoute
        IEnumerable<Stations> GetAllStationsInBusRoute(BusRoute broute);
        IEnumerable<BusRoute> GetAllBusRoutes();
        void AddBusRoute(BusRoute broute);//create
        void AddStationToBusRoute(BusRoute broute, LineStation line); //create
        BusRoute GetBusRoute(BusRoute broute); //retrieve
        void UpdateBusRoute(BusRoute broute); //update
        void DeleteFromBusRoute(BusRoute broute,LineStation station); //delete

        #endregion

        //#region BusStation
        //BusStop getOneBusStop(BusStations busStations);
        //#endregion

        #region Stations
        IEnumerable<BusStations> getAllBusStops();
        string GetBusStationsCode(BusStations bs);
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
        //NOTE: there are no addition or deletion methods in this crud implementation for this class
        // this is because the purpose of this class is retrieval of information only. Updating is allowed only to update
        // the active status of the busStop.
        StationWithRoutes GetStationWithRoute(string stationCode); //retrieve
        void UpdateStationWithRoutes(StationWithRoutes station);//update
        #endregion
    }
}
