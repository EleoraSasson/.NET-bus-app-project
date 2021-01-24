using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO
{
    /// <summary>
    /// Type: Business Object. 
    /// Description: This class contains information pertinant to the admin staff and is visible to managers only. 
    /// It contains information such as busdrivers; retirement plans, and schedules.
    /// </summary>
    public class AdminPortal
    {
        private Admin admin;

        public Admin adminPortal
        {
            get { return admin; }
            set { admin = value; }
        }

        //public override string ToString()
        //{
        //    return AdminDriver.BusDriverFirst + AdminDriver.BusDriverLast + ", with ID " + AdminDriver.BusDriverID; 
        //}
    }
}
