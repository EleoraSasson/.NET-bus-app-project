using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DALApi
{
    public interface IDAL
    {
        /* IMPLEMENTING CRUD METHODS FOR BUS DO ENTITY */

        #region Bus
        // Create
        void AddBus(Bus bus); //create
        //Retrieve
        Bus GetBus(string license); //retrieve

        //Update
        //Delete

        IEnumerable<DO.Bus> GetAllBuses();
        IEnumerable<object> GetBusListWithSelectedFields(Func<DO.Bus, object> generate);

        // void UpdateBus(Bus bus);
        void UpdateBus(string license, Action<Bus> update); //method that knows to updt specific fields in Bus
        void DeleteBus(string license);
        #endregion

        #region BusLine
        IEnumerable<DO.BusLine> GetAllBusLines(); //IEnumerable
        IEnumerable<object> GetBusLineWithSelectedFields(Func<DO.BusLine, object> generate); //IEnumerable
        void AddBusLine(BusLine busLine);//create
        void GetBusLine(int lineID);//retrieve
        void UpdateBusLine(int lineID, Action<BusLine> update);//update
        void DeleteBusLine(int lineID);//delete              
        #endregion

        #region BusOnTrip
        IEnumerable<DO.BusOnTrip> GetAllBusesOnTrip(); //IEnumerable
        IEnumerable<object> GetBusOnTripWithSelectedFields(Func<DO.BusOnTrip, object> generate); //IEnumerable
        void AddBusOnTrip(BusOnTrip busOnTrip);//create
        void GetBusOnTrip(int roadID);//retrieve
        void UpdateBusOnTrip(int roadID, Action<BusOnTrip> update);//update
        void DeleteBusOnTrip(int roadID);//delete

        #endregion

        #region BusStop
        IEnumerable<DO.BusStop> GetAllBusStops(); //IEnumerable
        IEnumerable<object> GetBusStopWithSelectedFields(Func<DO.BusOnTrip, object> generate); //IEnumerable
        void AddBusStop(BusStop busStop);//create
        void GetBusStop(int stopCode);//retrieve
        void UpdateBusStop(int stopCode, Action<BusOnTrip> update);//update
        void DeleteBusStop(int stopCode);//delete
        #endregion

        #region LineLeaving
        IEnumerable<DO.LineLeaving> GetAllLinesLeaving(); //IEnumerable
        IEnumerable<object> GetLineLeavingWithSelectedFields(Func<DO.LineLeaving, object> generate); //IEnumerable
        void AddLineLeaving(LineLeaving lineLeaving);//create
        void GetLineLeaving(int lineID, TimeSpan startTime);//retrieve
        void UpdateLineLeaving(int lineID, TimeSpan startTime, Action<LineLeaving> update);//update
        void DeleteLineLeaving(int lineID, TimeSpan startTime);//delete

        #endregion

        #region LineStation
        IEnumerable<DO.LineStation> GetAllLineStations(); //IEnumerable
        IEnumerable<object> GetLineStationWithSelectedFields(Func<DO.LineStation, object> generate); //IEnumerable
        void AddLineStation(LineStation lineStation);//create
        void GetLineStation(int lineID, int stationCode);//retrieve
        void UpdateLineStation(int lineID, int stationCode, Action<LineStation> update);//update
        void DeleteLineStation(int lineID, int stationCode);//delete

        #endregion

        #region Staff
        IEnumerable<DO.Staff> GetAllStaff(); //IEnumerable
        IEnumerable<object> GetStaffWithSelectedFields(Func<DO.Staff, object> generate); //IEnumerable
        void AddStaff(Staff staff);//create
        void GetStaff(string staffID);//retrieve
        void UpdateStaff(string staffID, Action<Staff> update);//update
        void DeleteStaff(string staffID);//delete

        #endregion

        #region SuccessiveStations
        IEnumerable<DO.SuccessiveStations> GetAllSuccessiveStations(); //IEnumerable
        IEnumerable<object> GetSuccessiveStationsWithSelectedFields(Func<DO.SuccessiveStations, object> generate); //IEnumerable
        void AddSuccessiveStations(SuccessiveStations successiveStations);//create
        void GetSuccessiveStations(int stCode1, int stCode2);//retrieve
        void UpdateSuccessiveStations(int stCode1, int stCode2, Action<DO.SuccessiveStations, object> update);//update
        void DeleteSuccessiveStations(int stCode1, int stCode2);//delete
        #endregion

        #region User
        IEnumerable<DO.User> GetAllUsers(); //IEnumerable
        IEnumerable<object> GetUserWithSelectedFields(Func<DO.SuccessiveStations, object> generate); //IEnumerable
        void AddUser(User user);//create
        void GetUser(int stCode1, int stCode2);//retrieve
        void UpdateUser(string userName, Action<DO.SuccessiveStations, object> update);//update
        void DeleteUser(string name);//delete
        #endregion

        #region UserTrip
        IEnumerable<DO.SuccessiveStations> GetAllUserTrip(); //IEnumerable
        IEnumerable<object> GetUserTripWithSelectedFields(Func<DO.UserTrip, object> generate); //IEnumerable
        void AddUserTrip(UserTrip userTrip);//create
        void GetUserTrip(int travelID);//retrieve
        void UpdateUserTrip(int travelID, Action<DO.UserTrip, object> update);//update
        void DeleteUserTrip(int travelID);//delete

        #endregion
    }
}
