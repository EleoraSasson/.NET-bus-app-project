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
        private IEnumerable<Staff> admin;
        /// <summary>
        /// Public BLObject "administrators"
        /// Value Type - BO.administrators.
        /// This is a countable list of the administrators in the company
        /// </summary>
        public IEnumerable<Staff> administrators
        {
            get { return admin; }
            set { admin = value; }
        }

        //needs to get from DO.Staff so that it can contain a list of all the admin
        //should have there passwords stored
        //needs methods to authenticate a admin user 
        //needs method to add an admin user to the system
        //needs to display the admins personal info if requested (get)
        //needs a method to determine if you are a manger or a driver

    }
}
