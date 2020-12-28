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
        {
            if (DataSource.busLineList.FirstOrDefault(b => b.BusLineID == busLine.BusLineID) != null)
                throw new DO.BadBusLineIDException(busLine.BusLineID, "Duplicate bus license number");
            DataSource.busLineList.Add(busLine.Clone());
        }

        public BusLine GetBusLine(int lineID)
        {
            DO.BusLine line = DataSource.busLineList.Find(b => b.BusLineID == lineID); //define list bus

            if (line != null)
                return line.Clone();
            else
                throw new DO.BadBusLineIDException(lineID, $"wrong bus license: {lineID}");
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
                throw new DO.BadBusLineIDException(busline.BusLineID, $"wrong bus line ID: {busline.BusLineID}");
        }

        public void DeleteBusLine(int lineID)
        {
            DO.BusLine line = DataSource.busLineList.Find(b => b.BusLineID == lineID);

            if (line != null)
            {
                DataSource.busLineList.Remove(line);
            }
            else
                throw new DO.BadBusLineIDException(lineID, $"wrong bus line ID: {lineID}");
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
                throw new DO.BadBusOnTripIDException(busOnTrip.BusRoadID, "Duplicate bus on trip ID");
            DataSource.busOnTripList.Add(busOnTrip.Clone());
        }

        public BusOnTrip GetBusOnTrip(int roadID)
        {
            DO.BusOnTrip trip = DataSource.busOnTripList.Find(b => b.BusRoadID == roadID); //define list bus

            if (trip != null)
                return trip.Clone();
            else
                throw new DO.BadBusOnTripIDException(roadID, $"wrong bus on trip ID: {roadID}");
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
                throw new DO.BadBusOnTripIDException(bus.BusRoadID, $"wrong bus on trip ID: {bus.BusRoadID}");
        }

        public void DeleteBusOnTrip(int roadID)
        {
            DO.BusOnTrip trip = DataSource.busOnTripList.Find(b => b.BusRoadID == roadID);

            if (trip != null)
            {
                DataSource.busOnTripList.Remove(trip);
            }
            else
                throw new DO.BadBusOnTripIDException(roadID, $"wrong bus on trip ID: {roadID}");
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
                throw new DO.BadBusStopCodeException(busStop.StopCode, "Duplicate bus stop code");
            DataSource.busStopList.Add(busStop.Clone());
        }

        public BusStop GetBusStop(int stopCode)
        {
            DO.BusStop stop = DataSource.busStopList.Find(b => b.StopCode == stopCode); //define list bus

            if (stop != null)
                return stop.Clone();
            else
                throw new DO.BadBusStopCodeException(stopCode, $"wrong bus stop code: {stopCode}");
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
                throw new DO.BadBusStopCodeException(bstop.StopCode, $"wrong bus stop code: {bstop.StopCode}");
        }

        public void DeleteBusStop(int stopCode)
        {
            DO.BusStop stop = DataSource.busStopList.Find(b => b.StopCode == stopCode);

            if (stop != null)
            {
                DataSource.busStopList.Remove(stop);
            }
            else
                throw new DO.BadBusStopCodeException(stopCode, $"wrong bus stop code: {stopCode}");
        }
        #endregion

        #region CRUD Implementation - LineLeaving

        public IEnumerable<LineLeaving> GetAllLinesLeaving()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetLineLeavingWithSelectedFields(Func<LineLeaving, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddLineLeaving(LineLeaving lineLeaving)
        {
            throw new NotImplementedException();
        }

        public LineLeaving GetLineLeaving(int lineID, TimeSpan startTime)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineLeaving(LineLeaving lineLeaving)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineLeaving(int lineID, TimeSpan startTime)
        {
            throw new NotImplementedException();
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
                   select generate(GetLineStation(lineStation.entityKey));

            //return from student in DataSource.ListStudents
            //       select generate(student.ID, GetPerson(student.ID).Name);
        }
        public void AddLineStation(LineStation lineStation)
        {
            //check if line exsists
            if ((DataSource.lineStationList.FirstOrDefault(line => line.lineID == lineStation.lineID) != null))
            {
                //if line exsists see if it holds the same station
                if ((DataSource.lineStationList.FirstOrDefault(stations => stations.stationCode == lineStation.stationCode) != null))
                {
                    throw new DO.ExsistingLineStationException(lineStation, "Duplicate lineStation"); 
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
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetSuccessiveStationsWithSelectedFields(Func<SuccessiveStations, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddSuccessiveStations(SuccessiveStations successiveStations)
        {
            throw new NotImplementedException();
        }

        public SuccessiveStations GetSuccessiveStations(int stCode1, int stCode2)
        {
            throw new NotImplementedException();
        }

        public void UpdateSuccessiveStations(int stCode1, int stCode2, Action<SuccessiveStations, object> update)
        {
            //DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.entityKey == lineStationKey);

            //if (findLineStation != null)
            //{
            //    DataSource.lineStationList.Remove(findLineStation);
            //    DataSource.lineStationList.Add(findLineStation.Clone());
            //}
            //else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }

        public void DeleteSuccessiveStations(int stCode1, int stCode2)
        {
            //DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.entityKey == lineStationKey);

            //if (findLineStation != null)
            //{
            //    DataSource.lineStationList.Remove(findLineStation);
            //}
            //else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }
        #endregion

        #region CRUD Implementation - User

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetUserWithSelectedFields(Func<SuccessiveStations, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int stCode1, int stCode2)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(string userName, Action<SuccessiveStations, object> update)
        {
            //DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.entityKey == lineStationKey);

            //if (findLineStation != null)
            //{
            //    DataSource.lineStationList.Remove(findLineStation);
            //    DataSource.lineStationList.Add(findLineStation.Clone());
            //}
            //else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }

        public void DeleteUser(string name)
        { 
        //{
        //    DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.entityKey == lineStationKey);

        //    if (findLineStation != null)
        //    {
        //        DataSource.lineStationList.Remove(findLineStation);
        //    }
        //    else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
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
            throw new NotImplementedException();
        }

        public UserTrip GetUserTrip(int travelID)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserTrip(int travelID, Action<UserTrip, object> update)
        {
            //DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.entityKey == lineStationKey);

            //if (findLineStation != null)
            //{
            //    DataSource.lineStationList.Remove(findLineStation);
            //    DataSource.lineStationList.Add(findLineStation.Clone());
            //}
            //else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }

        public void DeleteUserTrip(int travelID)
        {
        //    DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.entityKey == lineStationKey);

        //    if (findLineStation != null)
        //    {
        //        DataSource.lineStationList.Remove(findLineStation);
        //    }
        //    else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }
        #endregion

    }
}
