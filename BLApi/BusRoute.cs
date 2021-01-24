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
     /// Description: This class contains information about bus routes - the route with its start, end and successive stops etc. Visible to Admin.
     /// Based on: BusLine + LineStation
     /// </summary>
    public class BusRoute
    {
        /// <summary>
        /// Private BLObject "route" -
        /// Value Type: DO.BusLine, therefore contains the following properties - LineID; lineNo; Region; StartStop; EndStop; leaving; Erased.
        /// It defines the outline of a route in the companies travel system.
        /// </summary>
        private BusLine route;
        /// <summary>
        /// Public BLObject "Route" -
        /// Value Type: DO.BusLine, therefore contains the following properties - LineID; lineNo; Region; StartStop; EndStop; leaving; Erased.
        /// It defines the outline of a route in the companies travel system.
        /// </summary>
        public BusLine Route
        {
            get { return route; }
            set { route = value; }
        }
        /// <summary>
        /// Private BLObject "routeStops" -
        /// Value Type: Ienumerable<LineStation>, therefore contains the following properties - lineID; Stationcode; StationNum - and can be counted.
        /// It defines the list of stops on a route in the companies travel system.
        /// </summary>
        private IEnumerable<LineStation> routeStops;
        /// <summary>
        /// Public BLObject "routeStops" -
        /// Value Type: Ienumerable<LineStation>, therefore contains the following properties - lineID; Stationcode; StationNum - and can be counted.
        /// It defines the list of stops on a route in the companies travel system.
        /// </summary>
        public IEnumerable<LineStation> RouteStops
        {
            get { return routeStops; }
            set { routeStops = value; }
        }

        /* OVERRIDE TOSTRING */
        //public override string ToString()
        //{
        //    return "Bus Route " + route.BusLineNo + " with the ID " + route.BusLineID + " located in the " + route.BusRegion + " region:"
        //        + " Starting Station: " + route.BusStart + "\n Ending Station: " + route.BusEnd + "\n Number of Total Stations Visted:"
        //        + routeStops.Count() + "\n Line has been Erased = " + route.LineErased + "\n";
        //}

        public override string ToString()
        {
            return "( Route #" + route.BusLineNo + " ) \n Start station: " + route.BusStart  + "\n End station: " + route.BusEnd + "\n Location: " + route.BusRegion;
        }
    }
}
