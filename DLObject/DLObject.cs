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

        public void AddBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(string license)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetBusListWithSelectedFields(Func<Bus, object> generate)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(string license, Action<Bus> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(string license)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region CRUD Implementation - BusLine

        public IEnumerable<BusLine> GetAllBusLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetBusLineWithSelectedFields(Func<BusLine, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddBusLine(BusLine busLine)
        {
            throw new NotImplementedException();
        }

        public void GetBusLine(int lineID)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusLine(int lineID, Action<BusLine> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusLine(int lineID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CRUD Implementation - BusesOnTrip

        public IEnumerable<BusOnTrip> GetAllBusesOnTrip()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetBusOnTripWithSelectedFields(Func<BusOnTrip, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            throw new NotImplementedException();
        }

        public void GetBusOnTrip(int roadID)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusOnTrip(int roadID, Action<BusOnTrip> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusOnTrip(int roadID)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region CRUD Implementation - BusStop 

        public IEnumerable<BusStop> GetAllBusStops()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetBusStopWithSelectedFields(Func<BusOnTrip, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddBusStop(BusStop busStop)
        {
            throw new NotImplementedException();
        }

        public void GetBusStop(int stopCode)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusStop(int stopCode, Action<BusOnTrip> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusStop(int stopCode)
        {
            throw new NotImplementedException();
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

        public void GetLineLeaving(int lineID, TimeSpan startTime)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineLeaving(int lineID, TimeSpan startTime, Action<LineLeaving> update)
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
                   select generate(GetLineStation(lineStation.lineID, lineStation.stationCode)); //???

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
        public LineStation GetLineStation(int lineID, int stationCode)
        {
            //PICK AN OPTION (1)
            //DO.LineStation findLine = DataSource.lineStationList.Find(line => line.lineID == lineID);
            //DO.LineStation findStation = DataSource.lineStationList.Find(line => line.stationCode == stationCode);
            //(2)
            var lineStationKey = lineID + stationCode;
            DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.entityKey == lineStationKey);

            if (findLineStation != null)
            {
                return lineStation.Clone();
            }
            else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }
        public void UpdateLineStation(int lineID, int stationCode, Action<LineStation> update)
        {
            var lineStationKey = lineID + stationCode;
            DO.LineStation findLineStation = DataSource.lineStationList.Find(line => line.entityKey == lineStationKey);

            if (findLineStation != null)
            {
                DataSource.lineStationList.Remove(findLineStation);
                DataSource.lineStationList.Add(findLineStation.Clone());
            }
            else throw new DO.MissingLineStationException(lineStationKey, $"No data found for LineStation: {lineStationKey}");
        }
        public void DeleteLineStation(int lineID, int stationCode)
        {
            var lineStationKey = lineID + stationCode;
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
            DO.Staff FindStaff = DataSource.staffList.Find(member => member.BusDriverID == staffID);

            if (FindStaff != null)
            {
                return staff.Clone();
            }
            else throw new DO.StaffNotInSystemException(staff.BusDriverID, $"Staff member {staff.BusDriverID} is not listed in the system");
        }

        public void UpdateStaff(string staffID, Action<Staff> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStaff(string staffID)
        {
            throw new NotImplementedException();
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

        public void GetSuccessiveStations(int stCode1, int stCode2)
        {
            throw new NotImplementedException();
        }

        public void UpdateSuccessiveStations(int stCode1, int stCode2, Action<SuccessiveStations, object> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteSuccessiveStations(int stCode1, int stCode2)
        {
            throw new NotImplementedException();
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

        public void GetUser(int stCode1, int stCode2)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(string userName, Action<SuccessiveStations, object> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string name)
        {
            throw new NotImplementedException();
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

        public void GetUserTrip(int travelID)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserTrip(int travelID, Action<UserTrip, object> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserTrip(int travelID)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
