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
    /// </summary>
    /// 
    public enum DriverFirstName { Unknown, David, Sara, Mark, Ruth, Ron, Debbie };
    public enum DriverLastName { Unknown, Johnson, Cohen, Tailor, Brent, Owen, Harper };
    public class Staff : ICloneable
    {
        /* PROPERTIES */

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

        // Driver ID Number:
        // TZ with the first two letters of first and last name added to end --> 000000000LNFN
        private string driverID;

        public string BusDriverID
        {
            get { return driverID; }
            set { driverID = value; }
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

        /* CONSTRUCTORS */

        // Default Ctor:
        public Staff()
        {
            BusDriverFirst = DriverFirstName.Unknown;
            BusDriverLast = DriverLastName.Unknown;
            BusDriverID = 0;
            BusDriverCellNo = "NotRegistered";
            BusDriverAge = 18;
        }

        // Ctor:
        public Staff( DriverFirstName firstN, DriverLastName lastN, int IDdriver, string cellDriver, int ageDriver)
        {
            BusDriverFirst = firstN;
            BusDriverLast = lastN;
            BusDriverID = IDdriver;
            BusDriverCellNo = cellDriver;
            BusDriverAge = ageDriver;
        }

        /* OVERRIDE TOSTRING */

        public override string ToString()
        {
            return "Driver Information: \n" + BusDriverLast + ", " + BusDriverFirst + "\n Age:" + BusDriverAge + "\n ID:" + BusDriverID + "\n Cell Phone Number:" + BusDriverCellNo; ;
        }
    }
}
