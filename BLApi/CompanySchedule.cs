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
    /// Description: This class is the schedule of all the bus routes in the Company
    /// Based on: BO.ScheduleOfRoute
    /// </summary>
    public class CompanySchedule
    {
        /// <summary>
        /// Private BLObject "routeSchedule"
        /// Value Type - BO.ScheduleOfRoute.
        /// This is a countable list of the schedules for each route in the company
        /// </summary>
        private IEnumerable<ScheduleOfRoute> routeSchedule;
        /// <summary>
        /// Public BLObject "CompanyRouteSchedule"
        /// Value Type - BO.ScheduleOfRoute.
        /// This is a countable list of the schedules for each route in the company
        /// </summary>
        public IEnumerable<ScheduleOfRoute> CompanyRouteSchedule
        {
            get { return routeSchedule; }
            set { routeSchedule = value; }
        }

    }
}
