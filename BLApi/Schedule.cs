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
    /// Description: This class contains information pertinant to the admin staff and is visible to managers only. 
    /// It contains information of the bus & line schedules - what bus is assigned to what travel routes.
    /// </summary>
    public class Schedule
    {
        //RoadID; License; Running; FormalDT; ActualDT; Passed; TransitT; Arrival T	 - BusOnTrip
        //lineID; firstline; numofLines; LastLines	 - LineLeaving  
    }
}
