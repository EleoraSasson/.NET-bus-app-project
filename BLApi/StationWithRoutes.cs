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
    /// Description: This class contains a BusStop with a list of all the routes that pass through that BusStop/Station
    /// Based on: BusStop + BusRoute
    /// </summary>
    class StationWithRoutes
    {
        /// <summary>
        /// Private BLObject "station" -
        /// Value Type: DO.BusStop, therefore contains the following properties - Code; Location (long and lat); Name; Address; StopActive
        /// It defines a specific Station
        /// </summary>
        private BusStop station;
        /// <summary>
        /// Public BLObject "CurrentStation" -
        /// Value Type: DO.BusStop, therefore contains the following properties - Code; Location (long and lat); Name; Address; StopActive
        /// It defines a specific Station
        /// </summary>
        public BusStop CurrentStation
        {
            get { return station; }
            set { station = value; }
        }
        /// <summary>
        /// Private BLObject "lines" -
        /// Value Type: IEnumerable<BO.BusRoute> , is a countable list of routes defined in the company that pass through
        /// the current station
        /// </summary>
        private IEnumerable<BusRoute> lines;
        /// <summary>
        /// Public BLObject "CurrentLines" -
        /// Value Type: IEnumerable<BO.BusRoute> , is a countable list of routes defined in the company that pass through
        /// the current station
        /// </summary>
        public IEnumerable<BusRoute> CurrentLines
        {
            get { return lines; }
            set { lines = value; }
        }

    }
}
