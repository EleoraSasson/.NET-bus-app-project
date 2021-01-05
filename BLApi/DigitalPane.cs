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
    /// Description: This class contains information about upcoming bus lines to a particualr bus stop. It will be visible to the travelers.
    /// </summary>
    public class DigitalPane
    {
        // successive stations
        // bus routes BO object 
        // bus stop
        // lineleaving 

        //list of the lines that passthrough a given station and how long it will take fpr those lines to come
        //list of stations it will go to next and has gone to previously

        /* PROPERTIES */

        /// <summary>
        /// Private BLObject "thisStop"
        /// Value Type: DO.BusStop - contains a station code, station name, location, active or not
        /// This defines the current stop where the user is  at when looking at the digital pane
        /// </summary>
        private BusStop thisStop;
        /// <summary>
        /// Public BLObject "DigPaneThisStop"
        ///  Value Type: DO.BusStop - contains a station code, station name, location, active or not
        /// This defines the current stop where the user is  at when looking at the digital pane
        /// </summary>
        public BusStop DigPaneThisStop
        {
            get { return thisStop; }
            set { thisStop = value; }
        }



        /// <summary>
        /// Private BLObject busRoutes.
        /// Value Type: IEnumerable<BusRoutes>, contains a countable list of DO.BusRoutes - therefore contains routes and routeStops
        /// It collates a list of all Bus Routes
        /// </summary>
        private IEnumerable<BusRoutes> busRoutes;
        /// <summary>
        /// Public BLObject DigPaneBusRoutes.
        /// Value Type: IEnumerable<BusRoutes>, contains a countable list of DO.BusRoutes - therefore contains routes and routeStops
        /// It collates a list of all Bus Routes
        /// </summary>
        public IEnumerable<BusRoutes> DigPaneBusRoutes
        {
            get { return busRoutes; }
            set { busRoutes = value; }
        }

        ///??? succsessive stations



    }
}
