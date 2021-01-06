using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using BO;
using DALApi; //i think this should be here?

namespace BL
{
    public class BLObject : IBL
    {
        static Random rnd = new Random();
        /// <summary>
        /// Implementing the Singelton Design Method so that only one instance of each collection will be created
        /// This ensures that all the data is in one place and that we do not have any extra copies of collections
        /// </summary>
        #region Singleton
        static readonly BLObject instance = new BLObject();
        static BLObject() { }
        BLObject() { }
        public static BLObject Instance { get => instance; }

        public IEnumerable<BusFleet> GetEntireBusFleet()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BusFleet> GetBusFleetWithSelectedFields(Func<BusFleet, object> generate)
        {
            throw new NotImplementedException();
        }

        public void AddToBusFleet(BusFleet fleet)
        {
            throw new NotImplementedException();
        }

        public BusFleet GetFleet()
        {
            throw new NotImplementedException();
        }

        public void UpdateBusFleet(BusFleet fleet)
        {
            throw new NotImplementedException();
        }

        public void DeleteBusFleet()
        {
            throw new NotImplementedException();
        }
        #endregion

        //IDAL dal = DLFactory.GetDL();
    }
}
