using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum Status { Available, Refueling, AtService, Traveling};
  
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
        private DateTime regDate;

        public DateTime BusRegDate
        {
            get { return regDate; }
            set { regDate = value; }
        }

        // Maintenance Date (keeps the last time the bus went to maintenance):
        private DateTime maintenanceDate;

        public DateTime BusMaintenanceDate
        {
            get { return maintenanceDate; }
            set { maintenanceDate = value; }
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

        // Bus Erased [if bus is no longer in bus company service it is true that it is erased, else false]:
        private bool erased;

        public bool BusErased
        {
            get { return erased; }
            set { erased = value; }
        }


        /* CONSTRUCTORS */

        //Default ctor:
        public Bus()
        {
            BusLicense = "XX-XXX-XX";
            BusRegDate = DateTime.Now;
            BusMaintenanceDate = DateTime.Now;
            BusMileage = 0;
            BusFuel = 1200;
            BusStatus = Status.Available;
            BusErased = false; //by default it is part of bus company fleet
        }

        //ctor
        public Bus( string busLicense, DateTime busReg, DateTime busMaintenance, int busMileage, int busFuel)
        {
            BusLicense = busLicense;
            BusRegDate = busReg;
            BusMaintenanceDate = busMaintenance;

            if (busMileage < 0)
            {
                //throw exception to BL Layer to deal with invalid input
            }
            else BusMileage = busMileage;

            if (busFuel > 1200 || busFuel < 0)
            {
                //throw exception to BL Layer to deal with invalid input
            }
            else BusFuel = busFuel;

            //when a bus is added it is automatically in the company fleet therefore, erased boolean value need not be passed.
        }

        /* OVERRIDE TOSTRING */

        public override string ToString()
        {
            return "Bus Information:\n License Number:" + BusLicense + "\n Registration Date:" + BusRegDate + "\n Last Maintenance Date:" + BusMaintenanceDate + "\n Mileage Reading:" + BusMileage + "\n Fuel Amount:" + BusFuel + "\n Status of Bus:" + BusStatus + "\n";
        }
    }
}
