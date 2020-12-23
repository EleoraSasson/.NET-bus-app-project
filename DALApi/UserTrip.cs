using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class UserTrip :ICloneable
    {
        /* PROPERTIES */

        // Travel ID
        // User Name
        // Line ID
        // Bording Station ID
        // Time Borded
        // Alighting Station ID
        // Alighting Time

        /* CONSTRUCTORS */

        // Default ctor:
        public UserTrip()
        {

        }

        // ctor:
        public UserTrip(int RelevantParam)
        {

        }

        /* OVERRIDE TOSTRING*/

        public override string ToString()
        {
            return "Travel Information: \n [username], travel ID [travelID], is abord line [lineID] from bording station [BordingID] which departed at [time borded] ; to travel to [alightingID] and arrive at [alighting time].\n"; 
        }
    }
}
