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
        #region Singelton
        //priavet and using statiç
        //dal factroy creates the dal object once and only once!
        #endregion
        //must implement IDAL
        //gets data and adds to data source then we add a reference to DS
        //add crud methods from IDAL as have to implement the interface!!
    }
}
