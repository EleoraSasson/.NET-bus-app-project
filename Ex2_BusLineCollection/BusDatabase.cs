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
    //*BUS_ROUTES IN DATABASE*//


    class BusDatabase : BusLine, IEnumerator,IEnumerable//object made: database of bus routes (list of selected lines)
    {

        /*CLASS MEMBERS*/

        private List<BusLine> routes;

        public List<BusLine> setRoutes
        {
            get { return routes; }
            set
            {
                var Route = new List<BusLine>();
                this.routes = Route;
            }
        }
       

        //IEnumerable implementation

        int position = -1;
        //IEnumerator and IEnumerable require these methods.
        public new IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
        //IEnumerator
        public new bool MoveNext()
        {
            position++;
            return (position < routes.Count);
        }
        //IEnumerable
        public new void Reset()
        {
            position = 0;
        }
        //IEnumerable
        public new object Current
        {
            get { return routes[position]; }
        }
       
        /*CLASS CTOR*/
        public BusDatabase() : base() {} //default ctor
        public BusDatabase(int lineN, Areas area, List<BusRouteInfo> b, BusRouteInfo first, BusRouteInfo last, float dist, TimeSpan t, int key, double lat, double lon, string adr) : base(lineN, area, b, first, last, dist, t, key, lat, lon, adr) { } //ctor

        /*A add/remove line*/
        /* Method: addLine
        * Description: recieves a line number and adds the line to the database (keeping in mind that each line appears twice - to and from )
        * Return Type: void
        */
        public void addLine()
        {
            Console.WriteLine("Please enter the line number you wish to add: ");
            int lineNum = Convert.ToInt32(Console.ReadLine());
            foreach (BusLine line in routes)
            {
                if (line.BusLineNum == lineNum)
                {
                    throw new ArgumentException("Error: line already exists.\n");
                }
            }
               
            BusLine busLine = new BusLine();
            routes.Add(busLine);
        }

        /* Method: removeLine
        * Description: removes a bus line from the collection
        * Return Type: void
        */
        public void removeLine(int lineToRemove)
        {
            bool notFound = true;

            for (int i = 0; i < routes.Count; i++) //search for stop
            {
                if (lineToRemove == routes[i].BusLineNum)
                {
                    routes.RemoveAt(i); //remove line
                    notFound = false;
                }
            }

            if (notFound) // it hasn't been found
            {
                Console.WriteLine("Error: Bus stop does not exist in the route.");
            }
        }

        /*B returns list of bus lines passing through a given station*/

        /* Method: linesThroughStation
         * Description: recieves bus station key and returns a list of bus routes that pass through that station
         * Return Type: List<BusRoutes>
         */
        public List<BusLine> linesThroughStation(int StationKey)
        {
            bool keyMatch = false;
            var LinesThruStation = new List<BusLine>();
            foreach (BusStop busStop in routes) // for every bus in our list of bus stations
            {
                if (busStop.BusStationKey != StationKey) // if we don't find a matching key
                {
                    keyMatch = true;
                }
                else//key found - now search Bus Routes to find any bus lines that go through that station
                {
                    foreach (BusLine busRoutes in routes) //for every busline in our list of busroutes
                    {
                        if (busRoutes.BusStationKey == StationKey)
                        {
                            BusLine bLine = new BusLine();//cctor??
                            LinesThruStation.Add(bLine);
                        }
                    }
                }
            }
            if (keyMatch)//if key not found bus stop does not exist
            {
                Console.WriteLine("Error: Bus Key does not match any Bus Station in the directory.\n");
            }

            return LinesThruStation;///return list of lines that pass through the station
        }

        /*C sorts lines according to time*/
        /* Method: sortLines
        * Description: sorts the bus lines according to the total time of the route, from shortest time to longest time
        * Return Type: void
        */
        public void sortLines(List<BusDatabase> busRoutes)
        {
            for (int i = 0; i < routes.Count; i++)
            {
                var longerRoute = busRoutes[i].CompareTo(busRoutes[i + 1]);

            }
        }

        /*D indexer - checks for line number*/

        /* Method: BusIndexer
        * Description: Indexer that receives a line number and returns the instance. If no such line exits it returns throws an exception.
        * Return Type: int
        */
        public int BusIndexer(int lineNum) //if this work we should call it in our main and other methods aswell
        {
            int index = 0;
            try //locate line and find index
            {
                index = routes.FindIndex(bus => bus.BusLineNum == lineNum);
            } //line not found
            catch { Console.WriteLine("Error: no such line exists"); throw; }
            return index;
        }
    }
}
