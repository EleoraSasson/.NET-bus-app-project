using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALApi;
using DS;
//using DO might need to access some things from the interface

namespace DL //DAL
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

        //gets data and adds to data source then we add a reference to DS
    }
}
