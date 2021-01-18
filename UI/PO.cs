using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BLApi;

namespace PO
{
    /// <summary>
    /// PL object - Routes, a list of BO.BusRoute
    /// </summary>
    class Routes
    {
        private List<BO.BusRoute> routeList;

        public List<BO.BusRoute> RouteList
        {
            get { return routeList; }
            set { routeList = value; }
        }
          
        public override string ToString()
        {
            return base.ToString(); 
        }

    }
    /// <summary>
    /// PL object - FLeet, a list of BO.BusFleet
    /// </summary>
    class Fleet
    {

        private List<BO.BusFleet> fleetList;

        public List<BO.BusFleet> FleetList
        {
            get { return fleetList; }
            set { fleetList = value; }
        }

        public override string ToString()
        {
            return base.ToString(); 
        }
    }
    /// <summary>
    /// PL object - Stations, a list of BO.SationWithRoutes
    /// </summary>
    class Stations
    {
        private List<BO.StationWithRoutes> stationsList;

        public List<BO.StationWithRoutes> StationsList
        {
            get { return stationsList; }
            set { stationsList = value; }
        }

        public override string ToString()
        {
            return base.ToString(); 
        }
    }
    /// <summary>
    /// PL object - CompanySchedule, a list of BO.ScheuleOfRoute
    /// </summary>
    class CompanySchedule
    {
        private List<BO.ScheduleOfRoute> routeSchedule;

        public List<BO.ScheduleOfRoute> RoutesSchedules
        {
            get { return routeSchedule; }
            set { routeSchedule = value; }
        }

        public override string ToString()
        {
            return base.ToString(); 
        }
    }
    /// <summary>
    /// PL object - Admin, a link to the BO.AdminPortal class
    /// </summary>
    class Admin
    {
        private BO.AdminPortal adminList;

        public BO.AdminPortal AdminList
        {
            get { return adminList; }
            set { adminList = value; }
        }

        public override string ToString()
        {
            return base.ToString(); 
        }
    }
    /// <summary>
    /// PL object - Users, a link to the BO.UserPortal class
    /// </summary>
    class Users
    {
        private BO.UserPortal userList;

        public BO.UserPortal UserList
        {
            get { return userList; }
            set { userList = value; }
        }

        public override string ToString()
        {
            return base.ToString(); 
        }
    }





}
