﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DALApi
{
    public interface IDAL
    {
        /* IMPLEMENTING CRUD METHODS FOR DO ENTITIES */

        #region Bus
        IEnumerable<DO.Bus> GetAllBus();
        IEnumerable<object> GetBusListWithSelectedFields(Predicate<Bus> predicate);
        void AddBus(Bus bus); //create
        Bus GetBus(string license); //retrieve
        void UpdateBus(Bus bus); //update
        void DeleteBus(string license); //delete
        #endregion

        #region BusLine
        IEnumerable<DO.BusLine> GetAllBusLines(); //IEnumerable
        IEnumerable<object> GetBusLineWithSelectedFields(Func<DO.BusLine, object> generate); //IEnumerable
        string AddBusLine(BusLine busLine);//create
        BusLine GetBusLine(string lineID);//retrieve
        void UpdateBusLine(BusLine busline);//update
        void DeleteBusLine(string lineID);//delete              
        #endregion

        #region BusOnTrip
        IEnumerable<DO.BusOnTrip> GetAllBusOnTrip(); //IEnumerable
        IEnumerable<object> GetBusOnTripWithSelectedFields(Func<DO.BusOnTrip, object> generate); //IEnumerable
        string AddBusOnTrip(BusOnTrip busOnTrip, string lineID);//create
        BusOnTrip GetBusOnTrip(int roadID);//retrieve
        void UpdateBusOnTrip(BusOnTrip busOnTrip);//update
        void DeleteBusOnTrip(int roadID);//delete

        #endregion

        #region BusStop
        IEnumerable<DO.BusStop> GetAllBusStops(); //IEnumerable
        //IEnumerable<object> GetBusStopWithSelectedFields(Func<DO.BusOnTrip, object> generate); //IEnumerable
        void AddBusStop(BusStop busStop);//create
        BusStop GetBusStop(string stopCode);//retrieve
        void UpdateBusStop(BusStop busStop);//update
        void DeleteBusStop(string stopCode);//delete
        #endregion

        #region LineLeaving
        IEnumerable<DO.LineLeaving> GetAllLinesLeaving(); //IEnumerable
        IEnumerable<object> GetLineLeavingWithSelectedFields(Func<DO.LineLeaving, object> generate); //IEnumerable
        void AddLineLeaving(LineLeaving lineLeaving, string lineID, string staffID);//create
        LineLeaving GetLineLeaving(string lineID);//retrieve
        void UpdateLineLeaving(LineLeaving lineLeaving);//update
        void DeleteLineLeaving(string lineID, TimeSpan startTime);//delete

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
        int AddLineStation(LineStation lineStation, string lineID);//create
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
        string AddStaff(Staff staff);//create
        Staff GetStaff(string staffID);//retrieve
        void UpdateStaff(string staffID/*, Action<Staff> update*/);//update
        void DeleteStaff(string staffID);//delete

        #endregion

        //#region SuccessiveStations
        //IEnumerable<DO.SuccessiveStations> GetAllSuccessiveStations(); //IEnumerable
        //IEnumerable<object> GetSuccessiveStationsWithSelectedFields(Func<DO.SuccessiveStations, object> generate); //IEnumerable
        //void AddSuccessiveStations(SuccessiveStations successiveStations);//create
        //SuccessiveStations GetSuccessiveStations(int stat1, int stat2);//retrieve
        //void UpdateSuccessiveStations(string entityKey);//update
        //void DeleteSuccessiveStations(string entityKey);//delete
        //#endregion

        #region User
        IEnumerable<DO.User> GetAllUsers(); //IEnumerable
        void AddUser(User user);//create
        User GetUser(string name, string pass);//retrieve
        void UpdateUser(string name);//update
        void DeleteUser(string name);//delete
        #endregion

        #region Admin
        Admin GetAdmin(string name, string password);
        IEnumerable<Admin> GetAllAdmin();
        #endregion


    }
}
