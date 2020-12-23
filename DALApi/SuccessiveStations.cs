using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class SuccessiveStations : ICloneable
    {
        /* PROPERTIES */

        // Station Code 1:
        private int code1;

        public int StationCode1
        {
            get { return code1; }
            set { code1 = value; }
        }

        // Station Code 2:
        private int code2;

        public int StationCode2
        {
            get { return code2; }
            set { code2 = value; }
        }

        // Distance:
        private int distance;

        public int StationDistance
        {
            get { return distance; }
            set { distance = value; }
        }

        // Average Travel Time:
        private TimeSpan aveTravelT;

        public TimeSpan StationAveTravelT
        {
            get { return aveTravelT; }
            set { aveTravelT = value; }
        }

        /* CONSTRUCTORS */

        // default ctor:
        public SuccessiveStations()
        {
            StationCode1 = 0;
            StationCode2 = 1;
            StationDistance = 1; //set to 1km
            var aveTravel = new TimeSpan(00,10,00);
            StationAveTravelT = aveTravel; //set to 10 minutes
        }

        // ctor:
        public SuccessiveStations( int stCode0, int stCode1, int stDistance, TimeSpan stAveTime)
        {
            StationCode1 = stCode0;
            StationCode2 = stCode1;
            StationDistance = stDistance;
            StationAveTravelT = stAveTime;
        }

        /* OVERRIDE TOSTRING */
        public override string ToString()
        {
            return "Between stations " + StationCode1 + " and " + StationCode2 + ",there is a distance of " + StationDistance + "and the average travel time between them is " + StationAveTravelT + ".\n"; 
        }
    }
}
