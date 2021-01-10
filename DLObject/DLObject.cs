﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DALApi; //referance to DALApi interface
using DO; //reference to Data Object
using DLObject;
using DS;
//using DO might need to access some things from the interface

namespace DAL 
{
    public class DLObject : IDAL
    {
        static Random rnd = new Random();
        /// <summary>
        /// Implementing the Singelton Design Method so that only one instance of each collection will be created
        /// This ensures that all the data is in one place and that we do not have any extra copies of collections
        /// </summary>
        #region Singleton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }
        DLObject() { }
        public static DLObject Instance { get => instance; }
        #endregion

        LineStation lineStation;
        Staff staff;
        SuccessiveStations successiveStations;
        User user;
        UserTrip userTrip;

        #region CRUD Implementation - Bus

        /// <summary>
        /// AddBus: adds a bus to the collection of buses
        /// parameter: Bus
        /// return type: void
        /// </summary>
        public void AddBus(DO.Bus bus)
        {
            if (DataSource.busList.FirstOrDefault(b => b.BusLicense == bus.BusLicense) != null)
                throw new DO.InvalidBusLicenseException(bus.BusLicense, "Duplicate bus license number");
            DataSource.busList.Add(bus.Clone());
        }

        /// <summary>
        /// GetBus: gets a bus from the collection of buses 
        /// parameter: string (bus license)
        /// return type: Bus
        /// </summary>
        public Bus GetBus(string license)
        {
            DO.Bus bus = DataSource.busList.Find(b => b.BusLicense == license); //define list bus

            if (bus != null)
                return bus.Clone();
            else
                throw new DO.InvalidBusLicenseException(license, $"Bus {license} does not exist.");
        }

        /// <summary>
        /// GetAllBuses: gets all buses from the collection   
        /// parameter: none
        /// return type: IEnumerable
        /// </summary>
        public IEnumerable<Bus> GetAllBuses()
        {
            return from Bus in DataSource.busList

                   select Bus.Clone(); 
                    
        }

        public IEnumerable<object> GetBusListWithSelectedFields(Func<Bus, object> generate)
        {
            return from bus in DataSource.busList
                   select generate(bus);
            //var object = Bus.Items
            //     .Select(c => c.EmployeeFields)     // Select the fields per employee
            //     .SelectMany(fields => fields)      // Flatten to a single sequence of fields
            //     .OfType<EmployeeID>()              // Filter to only EmployeeID fields
            //     .Select(id => id.Item)             // Convert to strings
            //     .ToList();                         // Materialize as a list
        }

        /// <summary>
        /// UpdateBus: updates a bus from the collection of buses 
        /// parameter: Bus
        /// return type: void
        /// </summary>
        public void UpdateBus (Bus buss) //(string license, Action<Bus> update)
        {
            DO.Bus bus = DataSource.busList.Find(b => b.BusLicense == buss.BusLicense);

            if (bus != null)
            {
                DataSource.busList.Remove(bus);
                DataSource.busList.Add(bus.Clone());
            }
            else
                throw new DO.InvalidBusLicenseException(buss.BusLicense, $"bus: {buss.BusLicense} does not exist.");
        }

        /// <summary>
        /// DeleteBus: deletes a bus from the collection of buses 
        /// parameter: string (bus license)
        /// return type: void
        /// </summary>
        public void DeleteBus(string license)
        {
            DO.Bus bus = DataSource.busList.Find(b => b.BusLicense == license);

            if (bus != null)
            {
                DataSource.busList.Remove(bus);
            }
            else
                throw new DO.InvalidBusLicenseException(license, $"bus: {license} does not exist");
        }
        #endregion

        #region CRUD Implementation - BusLine

        /// <summary>
        /// GetAllBusLines: gets all bus lines from the collection  
        /// parameter: none
        /// return type: IEnumerable
        /// </summary>
        public IEnumerable<BusLine> GetAllBusLines()
        {
            return from BusLine in DataSource.busLineList
                   select BusLine.Clone();
        }

        public IEnumerable<object> GetBusLineWithSelectedFields(Func<BusLine, object> generate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// AddBusLine: adds a bus line to the collection of buses
        /// parameter: Bus Line
        /// return type: void
        /// </summary>
        public void AddBusLine(BusLine busLine)
        {//add asigning of running number
            if (DataSource.busLineList.FirstOrDefault(b => b.BusLineID == busLine.BusLineID) != null)
                throw new DO.InvalidBusLineException(busLine.BusLineID.ToString(), "Duplicate bus license number");
            DataSource.busLineList.Add(busLine.Clone());
        }

        /// <summary>
        /// GetBusLine: gets a bus line from the collection of bus lines 
        /// parameter: int (line ID)
        /// return type: BusLine
        /// </summary>
        public BusLine GetBusLine(int lineID)
        {
            DO.BusLine line = DataSource.busLineList.Find(b => b.BusLineID == lineID); //define list bus

            if (line != null)
                return line.Clone();
            else
                throw new DO.InvalidBusLineException(lineID.ToString(), $"Bus {lineID} does not exist."); 
        }

        /// <summary>
        /// UpdateBusLine: updates a bus line from the collection of bus lines
        /// parameter: BusLine
        /// return type: void
        /// </summary>
        public void UpdateBusLine (BusLine busline)
        {
            DO.BusLine line = DataSource.busLineList.Find(b => b.BusLineID == busline.BusLineID);

            if (line != null)
            {
                DataSource.busLineList.Remove(line);
                DataSource.busLineList.Add(line.Clone());
            }
            else
                throw new DO.InvalidBusLineException(busline.BusLineID.ToString(), $"Bus {busline.BusLineID}"); 
        }

        /// <summary>
        /// DeleteBusLine: deletes a bus line from the collection of bus lines 
        /// parameter: int (line ID)
        /// return type: void
        /// </summary>
        public void DeleteBusLine(int lineID)
        {
            DO.BusLine line = DataSource.busLineList.Find(b => b.BusLineID == lineID);

            if (line != null)
            {
                DataSource.busLineList.Remove(line);
            }
            else
                throw new DO.InvalidBusLineException(lineID.ToString(), $"Bus {lineID} does not exist.");
        }

        #endregion

        #region CRUD Implementation - BusesOnTrip

        /// <summary>
        /// GetAllBusOnTrip: gets all bus on trip from the collection  
        /// parameter: none
        /// return type: IEnumerable
        /// </summary>
        public IEnumerable<BusOnTrip> GetAllBusesOnTrip()
        {
            return from BusOnTrip in DataSource.busOnTripList
                   select BusOnTrip.Clone();
        }

        public IEnumerable<object> GetBusOnTripWithSelectedFields(Func<BusOnTrip, object> generate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// AddBusOnTrip: adds a bus on trip to the collection of buses on trip
        /// parameter: BusOnTrip
        /// return type: void
        /// </summary>
        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            if (DataSource.busOnTripList.FirstOrDefault(b => b.BusRoadID == busOnTrip.BusRoadID) != null)
                throw new DO.InvalidBusOnTripIDException(busOnTrip.BusRoadID.ToString(), "Duplicate bus on trip ID");
            DataSource.busOnTripList.Add(busOnTrip.Clone());
        }

        /// <summary>
        /// GetBusOnTrip: gets a bus on trip from the collection of buses on trip 
        /// parameter: int (road ID)
        /// return type: BusOnTrip
        /// </summary>
        public BusOnTrip GetBusOnTrip(int roadID)
        {
            DO.BusOnTrip trip = DataSource.busOnTripList.Find(b => b.BusRoadID == roadID); //define list bus

            if (trip != null)
                return trip.Clone();
            else
                throw new DO.InvalidBusOnTripIDException(roadID.ToString(), $"Bus on trip ID: {roadID} does not exist.");
        }

        /// <summary>
        /// UpdateBusOnTrip: updates a bus on trip from the collection of buses on trip
        /// parameter: BusOnTrip
        /// return type: void
        /// </summary>
        public void UpdateBusOnTrip(BusOnTrip bus)
        {
            DO.BusOnTrip trip = DataSource.busOnTripList.Find(b => b.BusRoadID == bus.BusRoadID);

            if (trip != null)
            {
                DataSource.busOnTripList.Remove(trip);
                DataSource.busOnTripList.Add(trip.Clone());
            }
            else
                throw new DO.InvalidBusOnTripIDException(bus.BusRoadID.ToString(), $"Bus on trip ID: {bus.BusRoadID} does not exist.");
        }

        /// <summary>
        /// DeleteBusOnTrip: deletes a bus on trip from the collection of buses on trip 
        /// parameter: int (int road ID)
        /// return type: void
        /// </summary>
        public void DeleteBusOnTrip(int roadID)
        {
            DO.BusOnTrip trip = DataSource.busOnTripList.Find(b => b.BusRoadID == roadID);

            if (trip != null)
            {
                DataSource.busOnTripList.Remove(trip);
            }
            else
                throw new DO.InvalidBusOnTripIDException(roadID.ToString(), $"Bus on trip ID: {roadID} does not exist.");
        }
        #endregion

        #region CRUD Implementation - BusStop 

        /// <summary>
        /// GetAllBusStops: gets all bus stops from the collection  
        /// parameter: none
        /// return type: IEnumerable
        /// </summary>
        public IEnumerable<BusStop> GetAllBusStops()
        {
            return from BusStop in DataSource.busStopList
                   select BusStop.Clone();
        }

        public IEnumerable<object> GetBusStopWithSelectedFields(Func<BusOnTrip, object> generate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// AddBusStop: adds a bus stop to the collection of bus stops
        /// parameter: BusStop
        /// return type: void
        /// </summary>
        public void AddBusStop(BusStop busStop)
        {
            if (DataSource.busStopList.FirstOrDefault(b => b.StopCode == busStop.StopCode) != null)
                throw new DO.InvalidStopCodeException(busStop.StopCode.ToString(), "Duplicate bus stop code");
            DataSource.busStopList.Add(busStop.Clone());
        }

        /// <summary>
        /// GetBusStop: gets a bus stop from the collection of bus stops 
        /// parameter: int (stop code)
        /// return type: BusLine
        /// </summary>
        public BusStop GetBusStop(int stopCode)
        {
            DO.BusStop stop = DataSource.busStopList.Find(b => b.StopCode == stopCode); //define list bus

            if (stop != null)
                return stop.Clone();
            else
                throw new DO.InvalidStopCodeException(stopCode.ToString(), $"Bus stop {stopCode} does not exist.");
        }

        /// <summary>
        /// UpdateBusStop: updates a bus stop from the collection of bus stops
        /// parameter: BusStop
        /// return type: void
        /// </summary>
        public void UpdateBusStop(BusStop bstop)
        {
            DO.BusStop stop = DataSource.busStopList.Find(b => b.StopCode == bstop.StopCode);

            if (stop != null)
            {
                DataSource.busStopList.Remove(stop);
                DataSource.busStopList.Add(stop.Clone());
            }
            else
                throw new DO.InvalidStopCodeException(bstop.StopCode.ToString(), $"Bus stop: {bstop.StopCode} does not exist.");
        }

        /// <summary>
        /// DeleteBusStop: deletes a bus stop from the collection of bus stops 
        /// parameter: int (stop code)
        /// return type: void
        /// </summary>
        public void DeleteBusStop(int stopCode)
        {
            DO.BusStop stop = DataSource.busStopList.Find(b => b.StopCode == stopCode);

            if (stop != null)
            {
                DataSource.busStopList.Remove(stop);
            }
            else
                throw new DO.InvalidStopCodeException(stopCode.ToString(), $"Bus stop {stopCode} does not exist.");
        }
        #endregion

        #region CRUD Implementation - LineLeaving

        /// <summary>
        /// GetAllLinesLeaving: gets all lines leaving from the collection  
        /// parameter: none
        /// return type: IEnumerable
        /// </summary>
        public IEnumerable<LineLeaving> GetAllLinesLeaving()
        {
            return from LineLeaving in DataSource.lineLeavingList
                   select LineLeaving.Clone();
        }

        public IEnumerable<object> GetLineLeavingWithSelectedFields(Func<LineLeaving, object> generate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// AddLineLeaving: adds a line leaving to the collection of lines leaving
        /// parameter: LineLeaving
        /// return type: void
        /// </summary>
        public void AddLineLeaving(LineLeaving lineLeaving)
        {
            string key = lineLeaving.BusLineID + lineLeaving.BusFirstLine.ToString();
            if (DataSource.lineLeavingList.FirstOrDefault(b => b.BusLineID + b.BusFirstLine.ToString() == key) != null)
                throw new DO.InvalidStopCodeException(key, "Duplicate line leaving.");
            DataSource.lineLeavingList.Add(lineLeaving.Clone());
        }

        /// <summary>
        /// GetLineLeaving: gets a line leaving from the collection of lines leaving 
        /// parameter: int, TimeSpan (line ID, starting time)
        /// return type: LineLeaving
        /// </summary>
        public LineLeaving GetLineLeaving(int lineID, TimeSpan startTime)
        {
            string key = lineID.ToString() + startTime.ToString();
            DO.LineLeaving line = DataSource.lineLeavingList.Find(b => b.BusLineID + b.BusFirstLine.ToString() == key); //define list bus

            if (line != null)
                return line.Clone();
            else
                throw new DO.InvalidLineLeavingKeyException(key, $"Line leaving {key} does not exist.");
        }

        /// <summary>
        /// UpdateLineLeaving: updates a line leaving from the collection of lines leaving
        /// parameter: LineLeaving
        /// return type: void
        /// </summary>
        public void UpdateLineLeaving(LineLeaving lineLeaving)
        {
            string key = lineLeaving.BusLineID + lineLeaving.BusFirstLine.ToString();
            DO.LineLeaving line = DataSource.lineLeavingList.Find(b => b.BusLineID + b.BusFirstLine.ToString() == key);

            if (line != null)
            {
                DataSource.lineLeavingList.Remove(line);
                DataSource.lineLeavingList.Add(line.Clone());
            }
            else
                throw new DO.InvalidLineLeavingKeyException(key, $"Line leaving {key} does not exist.");
        }

        /// <summary>
        /// DeleteLineLeaving: deletes a line leaving from the collection of lines leaving 
        /// parameter: int, TimeSpan (line ID, starting time)
        /// return type: void
        /// </summary>
        public void DeleteLineLeaving(int lineID, TimeSpan startTime)
        {
            string key = lineID.ToString() + startTime.ToString();
            DO.LineLeaving line = DataSource.lineLeavingList.Find(b => b.BusLineID + b.BusFirstLine.ToString() == key);

            if (line != null)
            {
                DataSource.lineLeavingList.Remove(line);
            }
            else
                throw new DO.InvalidLineLeavingKeyException(key, $"Line leaving {key} does not exist.");
        }

        #endregion

        #region CRUD Implementation - LineStation

        /// <summary>
        /// GetAllLinesStations: gets all line stations leaving from the collection  
        /// parameter: none
        /// return type: IEnumerable
        /// </summary>
        public IEnumerable<LineStation> GetAllLineStations()
        {
            return from LineStation in DataSource.lineStationList
                   select LineStation.Clone();
        }

        public IEnumerable<object> GetLineStationWithSelectedFields(Func<LineStation, object> generate)
        {
            return from LineStation in DataSource.lineStationList
                   select generate(GetLineStation(lineStation.lineID)); //check
        }

        /// <summary>
        /// AddLineStation: adds a line station to the collection of line stations
        /// parameter: LineLeaving
        /// return type: void
        /// </summary>
        public void AddLineStation(LineStation lineStation)
        {
            var entityKey = lineStation.lineID + lineStation.stationCode;
            //check if line exsists
            if ((DataSource.lineStationList.FirstOrDefault(line => line.lineID == lineStation.lineID) != null))
            {
                //if line exsists see if it holds the same station
                if ((DataSource.lineStationList.FirstOrDefault(stations => stations.stationCode == lineStation.stationCode) != null))
                {
                    throw new DO.ExsistingLineStationException(entityKey, "Duplicate lineStation"); 
                }
            } //it is a new LineSation so can add to collection:
            DataSource.lineStationList.Add(lineStation.Clone());
        }

        /// <summary>
        /// GetLineStation: gets a line station from the collection of line station 
        /// parameter: string (line station key)
        /// return type: LineStation
        /// </summary>
        public LineStation GetLineStation(string lineStationKey)
        {
            DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.lineID == lineStationKey);

            if (findLineStation != null)
            {
                return lineStation.Clone();
            }
            else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }

        /// <summary>
        /// UpdateLinesStation: updates a line leaving from the collection of lines leaving
        /// parameter: string (line station key)                                                are you sure?
        /// return type: void
        /// </summary>
        public void UpdateLineStation(string lineStationKey)
        {
            DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.lineID == lineStationKey);

            if (findLineStation != null)
            {
                DataSource.lineStationList.Remove(findLineStation);
                DataSource.lineStationList.Add(findLineStation.Clone());
            }
            else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }

        /// <summary>
        /// DeleteLineStation: deletes a line station from the collection of line stations 
        /// parameter:  string (line station key)  
        /// return type: void
        /// </summary>
        public void DeleteLineStation(string lineStationKey)
        {
            DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.lineID == lineStationKey);

            if (findLineStation != null)
            {
                DataSource.lineStationList.Remove(findLineStation);
            }
            else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }
        #endregion

        #region CRUD Implementation - Staff

        /// <summary>
        /// GetAllStaff: gets all staff from the collection  
        /// parameter: none
        /// return type: IEnumerable
        /// </summary>
        public IEnumerable<Staff> GetAllStaff()
        {
            return from Staff in DataSource.staffList
                   select staff.Clone();
        }

        public IEnumerable<object> GetStaffWithSelectedFields(Func<Staff, object> generate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// AddStaff: adds a staff member to the collection of staff
        /// parameter: Staff
        /// return type: void
        /// </summary>
        public void AddStaff(Staff staff)
        {
            //check if staff member exsists already
            if ((DataSource.staffList.FirstOrDefault(st => st.BusDriverID == staff.BusDriverID) != null))
            {
                throw new DO.StaffAlreadyInSystemException(staff.BusDriverID, $"Staff member {staff.BusDriverID} is already listed in the system");
            } //it is a new staff member so can add to list:
            DataSource.staffList.Add(staff.Clone());
        }

        /// <summary>
        /// GetStaff: gets a staff member from the collection of staff 
        /// parameter: string (staff ID)
        /// return type: Staff
        /// </summary>
        public Staff GetStaff(string staffID)
        {
            DO.Staff findStaff = DataSource.staffList.Find(member => member.BusDriverID == staffID);

            if (findStaff != null)
            {
                return staff.Clone();
            }
            else throw new DO.StaffNotInSystemException(staff.BusDriverID, $"Staff member {staff.BusDriverID} is not listed in the system");
        }

        /// <summary>
        /// UpdateLinesStation: updates a staff member from the collection of staff
        /// parameter: string (staff ID)                                               
        /// return type: void
        /// </summary>
        public void UpdateStaff(string staffID/*, Action<Staff> update*/)
        {
            DO.Staff findStaff = DataSource.staffList.Find(member => member.BusDriverID == staffID);

            if (findStaff != null)
            {
                DataSource.staffList.Remove(findStaff);
                DataSource.staffList.Add(findStaff.Clone());
            }
            else throw new DO.StaffNotInSystemException(staff.BusDriverID, $"Staff member {staff.BusDriverID} is not listed in the system");
        }

        /// <summary>
        /// DeleteStaff: deletes a staff member from the collection of staff members
        /// parameter:  string (staff ID)  
        /// return type: void
        /// </summary>
        public void DeleteStaff(string staffID)
        {
            DO.Staff findStaff = DataSource.staffList.Find(member => member.BusDriverID == staffID);
            // check if staff member exsists
            if (findStaff != null)
            {
                DataSource.staffList.Remove(findStaff);
            }
            else throw new DO.StaffNotInSystemException(staff.BusDriverID, $"Staff member {staff.BusDriverID} is not listed in the system");

        }
        #endregion

        #region CRUD Implementation - SuccessiveStations

        /// <summary>
        /// GetAllSuccessiveStations: gets all successive stations from the collection  
        /// parameter: none
        /// return type: IEnumerable
        /// </summary>
        public IEnumerable<SuccessiveStations> GetAllSuccessiveStations()
        {
            return from SuccessiveStations in DataSource.succStationsList
                   select SuccessiveStations.Clone();
        }

        public IEnumerable<object> GetSuccessiveStationsWithSelectedFields(Func<SuccessiveStations, object> generate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// AddSuccessiveStations: adds successive station to the collection of successive stations
        /// parameter: SuccessiveStations
        /// return type: void
        /// </summary>
        public void AddSuccessiveStations(SuccessiveStations successiveStations)
        {
            //check if station 1 exists
            if ((DataSource.succStationsList.FirstOrDefault(station => station.StationCode1 == successiveStations.StationCode1) != null))
            {
                //if station 1 exsists see if it connects to station 2
                if ((DataSource.succStationsList.FirstOrDefault(station => station.StationCode2 == successiveStations.StationCode2) != null))
                {
                    throw new DO.MissingSuccessiveStationsException(successiveStations.StationCode1.ToString(), "Duplicate Successive Station");
                }
            } //it is a new LineSation so can add to collection:
            DataSource.succStationsList.Add(successiveStations.Clone());
        }

        /// <summary>
        /// GetSuccessiveStations: gets successive station to the collection of successive stations
        /// parameter: string (entity key)
        /// return type: SuccessiveStations
        /// </summary>
        public SuccessiveStations GetSuccessiveStations(int stat1, int stat2)
        {
            var entityKey = stat1.ToString() + stat2.ToString();
            DO.SuccessiveStations findStations = DataSource.succStationsList.Find(stat => stat.StationCode1.ToString() == entityKey);
            if (findStations != null)
            {
                return successiveStations.Clone();
            }
            else throw new DO.MissingSuccessiveStationsException(entityKey, $"No data found for Successive Station : {entityKey}");
        }

        /// <summary>
        /// UpdateSuccessiveStations: updates successive station to the collection of successive stations
        /// parameter: string (entity key)                                               
        /// return type: void
        /// </summary>
        public void UpdateSuccessiveStations(string entityKey)
        {

            DO.SuccessiveStations findStations = DataSource.succStationsList.Find(stat => stat.StationCode1.ToString() == entityKey);
            if (findStations != null)
            {
                DataSource.succStationsList.Remove(findStations);
                DataSource.succStationsList.Add(findStations.Clone());
            }
            else throw new DO.MissingSuccessiveStationsException(entityKey, $"No data found for Successive Station : {entityKey}");
        }

        /// <summary>
        /// DeleteSuccessiveStations: deletes successive station to the collection of successive stations
        /// parameter:  string (entity key)  
        /// return type: void
        /// </summary>
        public void DeleteSuccessiveStations(string entityKey)
        {

            DO.SuccessiveStations findStations = DataSource.succStationsList.Find(stat => stat.StationCode1.ToString() == entityKey);
            if (findStations != null)
            {
                DataSource.succStationsList.Remove(findStations);
            }
            else throw new DO.MissingSuccessiveStationsException(entityKey, $"No data found for Successive Station : {entityKey}");
        }
        #endregion

        #region CRUD Implementation - User

        /// <summary>
        /// GetAllUsers: gets all users from the collection  
        /// parameter: none
        /// return type: IEnumerable
        /// </summary>
        public IEnumerable<User> GetAllUsers()
        {
            return from User in DataSource.usersList
                   select User.Clone();
        }

        public IEnumerable<object> GetUserWithSelectedFields(Func<SuccessiveStations, object> generate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// AddUser: adds a user to the collection of users
        /// parameter: User
        /// return type: void
        /// </summary>
        public void AddUser(User user)
        {
            if ((DataSource.usersList.FirstOrDefault(use => use.userName == user.userName)!= null))
            {
                throw new DO.ExsistingUserException(user.userName, "Duplicate user");
            }

            DataSource.usersList.Add(user.Clone());
        }

        /// <summary>
        /// GetUser: gets a user from the collection of users
        /// parameter: string (name)
        /// return type: User
        /// </summary>
        public User GetUser(string name)
        {
            DO.User findUser = DataSource.usersList.Find(u => u.userName == name);

            if (findUser != null)
            {
                return user.Clone();
            }
            else throw new DO.MissingUserException(name, $"No data found for user: {name}");
        }

        /// <summary>
        /// UpdateUser: updates a user from the collection of users
        /// parameter: string (name)                                               
        /// return type: void
        /// </summary>
        public void UpdateUser(string name)
        {
            DO.User findUser = DataSource.usersList.Find(u => u.userName == name);

            if (findUser != null)
            {
                DataSource.usersList.Remove(findUser);
                DataSource.usersList.Add(findUser.Clone());
            }
            else throw new DO.MissingUserException(name, $"No data found for user: {name}");
        }

        /// <summary>
        /// DeleteUser: deletes a user from the collection of users
        /// parameter:  string (name)  
        /// return type: void
        /// </summary>
        public void DeleteUser(string name)
        {

            DO.User findUser = DataSource.usersList.Find(u => u.userName == name);

            if (findUser != null)
            {
                DataSource.usersList.Remove(findUser);
            }
            else throw new DO.MissingUserException(name, $"No data found for user: {name}");
        }
        #endregion

        #region CRUD Implementation - UserTrip

        /// <summary>
        /// GetAllUsers: gets all users from the collection  
        /// parameter: none
        /// return type: IEnumerable
        /// </summary>
        public IEnumerable<UserTrip> GetAllUserTrip()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetUserTripWithSelectedFields(Func<UserTrip, object> generate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// AddUserTrip: adds a user trip to the collection of user trips
        /// parameter: UserTrip
        /// return type: void
        /// </summary>
        public void AddUserTrip(UserTrip userTrip)
        {
            DO.UserTrip findTrip = DataSource.userTripList.Find(utrip => utrip.userTravelID == userTrip.userTravelID);
            if (findTrip != null)
            {
                throw new DO.ExsistingUserTripException(userTrip.userTravelID.ToString(), "Duplicate UserTrip");
            }

            DataSource.userTripList.Add(findTrip.Clone());
        }

        /// <summary>
        /// GetUserTrip: gets a user trip from the collection of user trips
        /// parameter: int (travel ID)
        /// return type: UserTrip
        /// </summary>
        public UserTrip GetUserTrip(int travelID)
        {
                DO.UserTrip findTrip = DataSource.userTripList.Find(utrip => utrip.userTravelID == travelID);
            if (findTrip != null)
            {
                return userTrip.Clone();
            }
            else throw new DO.MissingUserTripException(travelID.ToString(), $"No data found for UserTrip: {travelID}");
        }

        /// <summary>
        /// UpdateUserTrip: updates a user trip from the collection of user trips
        /// parameter: int (travel ID)                                              
        /// return type: void
        /// </summary>
        public void UpdateUserTrip(int travelID)
        {
            DO.UserTrip findTrip = DataSource.userTripList.Find(utrip => utrip.userTravelID == travelID);
            if (findTrip != null)
            {
                DataSource.userTripList.Remove(findTrip);
                DataSource.userTripList.Add(findTrip.Clone());
            }
            else throw new DO.MissingUserTripException(travelID.ToString(), $"No data found for UserTrip: {travelID}");
        }

        /// <summary>
        /// DeleteUserTrip: deletes a user trip from the collection of user trips
        /// parameter:  int (travel ID)  
        /// return type: void
        /// </summary>
        public void DeleteUserTrip(int travelID)
        {
            DO.UserTrip findTrip = DataSource.userTripList.Find(utrip => utrip.userTravelID == travelID);
            if (findTrip != null)
            {
                DataSource.userTripList.Remove(findTrip);
            }
            else throw new DO.MissingUserTripException(travelID.ToString(), $"No data found for UserTrip: {travelID}");
        }

        #endregion

    }
}
