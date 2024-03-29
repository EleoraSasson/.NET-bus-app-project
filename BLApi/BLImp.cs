﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALApi;
using DO;
using BO;
using DS;
using System.Threading;

namespace BLApi
{
    class BLImp : IBL //internal class
    {

        #region Singleton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }
        BLImp() { } //dafualt => private
        public static BLImp Instance { get => instance; }
        #endregion

        IDAL dal = DLFactory.GetDL();

        #region Buses
        public void AddBus(string license, DateTime reg, DateTime maint, int mil, int fuel)
        {
            try
            {
                Buses bs = new Buses();
                Bus b = new Bus();
                b.BusLicense = license;
                b.BusFuel = fuel;
                b.BusMileage = mil;
                b.BusMaintenanceDate = maint;
                b.BusRegDate = reg;
                b.BusStatus = Status.Available;
                b.BusErased = false;
                bs.bus = b;
                dal.AddBus(b);
            }
            catch (DO.InvalidBusLicenseException ex)
            {
                throw new BO.BusExistsException("Bus already exists", ex);
            }

        }

        public void SetBus(Buses bs, string license, DateTime reg, DateTime maint, int mil, int fuel)
        {
            bs.bus = new Bus();
            bs.bus.BusLicense = license;
            bs.bus.BusFuel = fuel;
            bs.bus.BusMileage = mil;
            bs.bus.BusMaintenanceDate = maint;
            bs.bus.BusRegDate = reg;
            bs.bus.BusStatus = Status.Available;
            bs.bus.BusErased = false;

        }

        public void busRefuel(Buses b)
        {
            b.bus.BusStatus = Status.Refueling;
            b.bus.BusFuel = 1200;
        }

        public void statusToAvailable(Buses b)
        {
            b.bus.BusStatus = Status.Available;
        }
        public void busMaintenance(Buses b)
        {
            b.bus.BusStatus = Status.AtService;
            b.bus.BusFuel = 1200; //refuels
            DateTime current = DateTime.Today;
            b.bus.BusMaintenanceDate = current; //gives new date    //service date    
        }

        public int getFuel(Buses b)
        {
            return b.bus.BusFuel;
        }

        public bool needMaintenance(Buses b)
        {
            var today = DateTime.Now;
            var MaintenanceDate = b.bus.BusMaintenanceDate.AddYears(1); //adding a Year to last maintenance date

            var compareDates = today.CompareTo(MaintenanceDate);
            if (compareDates < 0)
            {
                //no maintenance needed as within a year
                return false;
            }
            else //a year or more has passed
            {
                return true; //needs maintenance
            }
        }
        public void updateTravel(Buses b, int distance)
        {
            b.bus.BusFuel -= distance;
            b.bus.BusMileage += distance;
        }

        public void busTravel(Buses b)
        {
            b.bus.BusStatus = Status.Traveling;
        }
        #endregion

        #region BusRoutes
        //create
        public string AddBusRoute(BO.BusRoute broute)
        {
            string RouteID;
            try { RouteID = dal.AddBusLine(broute.Route); }
            catch (DO.InvalidBusLineException ex)
            { throw new BO.BusLineAlreadyInSytemException("Bus line already exists", ex); }

            foreach (var lineS in broute.RouteStops)
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

            try { dal.UpdateBusLine(broute.Route); } //update busLine so that it has the corrent starting and end stations
            catch (DO.InvalidBusLineException ex)
            { throw new BO.BusLineNotInSystem("Bus line cannot be found", ex); }

            return RouteID; //returned RouteID so can add this route to a schedule
        }
        public void AddStationToBusRoute(BO.BusRoute broute, DO.LineStation station)
        {
            try
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
            }
            catch (DO.ExsistingLineStationException ex)
            { throw new BO.LineStationExistsException("Line station already exists", ex); }

            try { dal.UpdateBusLine(broute.Route); } //update busLine so that it has the corrent starting and end stations
            catch (DO.InvalidBusLineException ex)
            { throw new BO.BusLineMissingException("Bus line missing", ex); }
        }

        public string AddNewRoute(int region, string routeNum, IEnumerable<BusStations> stationList)
        {
            BusRoute newRoute = new BusRoute();
            BusLine line = new BusLine();
            line.BusLineNo = Int32.Parse(routeNum);
            int regionIndex = region + 1;
            line.BusRegion = (Regions)regionIndex;
            newRoute.Route = line;
            List<LineStation> lsList = new List<LineStation>();
            foreach (var stop in stationList)
            {
                LineStation lineStation = new LineStation();
                lineStation.stationCode = stop.Stop.StopCode;
                lsList.Add(lineStation);
            }
            newRoute.RouteStops = lsList;

            string routeID = AddBusRoute(newRoute);
            return routeID;
        }
        //retrieve
        public BusRoute GetBusRoute(BusRoute bRoute)
        {
            BusRoute broute = new BusRoute();//create an instance of BusRoute
            try
            {
                broute.Route = dal.GetBusLine(bRoute.Route.BusLineID); //get the BusLine with that ID and place in route
                IEnumerable<LineStation> stations = dal.GetAllLineStations(); //get all stations
                broute.RouteStops = (from st in stations
                                     where st.lineID == bRoute.Route.BusLineID
                                     select st); //select all the LineStations that have that ID and place in routeStops
            }
            catch (DO.InvalidBusLineException ex)
            { throw new BO.BusLineMissingException("Bus line cannot be found", ex); }
            return broute;
        }

        public BusRoute GetBusRouteID( string id)
        {
            BusRoute broute = new BusRoute();//create an instance of BusRoute
            try
            {
                broute.Route = dal.GetBusLine(id); //get the BusLine with that ID and place in route
                IEnumerable<LineStation> stations = dal.GetAllLineStations(); //get all stations
                broute.RouteStops = (from st in stations
                                     where st.lineID == id
                                     select st); //select all the LineStations that have that ID and place in routeStops
            }
            catch (DO.InvalidBusLineException ex)
            { throw new BO.BusLineMissingException("Bus line cannot be found", ex); }
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
            try { dal.UpdateBusLine(broute.Route); }
            catch (DO.InvalidBusLineException ex)
            { throw new BO.BusLineMissingException("Bus line cannot be found", ex); }

            foreach (var lineS in broute.RouteStops)
            {
                try { dal.UpdateLineStation(broute.Route.BusLineID + lineS.stationCode); }
                catch (DO.MissingLineStationException ex)
                { throw new BO.LineStationMissingException("Line station cannot be found", ex); }
            }
        }
        //delete
        public void DeleteFromBusRoute(BO.BusRoute broute, DO.LineStation station)
        {
            foreach (var lineS in broute.RouteStops)
            {
                if ((station.lineID + station.stationCode) == (lineS.lineID + lineS.stationCode))
                {
                    try { dal.DeleteLineStation(lineS.lineID + lineS.stationCode); }
                    catch (DO.MissingLineStationException ex)
                    { throw new LineStationMissingException("Line station does not exist", ex); }
                    UpdateBusRoute(broute);//update line
                }
                else throw new MissingLineStationException(lineS.lineID + lineS.stationCode, $"Line Station {lineS.lineID + lineS.stationCode} cannot be deleted as it is missing from the system");
            }
        }



        #endregion 

        #region Stations

        public void SetBusStop(BusStations s, string code, string name, string addr, float longi, float lat)
        {
            s.Stop = new BusStop();
            s.Stop.StopCode = code;
            s.Stop.StopAddress = addr;
            s.Stop.StopActive = true;
            s.Stop.StopLocation = new System.Device.Location.GeoCoordinate();
            s.Stop.StopLocation.Longitude = longi;
            s.Stop.StopLocation.Latitude = lat;
            s.Stop.StopName = name;
        }

        public void AddBusStations(BusStations s)
        {
            try
            {
                BusStations stat = new BusStations();
                BusStop bs = new BusStop();
                bs.StopActive = true;
                bs.StopAddress = s.Stop.StopAddress;
                bs.StopCode = s.Stop.StopCode;
                bs.StopName = s.Stop.StopName;
                bs.StopLocation = new System.Device.Location.GeoCoordinate();
                bs.StopLocation.Latitude = s.Stop.StopLocation.Latitude;
                bs.StopLocation.Longitude = s.Stop.StopLocation.Longitude;
                stat.Stop = bs;
                dal.AddBusStop(s.Stop);
            }
            catch (DO.MissingBusStopException)
            { throw new BO.BusStationNotInSystem("Bus stop already exists."); }
        }


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
            throw new BusStationNotInSystem(bs.Stop.StopCode, $"Station {bs.Stop.StopCode} does not exist.");
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
            try
            {
                dal.AddLineLeaving(sched.RouteSchedule, lineID, staffID);

            }
            catch (DO.InvalidLineLeavingKeyException ex)
            {
                throw new BO.LineLeavingExists("Line leaving already exists", ex);
            }

            int totalTravelTime = 0;

            for (int i = 0; i < sched.numOfStops; i++)
            {
                Random rand = new Random();
                var travelTime = rand.Next(5, 45); //we are guestinmating that the time between any given stops will be between 5 and 45 minutes
                Thread.Sleep(40); //allowing for our values to be random
                totalTravelTime += totalTravelTime; //add to total travel time
            }

            TimeSpan timeS = TimeSpan.FromMinutes(totalTravelTime);
            sched.BusOnRoute.BusArrivalT = sched.BusOnRoute.BusFormalDT + timeS;

            try
            {
                dal.AddBusOnTrip(sched.BusOnRoute, lineID);
            }
            catch (DO.InvalidBusOnTripIDException ex)
            {
                throw new BO.BusOnTripExists("Bus on trip already exists", ex);
            }
            sched.numOfStops = sched.CurrentRoute.RouteStops.Count();

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
            try
            {
                sched.RouteSchedule = dal.GetLineLeaving(lineID); //setting schedule of route
            }
            catch (DO.InvalidLineLeavingKeyException ex)
            {
                throw new BO.LineLeavingExists("Line leaving does not exist", ex);
            }
            try { sched.SelectedStaff = dal.GetStaff(sched.RouteSchedule.BusDriver); }
            catch (DO.InvalidBusOnTripIDException ex)
            {
                throw new BO.BusOnTripExists("Bus on trip does not exist", ex);
            }
            sched.numOfStops = sched.CurrentRoute.RouteStops.Count();
            return sched;
        }

        public IEnumerable<ScheduleOfRoute> GetAllScheduleOfRoutes()
        {
            List<ScheduleOfRoute> scheduleList = new List<ScheduleOfRoute>();
            IEnumerable<BusRoute> routes = GetAllBusRoutes();
            IEnumerable<LineLeaving> lines = dal.GetAllLinesLeaving();
            IEnumerable<BusOnTrip> buses = dal.GetAllBusOnTrip();
            IEnumerable<Staff> staffs = dal.GetAllStaff();

            foreach (var line in lines)
            {
                ScheduleOfRoute sroute = new ScheduleOfRoute();
                sroute.RouteSchedule = line;
                sroute.SelectedStaff = staffs.FirstOrDefault(s => s.BusDriverID == line.BusDriver);
                sroute.CurrentRoute = GetBusRouteID(line.BusLineID);
                sroute.BusOnRoute = buses.FirstOrDefault(b => b.BusLineID == line.BusLineID);
                sroute.numOfStops = sroute.CurrentRoute.RouteStops.Count();
                scheduleList.Add(sroute);
            }

            return scheduleList;
        }
        //update
        public void UpdateScheduleOfRoute(ScheduleOfRoute sched)
        { 
            UpdateBusRoute(sched.CurrentRoute); //update route
            try { dal.UpdateBusOnTrip(sched.BusOnRoute); } //update bus
            catch (DO.InvalidBusOnTripIDException ex)
            { throw new BO.BusOnTripExists("Bus on trip does not exist", ex); }

            try { dal.UpdateLineLeaving(sched.RouteSchedule); } //update schedule
            catch (DO.InvalidLineLeavingKeyException ex)
            { throw new BO.LineLeavingExists("Line leaving does not exist", ex); }

            try { dal.UpdateStaff(sched.SelectedStaff.BusDriverID); } //update staff of trip
            catch(DO.StaffNotInSystemException ex)
            { throw new BO.StaffMissing("Staff cannot be found", ex); }
        }
        //delete
        public void DeleteScheduleOfRoute(ScheduleOfRoute sched)
        {
            try { dal.DeleteLineLeaving(sched.RouteSchedule.BusLineID, sched.RouteSchedule.BusFirstLine); }
            catch (DO.InvalidLineLeavingKeyException ex)
            { throw new BO.LineLeavingExists("Line leaving does not exist", ex); }
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
                up.userPortal = new User();
                up.userPortal.adminPermission = user.adminPermission;
                up.userPortal.userName = user.userName;
                up.userPortal.userPassword = user.userPassword;
                up.userPortal.userFirst = user.userFirst;
                up.userPortal.userLast = user.userLast;
                up.userPortal.userId = user.userId;
                userList.Add(up);
            }

            return userList;
        }

        public void SetUser (UserPortal userp, string first, string last, string username, string password, string ID)
        {
            UserPortal up = new UserPortal(); 
            up.userPortal = new User();
            up.userPortal.userFirst = first;
            up.userPortal.userLast = last;
            up.userPortal.userName = username;
            up.userPortal.userPassword = password;
            up.userPortal.userId = ID;
            userp = up;
        }

        public void AddUser ( string first, string last, string username, string password, string ID)
        {
            try
            {
                UserPortal up = new UserPortal();
                User u = new User();
                u.userName = username;
                u.userLast = last;
                u.userFirst = first;
                u.userId = ID;
                u.userPassword = password;
                up.userPortal = u;
                dal.AddUser(u);
            }

            catch (DO.ExsistingUserException ex)
            {
                throw new BO.UserExistException("User already exists", ex);
            }
        }

        public void DeleteUser (UserPortal up)
        {
            try
            {
                User u = new User() { userFirst = up.userPortal.userFirst, userLast = up.userPortal.userLast, userName = up.userPortal.userName, userId = up.userPortal.userId, adminPermission = up.userPortal.adminPermission, userPassword = up.userPortal.userPassword };
                dal.DeleteUser(u.userName);
            }
            catch (DO.MissingUserException ex)
            {
                throw new BO.UserMissingExcpetion("User cannot be found", ex);
            }
        }

        public UserPortal GetUser(string username, string pass)
        {
            try
            {
                User user = dal.GetUser(username, pass);
                if (user != null)
                {

                    UserPortal u = new UserPortal();
                    User use = new User();
                    use.userName = username;
                    use.userPassword = pass;
                    u.userPortal = use;
                    return u;
                }
                else return null;
            }
            catch (DO.MissingUserException ex)
            {
                throw new BO.UserMissingExcpetion("User does not exist", ex);
            }
        }

        #endregion

        #region AdminPortal
        public IEnumerable<AdminPortal> GetAllAdmin()
        {
            List<AdminPortal> adminList = new List<AdminPortal>();
            IEnumerable<Admin> admin = dal.GetAllAdmin();

            foreach (var s in admin)
            {
                AdminPortal ap = new AdminPortal();
                Admin a = new Admin();
                a.adminPassword = s.adminPassword;
                a.adminName = s.adminName;
                ap.adminPortal = a;
               
                adminList.Add(ap);
            }

            return adminList;
        }
       
        public AdminPortal GetAdmin(string adminname, string pass)
        {
            try
            {
                Admin admin = dal.GetAdmin(adminname, pass);
                if (admin != null)
                {
                    AdminPortal a = new AdminPortal();
                    Admin ad = new Admin();
                    ad.adminName = adminname;
                    ad.adminPassword = pass;
                    a.adminPortal = ad;
                    return a;
                }
                else return null;
            }
            catch (DO.MissingUserException ex)
            { throw new BO.UserMissingExcpetion("Admin does not exist", ex); } 
           
        }
        #endregion

        #region EmployeePortal

        public IEnumerable<EmployeePortal> GetAllEmployees()
        {
            List<Staff> staffList = dal.GetAllStaff().ToList();
            List<EmployeePortal> employees = new List<EmployeePortal>();

            foreach (var staff in staffList)
            {
                EmployeePortal employee = new EmployeePortal();
                employee.Employee = new Staff();
                employee.Employee.BusDriverID = staff.BusDriverID;
                employee.Employee.BusDriverAge = staff.BusDriverAge;
                employee.Employee.BusDriverCellNo = staff.BusDriverCellNo;
                employee.Employee.BusDriverFirst = staff.BusDriverFirst;
                employee.Employee.BusDriverLast = staff.BusDriverLast;
                employee.Employee.StaffPosition= staff.StaffPosition;
                employee.Employee.StaffYrsWorked = staff.StaffYrsWorked;

                employees.Add(employee);
            }

            return employees;
        }

        #endregion
        
    }
}
