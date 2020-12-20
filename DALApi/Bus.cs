using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum Status { Available, Refueling, AtService, Traveling};
    public enum DriverFirstName { Unknown, David, Sara, Mark, Ruth, Ron, Debbie};
    public enum DriverLastName { Unknown, Johnson, Cohen, Tailor, Brent, Owen, Harper};
    public class Bus :ICloneable
    {
        /* PROPERTIES */
        
        // License Number:
        // Note: must be in the correct format based on registration date
        private string license;

        public string BusLicense   
        {
            get { return license; }
            set { license = value; }
        }

        // Registration Date:
        // Note: is DateTime cosidered simple enough to use??
        private DateTime regDate;

        public DateTime BusRegDate
        {
            get { return regDate; }
            set { regDate = value; }
        }

        // Mileage:
        // Note: cannot decrese or be a negative value
        private int mileage;

        public int BusMileage
        {
            get { return mileage; }
            set { mileage = value; }
        }

        // Fuel Amount (Tank):
        // Note: Cannot exceed 1200 Liters (this is a full tank)
        private float fuel;

        public float BusFuel
        {
            get { return fuel; }
            set { fuel = value; }
        }

        // Bus Status 
        private Status status;

        public Status BusStatus
        {
            get { return status; }
            set { status = value; }
        }

        //additional info as desired...

        // Bus Driver (note first element in Enum is Unkown therefore re=andomisation should start from 1)
        //firstName:
        private DriverFirstName firstName;

        public DriverFirstName BusDriverFirst
        {
            get { return firstName; }
            set { firstName = value; }
        }
        //LastName:
        private DriverLastName lastName;

        public DriverLastName BusDriverLast
        {
            get { return lastName; }
            set { lastName = value; }
        }

        /* CONSTRUCTORS */

        //Default ctor:
        Bus()
        {
            BusLicense = "Unknown";
            BusRegDate = DateTime.Now;
            BusMileage = 0;
            BusFuel = 1200;
            BusStatus = Status.Available;
            BusDriverFirst = DriverFirstName.Unknown;
            BusDriverLast = DriverLastName.Unknown;
        }

        //ctor
        Bus( string busLicense, DateTime busReg, int busMileage, int busFuel, Status busState, DriverFirstName busFirstN, DriverLastName busLastN)
        {
            BusLicense = busLicense;
            //do we check the licesnce num here?
            
            BusRegDate = busReg;

            if (busMileage < 0)
            {
                //throw exception to BL Layer to deal with inc=valid input
            }
            else BusMileage = busMileage;

            if (busFuel > 1200 || busFuel < 0)
            {
                //throw exception to BL Layer to deal with inc=valid input
            }
            else BusFuel = busFuel;

            BusStatus = busState;
            BusDriverFirst = busFirstN;
            BusDriverLast = busLastN;
        }
    }
}
