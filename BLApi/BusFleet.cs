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
    /// Description: This class contains information on the companies bus fleet - all the active buses there mainetenance, refueling, repairs travels etc
    /// </summary>
    public class BusFleet
    {
        /// <summary>
        /// Private BLObject "buses" -
        /// Value Type: IEnumerable<Bus>, therefore contains the following properties -License; Mileage; Fuel; RegDate; MaintenanceDate; Status; Erased - and is countable 
        /// It defines the fleet of buses
        /// </summary>
        private IEnumerable<Bus> buses;
        /// <summary>
        /// Public BLObject "busesInFleet" -
        /// Value Type: IEnumerable<Bus>, therefore contains the following properties -License; Mileage; Fuel; RegDate; MaintenanceDate; Status; Erased - and is countable 
        /// It defines the fleet of buses
        /// </summary>
        public IEnumerable<Bus> busesInFleet
        {
            get { return buses; }
            set { buses = value; }
        }

        ///NOT SURE ABOUT ADDING A LIST OF STAFF ALONGSIDE BUSES ???

        ///// <summary>
        ///// Private BLObject "driver" -
        ///// Value Type: DO.Staff, therefore contains the following properties -fisrtName; lastName; DriverID; CellNum; Age.
        ///// It defines the driver associated with the specific bus in the fleet
        ///// </summary>
        //private Staff driver;

        ///// <summary>
        ///// Public BLObject "busDriver" -
        ///// Value Type: DO.Staff, therefore contains the following properties -fisrtName; lastName; DriverID; CellNum; Age.
        ///// It defines the drivers.
        ///// </summary>
        //public Staff busDriver
        //{
        //    get { return driver; }
        //    set { driver = value; }
        //}


        //License; Mileage; Fuel; RegDate; MaintenanceDate; Status; Erased	 - BUS
        //fisrtName; lastName; DriverID; CellNum; Age - Staff [the driver associated with that BUS]

    }
}
