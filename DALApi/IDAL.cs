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
        IEnumerable<DO.Bus> GetAllBuses();
        IEnumerable<object> GetBusListWithSelectedFields(Func<DO.Bus, object> generate);
        void AddBus(Bus bus); //create
        Bus GetBus(string license); //retrieve
        void UpdateBus(string license, Action<Bus> update); //update
        void DeleteBus(string license); //delete
        #endregion

        #region BusLine
        IEnumerable<DO.BusLine> GetAllBusLines(); //IEnumerable
        IEnumerable<object> GetBusLineWithSelectedFields(Func<DO.BusLine, object> generate); //IEnumerable
        void AddBusLine(BusLine busLine);//create
        BusLine GetBusLine(int lineID);//retrieve
        void UpdateBusLine(BusLine busline);//update
        void DeleteBusLine(int lineID);//delete              
        #endregion

        #region BusOnTrip
        IEnumerable<DO.BusOnTrip> GetAllBusesOnTrip(); //IEnumerable
        IEnumerable<object> GetBusOnTripWithSelectedFields(Func<DO.BusOnTrip, object> generate); //IEnumerable
        void AddBusOnTrip(BusOnTrip busOnTrip);//create
        BusOnTrip GetBusOnTrip(int roadID);//retrieve
        void UpdateBusOnTrip(BusOnTrip busOnTrip);//update
        void DeleteBusOnTrip(int roadID);//delete

        #endregion

        #region BusStop
        IEnumerable<DO.BusStop> GetAllBusStops(); //IEnumerable
        IEnumerable<object> GetBusStopWithSelectedFields(Func<DO.BusOnTrip, object> generate); //IEnumerable
        void AddBusStop(BusStop busStop);//create
        BusStop GetBusStop(int stopCode);//retrieve
        void UpdateBusStop(BusStop busStop);//update
        void DeleteBusStop(int stopCode);//delete
        #endregion

        #region LineLeaving
        IEnumerable<DO.LineLeaving> GetAllLinesLeaving(); //IEnumerable
        IEnumerable<object> GetLineLeavingWithSelectedFields(Func<DO.LineLeaving, object> generate); //IEnumerable
        void AddLineLeaving(LineLeaving lineLeaving);//create
        LineLeaving GetLineLeaving(int lineID, TimeSpan startTime);//retrieve
        void UpdateLineLeaving(LineLeaving lineLeaving);//update
        void DeleteLineLeaving(int lineID, TimeSpan startTime);//delete

        #endregion
       
        #region LineStation
        /// <summary>
        /// Action: This Method returns a collection of all the Stations on a specific line.
        /// Return type: IEnumerable
        /// </summary>
        /// <returns></returns>
        IEnumerable<DO.LineStation> GetAllLineStations(); //IEnumerable
        /// <summary>
        /// Action: This Method returns a collection of all the Stations on a specific line which ???.
        /// Return type: IEnumerable
        /// </summary>
        /// <returns></returns>
        IEnumerable<object> GetLineStationWithSelectedFields(Func<DO.LineStation, object> generate); //IEnumerable
        /// <summary>
        /// Action: This method adds a LineStation (it adds a station to a specific line).
        /// Return type: void
        /// </summary>
        /// <returns></returns>
        void AddLineStation(LineStation lineStation);//create
        /// <summary>
        /// Action: This method gets a specific Line-Station pair.
        /// Return type: LineStation
        /// </summary>
        /// <returns></returns>
        LineStation GetLineStation(string lineStationKey);//retrieve
        /// <summary>
        /// Action: This method updates a LineStation given it's identifiying features.
        /// Return type: void
        /// </summary>
        /// <returns></returns>
        void UpdateLineStation(string lineStationKey);//update
        /// <summary>
        /// Action: This method removes a LineStation given it's identifiying features.
        /// Return type: void
        /// </summary>
        /// <returns></returns>
        void DeleteLineStation(string lineStationKey);//delete
        #endregion

        #region Staff
        IEnumerable<DO.Staff> GetAllStaff(); //IEnumerable
        IEnumerable<object> GetStaffWithSelectedFields(Func<DO.Staff, object> generate); //IEnumerable
        void AddStaff(Staff staff);//create
        Staff GetStaff(string staffID);//retrieve
        void UpdateStaff(string staffID, Action<Staff> update);//update
        void DeleteStaff(string staffID);//delete

        #endregion

        #region SuccessiveStations
        IEnumerable<DO.SuccessiveStations> GetAllSuccessiveStations(); //IEnumerable
        IEnumerable<object> GetSuccessiveStationsWithSelectedFields(Func<DO.SuccessiveStations, object> generate); //IEnumerable
        void AddSuccessiveStations(SuccessiveStations successiveStations);//create
        SuccessiveStations GetSuccessiveStations(int stCode1, int stCode2);//retrieve
        void UpdateSuccessiveStations(int stCode1, int stCode2, Action<DO.SuccessiveStations, object> update);//update
        void DeleteSuccessiveStations(int stCode1, int stCode2);//delete
        #endregion

        #region User
        IEnumerable<DO.User> GetAllUsers(); //IEnumerable
        IEnumerable<object> GetUserWithSelectedFields(Func<DO.SuccessiveStations, object> generate); //IEnumerable
        void AddUser(User user);//create
        User GetUser(int stCode1, int stCode2);//retrieve
        void UpdateUser(string userName, Action<DO.SuccessiveStations, object> update);//update
        void DeleteUser(string name);//delete
        #endregion

        #region UserTrip
        IEnumerable<DO.SuccessiveStations> GetAllUserTrip(); //IEnumerable
        IEnumerable<object> GetUserTripWithSelectedFields(Func<DO.UserTrip, object> generate); //IEnumerable
        void AddUserTrip(UserTrip userTrip);//create
        UserTrip GetUserTrip(int travelID);//retrieve
        void UpdateUserTrip(int travelID, Action<DO.UserTrip, object> update);//update
        void DeleteUserTrip(int travelID);//delete

        #endregion
    }
}
