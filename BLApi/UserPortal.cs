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
    /// Description: This class contains information about the user which is visbale to and relevant for the admin staff only.
    /// </summary>
    public class UserPortal
    {
        private User user;

        public User userPortal
        {
            get { return user; }
            set { user = value; }
        }

        //public override string ToString()
        //{
        //    return userPortal.userLast + "," + userPortal.userFirst + "- permission:" + userPortal.adminPermission; 
        //}
    }
}
