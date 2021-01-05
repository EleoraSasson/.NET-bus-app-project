using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLApi
{
    public interface IBL
    {

        //#region Bus
        //IEnumerable<DO.Bus> GetAllBuses();
        //IEnumerable<object> GetBusListWithSelectedFields(Func<DO.Bus, object> generate);
        //void AddBus(Bus bus); //create
        //Bus GetBus(string license); //retrieve
        //void UpdateBus(Bus bus); //update
        //void DeleteBus(string license); //delete
        //#endregion

        /* IMPLEMENTING CRUD METHODS FOR BO ENTITIES */

        #region BusFleet
        IEnumerable<BusFleet> GetEntireBusFleet();
        IEnumerable<BusFleet> GetBusFleetWithSelectedFields(Func<BusFleet, object> generate);
        void AddToBusFleet(BusFleet fleet); //create
        BusFleet GetFleet(); //retrieve
        void UpdateBusFleet(BusFleet fleet); //update
        void DeleteBusFleet(); //delete
        #endregion

        #region BusRoute

        #endregion

        #region ScheduleOfRoute

        #endregion

        #region CompanySchedule

        #endregion

        #region StationWithRoutes

        #endregion

    }
}
