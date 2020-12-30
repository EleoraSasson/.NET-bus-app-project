using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DALApi; //referance to DALApi interface
using DS; //reference to Data Structure
using DO; //reference to Data Object
using DLObject;
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

        //Eleora
        #region CRUD Implementation - Bus

        public void AddBus(DO.Bus bus)
        {
            if (DataSource.busList.FirstOrDefault(b => b.BusLicense == bus.BusLicense) != null)
                throw new DO.InvalidBusLicenseException(bus.BusLicense, "Duplicate bus license number");
            DataSource.busList.Add(bus.Clone());
        }

        public Bus GetBus(string license)
        {
            DO.Bus bus = DataSource.busList.Find(b => b.BusLicense == license); //define list bus

            if (bus != null)
                return bus.Clone();
            else
                throw new DO.InvalidBusLicenseException(license, $"wrong bus license: {license}");
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            return from Bus in DataSource.busList
                   select Bus.Clone();
        }

        public IEnumerable<object> GetBusListWithSelectedFields(Func<Bus, object> generate)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus (Bus buss) //(string license, Action<Bus> update)
        {
            DO.Bus bus = DataSource.busList.Find(b => b.BusLicense == buss.BusLicense);

            if (bus != null)
            {
                DataSource.busList.Remove(bus);
                DataSource.busList.Add(bus.Clone());
            }
            else
                throw new DO.InvalidBusLicenseException(buss.BusLicense, $"wrong bus license: {buss.BusLicense}");
        }

        public void DeleteBus(string license)
        {
            DO.Bus bus = DataSource.busList.Find(b => b.BusLicense == license);

            if (bus != null)
            {
                DataSource.busList.Remove(bus);
            }
            else
                throw new DO.InvalidBusLicenseException(license, $"bad person id: {license}");
        }
        #endregion

        #region CRUD Implementation - BusLine

        public IEnumerable<BusLine> GetAllBusLines()
        {
            return from BusLine in DataSource.busLineList
                   select BusLine.Clone();
        }

        public IEnumerable<object> GetBusLineWithSelectedFields(Func<BusLine, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddBusLine(BusLine busLine)
        {//add asigning of running number
            if (DataSource.busLineList.FirstOrDefault(b => b.BusLineID == busLine.BusLineID) != null)
                throw new DO.InvalidBusLineException(busLine.BusLineID.ToString(), "Duplicate bus license number");
            DataSource.busLineList.Add(busLine.Clone());
        }

        public BusLine GetBusLine(int lineID)
        {
            DO.BusLine line = DataSource.busLineList.Find(b => b.BusLineID == lineID); //define list bus

            if (line != null)
                return line.Clone();
            else
                throw new DO.InvalidBusLineException(lineID.ToString(), $"wrong bus license: {lineID}");
        }

        public void UpdateBusLine (BusLine busline)
        {
            DO.BusLine line = DataSource.busLineList.Find(b => b.BusLineID == busline.BusLineID);

            if (line != null)
            {    
                DataSource.busLineList.Remove(line);
                DataSource.busLineList.Add(line.Clone());
            }
            else
                throw new DO.InvalidBusLineException(busline.BusLineID.ToString(), $"wrong bus line ID: {busline.BusLineID}");
        }

        public void DeleteBusLine(int lineID)
        {
            DO.BusLine line = DataSource.busLineList.Find(b => b.BusLineID == lineID);

            if (line != null)
            {
                DataSource.busLineList.Remove(line);
            }
            else
                throw new DO.InvalidBusLineException(lineID.ToString(), $"wrong bus line ID: {lineID}");
        }

        #endregion

        #region CRUD Implementation - BusesOnTrip

        public IEnumerable<BusOnTrip> GetAllBusesOnTrip()
        {
            return from BusOnTrip in DataSource.busOnTripList
                   select BusOnTrip.Clone();
        }

        public IEnumerable<object> GetBusOnTripWithSelectedFields(Func<BusOnTrip, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            if (DataSource.busOnTripList.FirstOrDefault(b => b.BusRoadID == busOnTrip.BusRoadID) != null)
                throw new DO.InvalidBusOnTripIDException(busOnTrip.BusRoadID.ToString(), "Duplicate bus on trip ID");
            DataSource.busOnTripList.Add(busOnTrip.Clone());
        }

        public BusOnTrip GetBusOnTrip(int roadID)
        {
            DO.BusOnTrip trip = DataSource.busOnTripList.Find(b => b.BusRoadID == roadID); //define list bus

            if (trip != null)
                return trip.Clone();
            else
                throw new DO.InvalidBusOnTripIDException(roadID.ToString(), $"wrong bus on trip ID: {roadID}");
        }

        public void UpdateBusOnTrip(BusOnTrip bus)
        {
            DO.BusOnTrip trip = DataSource.busOnTripList.Find(b => b.BusRoadID == bus.BusRoadID);

            if (trip != null)
            {
                DataSource.busOnTripList.Remove(trip);
                DataSource.busOnTripList.Add(trip.Clone());
            }
            else
                throw new DO.InvalidBusOnTripIDException(bus.BusRoadID.ToString(), $"wrong bus on trip ID: {bus.BusRoadID}");
        }

        public void DeleteBusOnTrip(int roadID)
        {
            DO.BusOnTrip trip = DataSource.busOnTripList.Find(b => b.BusRoadID == roadID);

            if (trip != null)
            {
                DataSource.busOnTripList.Remove(trip);
            }
            else
                throw new DO.InvalidBusOnTripIDException(roadID.ToString(), $"wrong bus on trip ID: {roadID}");
        }
        #endregion

        #region CRUD Implementation - BusStop 

        public IEnumerable<BusStop> GetAllBusStops()
        {
            return from BusStop in DataSource.busStopList
                   select BusStop.Clone();
        }

        public IEnumerable<object> GetBusStopWithSelectedFields(Func<BusOnTrip, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddBusStop(BusStop busStop)
        {
            if (DataSource.busStopList.FirstOrDefault(b => b.StopCode == busStop.StopCode) != null)
                throw new DO.InvalidStopCodeException(busStop.StopCode.ToString(), "Duplicate bus stop code");
            DataSource.busStopList.Add(busStop.Clone());
        }

        public BusStop GetBusStop(int stopCode)
        {
            DO.BusStop stop = DataSource.busStopList.Find(b => b.StopCode == stopCode); //define list bus

            if (stop != null)
                return stop.Clone();
            else
                throw new DO.InvalidStopCodeException(stopCode.ToString(), $"wrong bus stop code: {stopCode}");
        }

        public void UpdateBusStop(BusStop bstop)
        {
            DO.BusStop stop = DataSource.busStopList.Find(b => b.StopCode == bstop.StopCode);

            if (stop != null)
            {
                DataSource.busStopList.Remove(stop);
                DataSource.busStopList.Add(stop.Clone());
            }
            else
                throw new DO.InvalidStopCodeException(bstop.StopCode.ToString(), $"wrong bus stop code: {bstop.StopCode}");
        }

        public void DeleteBusStop(int stopCode)
        {
            DO.BusStop stop = DataSource.busStopList.Find(b => b.StopCode == stopCode);

            if (stop != null)
            {
                DataSource.busStopList.Remove(stop);
            }
            else
                throw new DO.InvalidStopCodeException(stopCode.ToString(), $"wrong bus stop code: {stopCode}");
        }
        #endregion

        #region CRUD Implementation - LineLeaving

        public IEnumerable<LineLeaving> GetAllLinesLeaving()
        {
            return from LineLeaving in DataSource.lineLeavingList
                   select LineLeaving.Clone();
        }

        public IEnumerable<object> GetLineLeavingWithSelectedFields(Func<LineLeaving, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddLineLeaving(LineLeaving lineLeaving)
        {
            if (DataSource.lineLeavingList.FirstOrDefault(b => b.leavingEntityKey == lineLeaving.leavingEntityKey) != null)
                throw new DO.InvalidStopCodeException(lineLeaving.leavingEntityKey, "Duplicate line leaving key");
            DataSource.lineLeavingList.Add(lineLeaving.Clone());
        }

        public LineLeaving GetLineLeaving(int lineID, TimeSpan startTime)
        {
            string key = lineID.ToString() + startTime.ToString();
            DO.LineLeaving line = DataSource.lineLeavingList.Find(b => b.leavingEntityKey == key); //define list bus

            if (line != null)
                return line.Clone();
            else
                throw new DO.InvalidLineLeavingKeyException(key, $"wrong line leaving key: {key}");
        }

        public void UpdateLineLeaving(LineLeaving lineLeaving)
        {
            DO.LineLeaving line = DataSource.lineLeavingList.Find(b => b.leavingEntityKey == lineLeaving.leavingEntityKey);

            if (line != null)
            {
                DataSource.lineLeavingList.Remove(line);
                DataSource.lineLeavingList.Add(line.Clone());
            }
            else
                throw new DO.InvalidLineLeavingKeyException(lineLeaving.leavingEntityKey, $"wrong line leaving key: {lineLeaving.leavingEntityKey}");
        }

        public void DeleteLineLeaving(int lineID, TimeSpan startTime)
        {
            string key = lineID.ToString() + startTime.ToString();
            DO.LineLeaving line = DataSource.lineLeavingList.Find(b => b.leavingEntityKey == key);

            if (line != null)
            {
                DataSource.lineLeavingList.Remove(line);
            }
            else
                throw new DO.InvalidLineLeavingKeyException(key, $"wrong line leaving key: {key}");
        }

        #endregion

        //Gila
  #region CRUD Implementation - LineStation

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
        public LineStation GetLineStation(string lineStationKey)
        {
            DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.entityKey == lineStationKey);

            if (findLineStation != null)
            {
                return lineStation.Clone();
            }
            else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }
        public void UpdateLineStation(string lineStationKey)
        {
            DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.entityKey == lineStationKey);

            if (findLineStation != null)
            {
                DataSource.lineStationList.Remove(findLineStation);
                DataSource.lineStationList.Add(findLineStation.Clone());
            }
            else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }
        public void DeleteLineStation(string lineStationKey)
        {
            DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.entityKey == lineStationKey);

            if (findLineStation != null)
            {
                DataSource.lineStationList.Remove(findLineStation);
            }
            else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }
        #endregion

        #region CRUD Implementation - Staff

        public IEnumerable<Staff> GetAllStaff()
        {
            return from Staff in DataSource.staffList
                   select staff.Clone();
        }

        public IEnumerable<object> GetStaffWithSelectedFields(Func<Staff, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddStaff(Staff staff)
        {
            //check if staff member exsists already
            if ((DataSource.staffList.FirstOrDefault(st => st.BusDriverID == staff.BusDriverID) != null))
            {
                throw new DO.StaffAlreadyInSystemException(staff.BusDriverID, $"Staff member {staff.BusDriverID} is already listed in the system");
            } //it is a new staff member so can add to list:
            DataSource.staffList.Add(staff.Clone());
        }

        public Staff GetStaff(string staffID)
        {
            DO.Staff findStaff = DataSource.staffList.Find(member => member.BusDriverID == staffID);

            if (findStaff != null)
            {
                return staff.Clone();
            }
            else throw new DO.StaffNotInSystemException(staff.BusDriverID, $"Staff member {staff.BusDriverID} is not listed in the system");
        }

        public void UpdateStaff(string staffID, Action<Staff> update)
        {
            DO.Staff findStaff = DataSource.staffList.Find(member => member.BusDriverID == staffID);

            if (findStaff != null)
            {
                DataSource.staffList.Remove(findStaff);
                DataSource.staffList.Add(findStaff.Clone());
            }
            else throw new DO.StaffNotInSystemException(staff.BusDriverID, $"Staff member {staff.BusDriverID} is not listed in the system");
        }

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


        public IEnumerable<SuccessiveStations> GetAllSuccessiveStations()
        {
            return from SuccessiveStations in DataSource.succStationsList
                   select SuccessiveStations.Clone();
        }

        public IEnumerable<object> GetSuccessiveStationsWithSelectedFields(Func<SuccessiveStations, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddSuccessiveStations(SuccessiveStations successiveStations)
        {
            //check if station 1 exists
            if ((DataSource.succStationsList.FirstOrDefault(station => station.StationCode1 == successiveStations.StationCode1) != null))
            {
                //if station 1 exsists see if it connects to station 2
                if ((DataSource.succStationsList.FirstOrDefault(station => station.StationCode2 == successiveStations.StationCode2) != null))
                {
                    throw new DO.MissingSuccessiveStationsException(successiveStations.StationEntityKey, "Duplicate Successive Station");
                }
            } //it is a new LineSation so can add to collection:
            DataSource.succStationsList.Add(successiveStations.Clone());
        }

        public SuccessiveStations GetSuccessiveStations(string entityKey)
        {
            DO.SuccessiveStations findStations = DataSource.succStationsList.Find(stat => stat.StationEntityKey == entityKey);
            if (findStations != null)
            {
                return successiveStations.Clone();
            }
            else throw new DO.MissingSuccessiveStationsException(entityKey, $"No data found for Successive Station : {entityKey}");
        }

        public void UpdateSuccessiveStations(string entityKey)
        {

            DO.SuccessiveStations findStations = DataSource.succStationsList.Find(stat => stat.StationEntityKey == entityKey);
            if (findStations != null)
            {
                DataSource.succStationsList.Remove(findStations);
                DataSource.succStationsList.Add(findStations.Clone());
            }
            else throw new DO.MissingSuccessiveStationsException(entityKey, $"No data found for Successive Station : {entityKey}");
        }

        public void DeleteSuccessiveStations(string entityKey)
        {

            DO.SuccessiveStations findStations = DataSource.succStationsList.Find(stat => stat.StationEntityKey == entityKey);
            if (findStations != null)
            {
                DataSource.succStationsList.Remove(findStations);
            }
            else throw new DO.MissingSuccessiveStationsException(entityKey, $"No data found for Successive Station : {entityKey}");
        }
        #endregion

        #region CRUD Implementation - User

        public IEnumerable<User> GetAllUsers()
        {
            return from User in DataSource.usersList
                   select User.Clone();
        }

        public IEnumerable<object> GetUserWithSelectedFields(Func<SuccessiveStations, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            if ((DataSource.usersList.FirstOrDefault(use => use.userName == user.userName)!= null))
            {
                throw new DO.ExsistingUserException(user.userName, "Duplicate user");
            }

            DataSource.usersList.Add(user.Clone());
        }

        public User GetUser(string name)
        {
            DO.User findUser = DataSource.usersList.Find(u => u.userName == name);

            if (findUser != null)
            {
                return user.Clone();
            }
            else throw new DO.MissingUserException(name, $"No data found for user: {name}");
        }

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

        public IEnumerable<SuccessiveStations> GetAllUserTrip()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetUserTripWithSelectedFields(Func<UserTrip, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddUserTrip(UserTrip userTrip)
        {
            DO.UserTrip findTrip = DataSource.userTripList.Find(utrip => utrip.userTravelID == userTrip.userTravelID);
            if (findTrip != null)
            {
                throw new DO.ExsistingUserTripException(userTrip.userTravelID.ToString(), "Duplicate UserTrip");
            }

            DataSource.userTripList.Add(findTrip.Clone());
        }
        public UserTrip GetUserTrip(int travelID)
        {
                DO.UserTrip findTrip = DataSource.userTripList.Find(utrip => utrip.userTravelID == travelID);
            if (findTrip != null)
            {
                return userTrip.Clone();
            }
            else throw new DO.MissingUserTripException(travelID.ToString(), $"No data found for UserTrip: {travelID}");
        }

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
