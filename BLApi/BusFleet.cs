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

        //ToString
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
