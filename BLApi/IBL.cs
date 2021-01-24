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
        string AddNewRoute(int region, string routeNum, IEnumerable<BusStations> stationList); // create for adding a route from add window
        void AddStationToBusRoute(BusRoute broute, LineStation line); //create
        BusRoute GetBusRoute(BusRoute broute); //retrieve
        BusRoute GetBusRouteID(string id); //retrieve using id
        void UpdateBusRoute(BusRoute broute); //update
        void DeleteFromBusRoute(BusRoute broute, LineStation station); //delete

        #endregion

        #region Buses
        IEnumerable<Buses> GetAllBuses();
        void AddBus(string license, DateTime reg, DateTime maint, int mil, int fuel);
        void SetBus(Buses bs, string license, DateTime reg, DateTime maint, int mil, int fuel);

        void statusToAvailable(Buses b);
        void busRefuel(Buses b);
        void busMaintenance(Buses b);
        bool needMaintenance(Buses b);
        void updateTravel(Buses b, int distance); 
        int getFuel(Buses b);

        void busTravel(Buses b);
        #endregion

        #region Stations
        IEnumerable<BusStations> GetAllBusStops();
        string GetBusStationsCode(BusStations bs);
        void AddBusStations(BusStations s);
        void SetBusStop(BusStations s, string code, string name, string addr, float longi, float lat);

        #endregion

        #region ScheduleOfRoute
        void AddScheduleOfRoute(ScheduleOfRoute sched); //create
        ScheduleOfRoute GetScheduleOfRoute(BusRoute route); //retrieve
        IEnumerable<ScheduleOfRoute> GetAllScheduleOfRoutes();
        void UpdateScheduleOfRoute(ScheduleOfRoute sched); //update
        void DeleteScheduleOfRoute(ScheduleOfRoute sched); //delete
        #endregion

        #region Routes Through a Specific Station 
        IEnumerable<BusRoute> GetRoutesofStation(BusStations station); //retrieve
        #endregion

        #region UserPortal
        bool UserSearch(string username, string pass);
        IEnumerable<UserPortal> GetAllUsers();
        #endregion

        #region Admins
        AdminPortal GetAdmin(string adminname, string pass);
        #endregion

        #region AdminPortal
        IEnumerable<AdminPortal> GetAllAdmin();
        #endregion
    }
}
