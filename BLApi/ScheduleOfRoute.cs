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
    /// Description: This class contains information about the schedule of a bus route (includes time of last lines and first lines)
    /// Based on: BusRoute + LineLeaving + Staff
    /// </summary>
    public class ScheduleOfRoute
    {
        /// <summary>
        /// Private BLObject "route"
        /// Value Type - BO.BusRoute.
        /// This is an entire route
        /// </summary>
        private BusRoute route;
        /// <summary>
        /// Public BLObject "CurrentRoute"
        /// Value Type - BO.BusRoute.
        /// This is an entire route
        /// </summary>
        public BusRoute CurrentRoute
        {
            get { return route; }
            set { route = value; }
        }
        /// <summary>
        /// Private BLObject "schedule"
        /// Value Type - DO.LineLeaving.
        /// This is the schedule for the current route
        /// </summary>
        private LineLeaving schedule;
        /// <summary>
        /// Public BLObject "RouteSchedule"
        /// Value Type - DO.LineLeaving.
        /// This is the schedule for the current route
        /// </summary>
        public LineLeaving RouteSchedule
        {
            get { return schedule; }
            set { schedule = value; }
        }
        /// <summary>
        /// Private BLObject "staff"
        /// Value Type - DO.Staff.
        /// This is the driver assigned to the specific route
        /// </summary>
        private Staff staff;
        /// <summary>
        /// Public BLObject "SelectedStaff"
        /// Value Type - DO.Staff.
        /// This is the driver assigned to the specific route
        /// </summary>
        public Staff SelectedStaff
        {
            get { return staff; }
            set { staff = value; }
        }
    }
}
