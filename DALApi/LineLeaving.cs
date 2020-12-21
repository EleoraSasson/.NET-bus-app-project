using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineLeaving :ICloneable
    {
        /* PROPERTIES */

        // Bus Line ID:
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        // Start Time [First Line]:
        // Frequency of Departure [Number of Lines per day]:
        // End Time of Travel (per 24hrs) [Last Line]:

        /* CONSTRUCTORS */

        // Default Ctor:
        public LineLeaving()
        {

        }

        // Ctor:
        public LineLeaving(int param1, bool param2,)
        {

        }
    }
}
