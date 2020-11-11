//DELETE COMMENTS
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
    class Program
    {
        //*BUS_STOP*//
        class BusStop //object made: Bus Station/Stop
        {
            /*CLASS MEMBERS*/
            private int stationKey; // bus key (to identify bus stop)

            public int BusStationKey
            {
                get { return stationKey; }
                set { stationKey = value; }
            }

            private GeoCoordinate location;  //location information of stops

            public GeoCoordinate BusLocation
            {
                get { return location; }
                set { location = value; }
            }

            public void setLocation(BusStop stop)
            {
                Random rlat = new Random();
                stop.BusLocation.Latitude = rlat.NextDouble() * (33.30 - 31.30) + 31.30; //returns random variable between 31.30 and 33.30 and sets it as latitude
                Random rlong = new Random();
                stop.BusLocation.Longitude = rlong.NextDouble() * (35.50 - 34.30) + 34.30; //returns random variable between 34.3 and 35.5  
                stop.BusLocation.Speed = 6; //around 20 km/hr
            }



            private string address;//physical address

            public string BusAddress
            {
                get { return address; }
                set { address = value; }
            }

            /*CLASS CTORS*/

            public BusStop() //defualt ctor 
            {
                BusStationKey = 000000;
                BusLocation = new GeoCoordinate(0,0);
                BusAddress = "No Address Assigned";
            }

            public BusStop(int key, double lat, double lon, string adr) //ctor
            {
                BusStationKey = key;
                BusLocation.Latitude = lat;
                BusLocation.Longitude = lon;
                BusAddress = adr;
            }

            /*CLASS METHODS*/

            //sets key
            public void setKey(List<BusRouteInfo> bus, BusStop stop)
            {
                bool enterKey = false;
                int key = 0;
                bool exists = false;
                while (enterKey == false)
                {
                    Console.WriteLine("Enter the bus station ID (must be 6 digits): ");
                    key = Convert.ToInt32(Console.ReadLine());
                    while (key < 100000 || key > 1000000) //check if key is valid
                    {
                        Console.WriteLine("Error: Invalid Key - Must be 6 digits long\n");
                        key = Convert.ToInt32(Console.ReadLine());
                    }

                    foreach (var Bstop in bus)
                    {
                        if (Bstop.BusStationKey == key)
                        {
                            Console.WriteLine("ERROR: bus station already exists.");
                        }
                        else { enterKey = true; }
                    }
                }
                stop.BusStationKey = key;
            }
            //sets address
            public void setAddress(List<BusRouteInfo> bus, BusStop stop)
            {
                Console.WriteLine("Enter Physical Address:");
                var address = Console.ReadLine();
                string AdrCheck = @"\d{ 1,3}.?\d{ 0,3}\s[a - zA - Z]{ 2,30}\s[a - zA - Z]{ 2,15}";
                bool verifyAdr = Regex.IsMatch(address, AdrCheck);
                if (verifyAdr == false)
                {
                    Console.WriteLine("Error: Invalid address format");
                    address = Console.ReadLine();
                    verifyAdr = (Regex.IsMatch(address, AdrCheck));
                }
                stop.BusAddress = address;
            }
            public override string ToString()
            {
                return ("Bus Station Code: " + BusStationKey + ", " + BusLocation.Latitude + " N " + BusLocation.Longitude + " E, " + BusAddress);
            }
        }

        //*BUS_STOP W DISTANCE&TIME*//
        class BusRouteInfo : BusStop //object made: a bus stop with distance and time properties added
        {
            /*CLASS MEMBERS*/
            private float distance; //distance from prev stop

            public float BusDistance
            {
                get { return distance; }
                set { distance = value; }
            }

            private TimeSpan time; //time since prev stop

            public TimeSpan BusTime
            {
                get { return time; }
                set { time = value; }
            }

            /*CLASS CTOR*/

            public BusRouteInfo() : base() //default ctor
            {
                BusDistance = 0;
                var t = new TimeSpan(0, 0, 0, 0);
                BusTime = t;
            }

            public BusRouteInfo(float dist, TimeSpan t, int key, double lat, double lon, string adr) : base(key, lat, lon, adr) // ctor
            {
                BusDistance = dist;
                BusTime = t;
            }
        }

        //*BUS_LINES*//
        public enum Areas { Unknown, North_Golan, North_Haifa, Center_TelAviv, Center_Jerusalem, South_BeerSheva, South_Eilat, National }; //unknown indicates area has not yet been set & National implies bus goes throughout country
        class BusLine : BusRouteInfo //IComparable //object made: Lines (list of BusStops) w area info //Do we really need inheritance? else how we implement iComparable
        {
            /*CLASS MEMBERS*/

            private int lineNum; //line number 
            public int BusLineNum
            {
                get { return lineNum; }
                set { lineNum = value; }
            }

            private Areas area; //area of line

            public Areas BusArea
            {
                get { return area; }
                set { area = value; }
            }

            private BusRouteInfo first; //first station on bus route

            public BusRouteInfo firstStop
            {
                get { return first; }
                set { first = value; }
            }

            private List<BusRouteInfo> stations; //stations



            public BusLine(int lineN, Areas area, List<BusRouteInfo> b, BusRouteInfo first, BusRouteInfo last, float dist, TimeSpan t, int key, double lat, double lon, string adr) : base(dist, t, key, lat, lon, adr)
            {
                BusLineNum = lineN;
                BusArea = area;
                firstStop = first;
                lastStop = last;
            }

            //Implementatiion of IEnumerable Interface
            public class BusStations : IEnumerator, IEnumerable
            {
                public List<BusRouteInfo> stations = new List<BusRouteInfo>();
                int position = -1;
                //IEnumerator and IEnumerable require these methods.
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
            }

            private BusRouteInfo last; //last station on bus route

            public BusRouteInfo lastStop
            {
                get { return last; }
                set { last = value; }
            }

            /*CLASS CTORS*/
            public BusLine() : base() //default ctor
            {
                BusLineNum = 0;
                BusArea = 0; //aka Unknown
                firstStop = new BusRouteInfo();
                lastStop = firstStop;
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
            void addStop(List<BusRouteInfo> bus)
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
                    stations.Add((BusRouteInfo)stop); //add the new stop to the list of stops
                    lastStop = (BusRouteInfo)stop;
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
            bool isStopOnRoute(int busKey)
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
                    var loc_y = busLines[i+1].BusLocation;
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
                for(int i = 0; i<bus.Count; i++)
                {
                    if (bus[i].BusStationKey == stop.BusStationKey)//if found increment time
                    {
                        BusTotalTime += bus[i].BusTime;
                    }
                    else { }
                }
               
            }

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
            public BusLine routeLine (BusStop a, BusStop b)
            {
                foreach (BusLine bus in stations) //we don't have a list of busLines?
                {
                    if (bus.firstStop == a && bus.lastStop == b)
                        return bus;
                }
                throw new ArgumentException("Error: Invalid Input.");
            }

            /*G comparing routes (IComparable)*/
            public BusLine shortestTravelTime (BusLine a, BusLine b)
            {
                return compareLines(a, b);
            }



            //*BUS_ROUTES IN DATABASE*//
            class BusDatabase : BusLine //object made: database of bus routes (list of selected lines)
            {

                /*CLASS MEMBERS*/
                private List<BusLine> routes;

                //IEnumerable implementation
                public class BusRoutes : IEnumerator, IEnumerable
                {
                    public List<BusRoutes> routes = new List<BusRoutes>();
                    int position = -1;
                    //IEnumerator and IEnumerable require these methods.
                    public IEnumerator GetEnumerator()
                    {
                        return (IEnumerator)this;
                    }
                    //IEnumerator
                    public bool MoveNext()
                    {
                        position++;
                        return (position < routes.Count);
                    }
                    //IEnumerable
                    public void Reset()
                    {
                        position = 0;
                    }
                    //IEnumerable
                    public object Current
                    {
                        get { return routes[position]; }
                    }
                }
                /*CLASS CTOR*/
                public BusDatabase() //default cctor
                {

                }
                public BusDatabase(int lineN, Areas area, List<BusRouteInfo> b, BusRouteInfo first, BusRouteInfo last, float dist, TimeSpan t, int key, double lat, double lon, string adr) : base(lineN, area, b, first, last, dist, t, key, lat, lon, adr) { }

                /*A add/remove line*/
                /* Method: addLine
                * Description: recieves a line number and adds the line to the database (keeping in mind that each line appears twice - to and from )
                * Return Type: void
                */
                public void addLine()
                {
                    Console.WriteLine("Please enter the line number you wish to add: ");
                    int lineNum = Convert.ToInt32(Console.ReadLine());
                    foreach (BusLine bus in routes)
                    {
                        if (bus.BusLineNum == lineNum)//if line already exists
                        {
                            Console.WriteLine("Error: bus line already exists in database.");
                        }
                        //bus line is new
                        BusLine busLine = new BusLine();
                        routes.Add(busLine);
                    }
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
            //generic exceptions:
            public class BusExceptions : Exception
            {
                public BusExceptions(string message) : base(message) { }
            }

            public enum BusLineOptions { Insert = 1, Delete, Search, Print, Exit }; //idea can change
            static void Main(string[] args)
            {
                var BusCollection = new List<BusDatabase>();//list of selected lines/routes you chose
                var BusRoutes = new List<BusLine>(); //list of all lines/routes available
                var BusLines = new List<BusRouteInfo>(); //list of stations in possible lines/routes
                

                for (int i = 0; i < 40; i++) //random list of 40 stations
                {
                    //var noDuplicatesKey = new List<int>();
                    //bool repeat = true;
                    var stop = new BusRouteInfo();
                    BusLines.Add(stop);
                    //setting station key (6 digits)
                    Random rd = new Random();
                    int busKey = rd.Next(100000, 1000000);
                    BusLines[i].BusStationKey = busKey;
                    
                    //while (repeat)
                    //{
                    //    Random rd = new Random();
                    //    int busKey = rd.Next(100000, 1000000);
                    //    if (noDuplicatesKey.Contains(busKey))
                    //    {
                    //        rd = new Random();
                    //        busKey = rd.Next(100000, 1000000);
                    //    }
                    //    noDuplicatesKey.Add(busKey);//add to duplicate list 
                    //    BusLines[i].BusStationKey = busKey;
                    //}
                    //setting latitude (31.30 - 33.30)
                    Random rlat = new Random();
                    BusLines[i].BusLocation.Latitude = rlat.NextDouble() * (33.30 - 31.30) + 31.30;
                    //setting longitude (34.30 - 35.50)
                    Random rlong = new Random();
                    BusLines[i].BusLocation.Longitude = rlong.NextDouble() * (35.50 - 34.30) + 34.30;
                    
                    //set speed (10km/h - 200km/h)
                    Random spd = new Random();
                    BusLines[i].BusLocation.Speed = (spd.Next(1, 201));
                    //setting address to default unknown
                }

                for (int i = 0; i < 10; i++) //creating 10 lines
                {
                    var noDuplicates = new List<int>();
                    bool repeat = true;
                    Random randS = new Random();
                    int numStops = randS.Next(1, 7);
                    for (int j = 0; j < numStops; j++) //adding the BusStops to the line
                    {
                        int k = 0;
                        while (repeat)
                        {
                            Random randL = new Random();
                            k = (randL.Next(1, 41));
                            if (noDuplicates.Contains(k))
                            {
                                Random rand = new Random();
                                k = (rand.Next(1, 41));
                            }
                            else
                            {
                                repeat = false; 
                            }
                        }
                        noDuplicates.Add(k);//add to duplicate list 
                        BusRoutes.Add(BusLines[k] as BusLine);
                    }
                }

            
                for (int  i = 0;  i < 10;  i++) //adding all the lines to our collection
                {
                    BusCollection.Add(BusLines[i] as BusDatabase);
                }

                Console.WriteLine(" 1. Add a Bus Line \n 2. Remove a Bus Line \n 3. Search for a Bus Line \n 4. Print Bus Lines \n 5. Exit");

                int choice;
                do
                {

                    int.TryParse(Console.ReadLine(), out choice);
                    int ch = 3;//indicates the users inputted choice
                    switch ((BusLineOptions)choice)
                    {
                        case BusLineOptions.Insert:
                            Console.WriteLine("INSERT:");//COMMENT OUT!!
                            Console.WriteLine("Enter 0 to add a bus stop or 1 to add a bus line:\n");
                            ch = Int32.Parse(Console.ReadLine());
                            if (ch == 0)
                            {
                                BusLine newBus = new BusLine();
                                newBus.addStop(BusLines);
                                BusRoutes.Add(newBus);
                            }
                            else if (ch == 1)
                            {
                                BusDatabase newLine = new BusDatabase();
                                newLine.addLine();
                                BusCollection.Add(newLine);
                            }
                            else 
                            {
                                throw new ArgumentException("Error: Invalid Input.");
                            }
                            break;
                        case BusLineOptions.Delete:
                            Console.WriteLine("DELETE:");//COMMENT OUT!!
                            Console.WriteLine("Enter 0 to remove a bus stop or 1 to remove a bus line:\n");
                            ch = Int32.Parse(Console.ReadLine());
                            if (ch == 0)
                            {
                                Console.WriteLine("Ënter stationKey of the bus stop that you want removed.\n");
                                int key = Int32.Parse(Console.ReadLine());
                                try { BusRoutes[0].removeStop(key); } 
                                catch { throw new ArgumentException("Error: There are no bus stops listed to remove.\n"); }
                            }
                            else if (ch == 1)
                            {
                                Console.WriteLine("Ënter line number of the bus line that you want removed.\n");
                                int num = Int32.Parse(Console.ReadLine());
                                try { BusCollection[0].removeLine(num); } 
                                catch { throw new ArgumentException("Error: There are no bus stops listed to remove.\n"); }
                            }
                            else 
                            {
                                throw new ArgumentException("Error: Invalid Input.");
                            }
                            break;
                        case BusLineOptions.Search:
                            Console.WriteLine("SEARCH:");//COMMENT OUT!!
                            Console.WriteLine("Enter 0 to search for lines that go through a specific station or 1 to show potential travel options given a strat and end station:\n");
                            ch = Int32.Parse(Console.ReadLine());
                            if (ch == 0)
                            {
                                List<BusLine> lines = FindRoutes(BusCollection);
                            }
                            else if (ch == 1)
                            {
                                FindTravelOptions(BusRoutes);
                            }
                            else
                            {
                                throw new ArgumentException("Error: Invalid Input.");
                            }
                            break;
                        case BusLineOptions.Print:
                            Console.WriteLine("Bus Line in the BusCollection:\n");
                            Console.WriteLine("Stations and line numbers in BusCollection:\n");
                            Console.WriteLine("PRINT:");//COMMENT OUT!!
                            break;
                        case BusLineOptions.Exit:
                            Console.WriteLine("EXIT:");//COMMENT OUT!!
                            System.Environment.Exit(0); 
                            break;
                        default:
                            Console.WriteLine("Invalid option.\n");
                            break;
                    }
                    Console.WriteLine("Select another option from the menu:\n");
                    Console.WriteLine("1. Add a Bus Line \n 2. Remove a Bus Line \n 3. Search for a Bus Line \n 4. Print Bus Lines \n 5. Exit");
                } while (choice != 0);
            }

            private static List<BusLine> FindRoutes(List<BusDatabase> BusCollection)
            {
                Console.WriteLine("Enter the station key for the station that you are looking for:\n");
                int station = Int32.Parse(Console.ReadLine());
                var lines = BusCollection[0].linesThroughStation(station); //finding possible routes user can take
                Console.WriteLine("These are the lines which pass through your selected station:\n");
                for (int i = 0; i < lines.Count; i++)//print routes can take
                {
                    Console.WriteLine(lines);
                }

                return lines;
            }

            private static void FindTravelOptions(List<BusLine> lines)
            {
                Console.WriteLine("Enter the station key of the Start Station:\n");
                int startStation = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Enter the station key of the End Station:\n");
                int endStation = Int32.Parse(Console.ReadLine());

                var travelOptions = new List<BusDatabase>();
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].isStopOnRoute(startStation))//is their startStaion on one of the lines
                    {
                        if (lines[i].isStopOnRoute(endStation))//is the endstation on the same line
                        {
                            travelOptions.Add((BusDatabase)lines[i]); //add that line to options
                        }
                    }
                }

                bool isEmpty = !travelOptions.Any();
                if (isEmpty)
                {
                    throw new ArgumentException("Error: No travel options are available.\n");
                }

                travelOptions[0].sortLines(travelOptions);//sort lines
                Console.WriteLine("Your travel options are:\n");
                Console.WriteLine(travelOptions);//print result
            }
        }
    }
}
