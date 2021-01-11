using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALApi;
using DO;
using BO;

namespace BLApi
{
    class BLImp1 : IBL
    {
        #region CRUD BusFleet
        IDAL dal = DLFactory.GetDL();
        public void AddToBusFleet(Bus bus)
        {
            bus.BusStatus = Status.Available; //every new bus added to the fleet is automatically available for travel.
            bus.BusFuel = 1200; //every new bus added to fleet is considered to have a full tank of fuel.
            try { dal.AddBus(bus); }
            catch { throw new BusAlreadyInSystemException(bus.BusLicense, "Bus Already found in Bus Fleet"); }
        }

        public void DeleteFromBusFleet(Bus bus)
        {
            try { dal.DeleteBus(bus.BusLicense); }
            catch { throw new BusMissingFromSystemException(bus.BusLicense, "Bus cannot be found in the BUs Fleet"); }
           
        }

        public IEnumerable<BusFleet> GetBusFleetWithSelectedFields(Func<BusFleet, object> generate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BusFleet> GetEntireBusFleet()
        {
            return from Bus in dal.GetAllBuses()
                   select BusFleet; //?
        }

        //public IEnumerable<BO.Course> GetAllCourses()
        //{
        //    return from crsDO in dl.GetAllCourses()
        //           select courseDoBoAdapter(crsDO);
        //}

        public BusFleet GetFleet() //???
        {
            var fleet = dal.GetAllBuses();
            return fleet as BusFleet;
        }

        public void UpdateBusFleet(Bus bus)
        {
            try { dal.UpdateBus(bus); }
            catch { throw new BusMissingFromSystemException(bus.BusLicense, "Bus cannot be found in the BUs Fleet"); }
        }
        #endregion
    }
}
