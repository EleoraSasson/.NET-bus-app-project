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

        

        #region BusRoute
        IEnumerable<Stations> GetAllStationsInBusRoute(BusRoute broute);
        IEnumerable<BusRoute> GetAllBusRoutes();
        string AddBusRoute(BusRoute broute);//create
        void AddStationToBusRoute(BusRoute broute, LineStation line); //create
        BusRoute GetBusRoute(BusRoute broute); //retrieve
        void UpdateBusRoute(BusRoute broute); //update
        void DeleteFromBusRoute(BusRoute broute,LineStation station); //delete

        #endregion

        #region Bus
        IEnumerable<Buses> GetAllBuses();
        #endregion

        #region Stations
        IEnumerable<BusStations> getAllBusStops();
        string GetBusStationsCode(BusStations bs);
        #endregion

        #region ScheduleOfRoute
        void AddScheduleOfRoute(ScheduleOfRoute sched); //create
        ScheduleOfRoute GetScheduleOfRoute(string lineID); //retrieve
        void UpdateScheduleOfRoute(ScheduleOfRoute sched); //update
        void DeleteScheduleOfRoute(ScheduleOfRoute sched); //delete
        #endregion

        #region Routes Through a Specific Station 
        IEnumerable<BusRoute> GetRoutesofStation(BusStations station); //retrieve
        #endregion
       
    }
}
