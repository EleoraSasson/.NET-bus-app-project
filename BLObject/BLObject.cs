using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
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
        #endregion

        //IDAL dal = DLFactory.GetDL();
    }
}
