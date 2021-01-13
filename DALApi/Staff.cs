using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// This added class is a record of all the information of our staff (drivers)
    /// We added this so that the manager can know more information of the driver. It also enables us to assign the driver to a specific route
    /// Note: even though most fields are named Driver___ a staff memeber can be admin manager etc and not be a driver
    /// </summary>
    /// 
    public enum DriverFirstName { Unknown, David, Sara, Mark, Ruth, Ron, Debbie };
    public enum DriverLastName { Unknown, Johnson, Cohen, Tailor, Brent, Owen, Harper };
    public enum Position { Driver, Manager};
    public class Staff : ICloneable
    {
        /* PROPERTIES */

        // Driver ID Number:
        // TZ with the first two letters of first and last name added to end --> 000000000LNFN (identifying feature)
        private string driverID;

        public string BusDriverID
        {
            get { return driverID; }
            set { driverID = value; }
        }
        
        //returns true if the user is an administrator
        private bool admin;

        public bool isAdmin
        {
            get { return admin; }
            set { admin = value; }
        }

        //Password:
        // Needed to login to system (only if the user is an admin)
        private string password;

        public string LoginPassword
        {
            get { return password; }
            set { password = value; }
        }

        // Driver First Name:
        private DriverFirstName firstName;

        public DriverFirstName BusDriverFirst
        {
            get { return firstName; }
            set { firstName = value; }
        }

        // Driver Last Name:
        private DriverLastName lastName;

        public DriverLastName BusDriverLast
        {
            get { return lastName; }
            set { lastName = value; }
        }

        //number of lines driven by this driver
        private int lines;

        public int StaffNoOfLines
        {
            get { return lines; }
            set { lines = value; }
        }

        // Driver Cell-Phone Number:
        private string cellNo;

        public string BusDriverCellNo
        {
            get { return cellNo; }
            set { cellNo = value; }
        }

        // Driver age (if over 65 driver is retired, note for bday) 
        private int age;

        public int BusDriverAge
        {
            get { return age; }
            set { age = value; }
        }

        //Position in Company:
        private Position position;

        public Position StaffPosition
        {
            get { return position; }
            set { position = value; }
        }

        //Years worked in company
        private int yrsWorked;

        public int StaffYrsWorked
        {
            get { return yrsWorked; }
            set { yrsWorked = value; }
        }

        /* OVERRIDE TOSTRING */

        public override string ToString()
        {
            return "Driver Information: \n" + BusDriverLast + ", " + "Administrator" + isAdmin.ToString() + BusDriverFirst + "\n Age:" + BusDriverAge + "\n ID:" + BusDriverID + "\n Cell Phone Number:" + BusDriverCellNo + "/n Position in Company:" + StaffPosition + "/n Number of years at company:" + StaffYrsWorked + "/n Login Password:" + LoginPassword; 
        }
    }
}
