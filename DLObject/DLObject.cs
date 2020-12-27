using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALApi; //referance to DALApi interface
using DS; //reference to Data Structure
using DO; //reference to Data Object
//using DO might need to access some things from the interface

namespace DAL //might need to be DL
{
    public class DLObject : IDAL
    {
        //will implement IDAL
        //we want only one copy of our data
        //we want everything to write to the same thing...the way we do this is with the Singleton pattern
        //use singelton pattern
        static Random rnd = new Random();
        #region Singleton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }
        DLObject() { }
        public static DLObject Instance => instance;
        #endregion
        //must implement IDAL
        //gets data and adds to data source then we add a reference to DS
        //add crud methods from IDAL as have to implement the interface!!
    }
}
