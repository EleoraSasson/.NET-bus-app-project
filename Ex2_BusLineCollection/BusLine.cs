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

namespace Ex2_BusLineCollection
{
    public enum Areas { Unknown, North_Golan, North_Haifa, Center_TelAviv, Center_Jerusalem, South_BeerSheva, South_Eilat, National }; //unknown indicates area has not yet been set & National implies bus goes throughout country
    class BusLine : BusRouteInfo, IEnumerable, IEnumerator, IComparable
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
        private List<BusRouteInfo> stations;

        public List<BusRouteInfo> setStations
        {
            get { return stations; }
            set
            {
                var stops = new List<BusRouteInfo>();
                this.stations = stops;
            }
        }


        //IEnumerator and IEnumerable require these methods.

        int position = 0;
        
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        //IEnumerator
        public bool MoveNext()
        {
            position++;
            return (position < stations.Count);
        }

        //IEnumerable
        public void Reset()
        {
            position = 0;
        }

        //IEnumerable
        public object Current
        {
            get { return stations[position]; }
        }
      

        /*CLASS CTORS*/
        public BusLine() : base() //default ctor
        {
            BusLineNum = 0;
            BusArea = 0; //aka Unknown
            firstStop = new BusRouteInfo();
            lastStop = firstStop;
        }

        public BusLine(int lineN, Areas area, List<BusRouteInfo> b, BusRouteInfo first, BusRouteInfo last, float dist, TimeSpan t, int key, double lat, double lon, string adr) : base(dist, t, key, lat, lon, adr)
        {
            BusLineNum = lineN;
            BusArea = area;
            firstStop = first;
            lastStop = last;
        }

        /*CLASS METHODS*/

        /*A overriding ToString*/
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (var BusStationKey in stations) //create a list //see if it still works
            {
                str.AppendLine(BusStationKey.ToString());
            }
            for (int i = stations.Count - 1; i >= 0; i--) //iterates through the list backwars
            {
                str.AppendLine(stations[i].BusStationKey.ToString());
            }

            return ("Bus Line: " + BusLineNum + ": " + BusArea + str.ToString()); //check if it works
                                                                                  // return string.Join(",", this.Employees.Select(employee => $"Employee: {employee.FullName}"));

        }

        /*B adding/removing stops*/

        /* Method: addStop
         * Description: adds a bus station to a route
         * Return Type: void
         */
        public void addStop(List<BusRouteInfo> bus)
        {
            var stop = new BusStop();
            setKey(bus, stop); //sets key of station
            setLocation(stop); //sets location and speed
            setAddress(bus, stop); //sets address of station
            setTotalTime(bus, stop);
            if (bus.First() == null) //if first element is empty
            {
                stations.Add((BusRouteInfo)stop); //add the new stop to the list of stops
                firstStop = (BusRouteInfo)stop; // the stop you added is also the first stop of route
                lastStop = (BusRouteInfo)stop; // it is also the last stop on the route
            }
            else // the station you add now will become the last stop of the route
            {
                bus.Add(stop as BusRouteInfo); //add the new stop to the list of stops
                lastStop = (stop as BusRouteInfo);
            }
        }

        /* Method: removeStop
        * Description: Searches for busKey in a given route and if found removes the bus station with that bus key.
        * Return Type: void 
        */
        public void removeStop(int busKey)
        {
            for (int i = 0; i < stations.Count; i++) //search for stop
            {
                if (busKey == stations[i].BusStationKey)
                {
                    stations.RemoveAt(i); //remove stop
                    return;
                }
            }
            throw new ArgumentException("Stop to remove is not in the line.");//if bus is not found
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

        public void setTotalTime(List<BusRouteInfo> bus, BusStop stop)
        {
            //int index = bus.FindIndex(busS => busS.BusStationKey == stop.BusStationKey);
            for (int i = 0; i < bus.Count; i++)
            {
                if (bus[i].BusStationKey == stop.BusStationKey)//if found increment time
                {
                    BusTotalTime += bus[i].BusTime;
                }
                else { }
            }

        }

        //IComaparable
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
        public BusLine routeLine(BusStop a, BusStop b)
        {
            foreach (BusLine bus in stations) //we don't have a list of busLines?
            {
                if (bus.firstStop == a && bus.lastStop == b)
                    return bus;
            }
            throw new ArgumentException("Error: Invalid Input.");
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
