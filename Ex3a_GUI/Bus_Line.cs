using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;//this can be used to validate strings
using System.Xml;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Runtime.Remoting;
using System.Data;
using System.Device.Location; //is used for location coordinates
using System.Collections;
using System.CodeDom;

namespace Ex3a_GUI
{
    public enum Areas { Unknown, North_Golan, North_Haifa, Center_TelAviv, Center_Jerusalem, South_BeerSheva, South_Eilat, National }; //unknown indicates area has not yet been set & National implies bus goes throughout country
    class BusLine : IComparable, IEnumerable
    {
        /*CLASS MEMBERS*/

        //line number:
        private int lineNum;
        public int BusLineNum
        {
            get { return lineNum; }
            set { lineNum = value; }
        }

        //area of line:
        private Areas area;
        public Areas BusArea
        {
            get { return area; }
            set { area = value; }
        }

        //first station on bus route:
        private BusRouteInfo first;

        public BusRouteInfo firstStop
        {
            get { return first; }
            set { first = value; }
        }

        //last station on bus route:
        private BusRouteInfo last;

        public BusRouteInfo lastStop
        {
            get { return last; }
            set { last = value; }
        }

        //stations:
        public List<BusRouteInfo> stations;

        /*CLASS CTORS*/

        public BusLine(int lineNum, Areas lineArea)
        {
            BusLineNum = lineNum;
            BusArea = lineArea;
            stations = new List<BusRouteInfo>();
        }
        public BusLine()  //default ctor
        {
            Random lnum = new Random();
            Random area = new Random();

            var randLnum = lnum.Next(1, 89); //bus line 1 to 89
            var randArea = area.Next(1, 7); //randomly select an area

            BusLineNum = randLnum;
            BusArea = (Areas)randArea;
        }

        //IEnumerator and IEnumerable Implementation:
        public IEnumerator GetEnumerator()
        {
            return new AIEnumerator(this);
        }

        private class AIEnumerator : IEnumerator
        {
            private BusLine instance;
            private int position = -1;

            public AIEnumerator(BusLine inst)
            {
                this.instance = inst;
            }

            //IEnumerator
            public bool MoveNext()
            {
                position++;
                return (position < instance.stations.Count);
            }
            //IEnumerable
            public void Reset()
            {
                position = -1;
            }
            //IEnumerable
            public object Current
            {
                get { return instance.stations[position]; }
            }
        }


        /*CLASS METHODS*/

        /*A overriding ToString*/
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (var BusStationKey in stations) //create a list /
            {
                str.AppendLine(BusStationKey.ToString());
            }
            //for (int i = stations.Count - 1; i >= 0; i--) //iterates through the list backwards
            //{
            //    str.AppendLine(stations[i].BusStationKey.ToString());
            //}

            return ("Bus Line: " + BusLineNum + "\n" + "Area: " + BusArea + "\n" + str.ToString());


        }

        /*B adding/removing stops*/

        /* Method: addStop
            * Description: adds a bus station to a line
            * Return Type: void
            */
        public void addStop()
        {
            var stop = new BusRouteInfo();
            int k = stop.setKey(stations);
            stop = new BusRouteInfo(k);
            //setTotalTime(bus, stop);
            if (!(stations.Any())) //if first element is empty
            {
                stations.Add(stop); //add the new stop to the list of stops
                firstStop = (stop);// the stop you added is also the first stop of route
                lastStop = (stop); // it is also the last stop on the route
            }
            else // the station you add now will become the last stop of the route
            {
                stations.Add(stop); //add the new stop to the list of stops
                lastStop = (stop);
            }
        }


        /* Method: removeStop
        * Description: Searches for busKey in a given route and if found removes the bus station with that bus key.
        * Return Type: void 
        */
        public void removeStop(int key)
        {
            for (int i = 0; i < stations.Count; i++) //search for stop
            {
                if (key == stations[i].BusStationKey)
                {
                    stations.RemoveAt(i); //remove stop
                    return;
                }
            }
            //throw new ArgumentException("Stop to remove is not in the line.");//if bus is not found

        }

        /*C is station/stop on line*/

        /* Method: isStopOnRoute
        * Description: returns true if the stop is on the route
        * Return Type: bool
        */
        public bool isStopOnRoute(int busKey)
        {
            int index = stations.FindIndex(stop => stop.BusStationKey == busKey);

            if (index >= 0)
            {
                return true;
            }
            else { return false; }
        }

        /*D distance between stops*/

        /* Method: routeDistance
            * Description: returns the distance between two stops given their locations
            * Return Type: float
            */

        public double routeDistance(List<BusRouteInfo> busLines, int keyStart, int keyEnd)
        {
            double totalDist = 0;

            int startIndex = stations.FindIndex(bus => bus.BusStationKey == keyStart);
            int endIndex = stations.FindIndex(bus => bus.BusStationKey == keyEnd);
            for (int i = startIndex; i < endIndex; i++)
            {
                var loc_x = busLines[i].BusLocation;
                var loc_y = busLines[i + 1].BusLocation;
                var distance = loc_x.GetDistanceTo(loc_y);
                totalDist += distance;
            }
            return totalDist;
        }

        /*E travel time between stops*/

        /* Method: routeTime
            * Description: returns the travel time between two stops given their keys
            * Return Type: TimeSpan
            */
        public TimeSpan routeTime(List<BusRouteInfo> busroutes, int keyStart, int keyEnd)
        {
            var travelTime = new TimeSpan();
            int startIndex = stations.FindIndex(bus => bus.BusStationKey == keyStart);
            int endIndex = stations.FindIndex(bus => bus.BusStationKey == keyEnd);
            for (int i = startIndex; i < endIndex; i++)
            {
                travelTime += busroutes[i].BusTime; //increment time as you go along route
            }
            return travelTime;
        }

        private TimeSpan totalTime;

        public TimeSpan BusTotalTime
        {
            get { return totalTime; }
            set { totalTime = value; }
        }

        //IComparable
        public int CompareTo(BusLine x)
        {
            BusLine b = (BusLine)x;
            return BusTotalTime.CompareTo(b.BusTotalTime); //See if it works with the time       
        }

        public BusLine compareLines(BusLine a, BusLine b)
        {
            IComparable c1 = (IComparable)a;
            IComparable c2 = (IComparable)b;
            if (c1.CompareTo(c2) == 0)
            {
                throw new ArgumentException("Same travel time.");
            }
            else if (c1.CompareTo(c2) == 1)
                return a; //a's travel time is greater than b's
            else if (c1.CompareTo(c2) == -1)
                return b;
            else
                throw new ArgumentException("Comparison failed.");
        }

        /*F sub route of the line*/
        public List<BusRouteInfo> routeLine(BusStop a, BusStop b)
        {
            var subRoute = new List<BusRouteInfo>();

            int start = stations.FindIndex(stop => stop.BusStationKey == a.BusStationKey);
            int end = stations.FindIndex(stop => stop.BusStationKey == b.BusStationKey);

            for (int i = start; i < (end + 1); i++)
            {
                subRoute.Add(stations[i]);
            }

            return subRoute;
        }

        /*G comparing routes (IComparable)*/
        public BusLine shortestTravelTime(BusLine a, BusLine b)
        {
            return compareLines(a, b);
        }

        public int CompareTo(object obj)
        {
            return BusTotalTime.CompareTo(obj);
        }

    }
    
}
