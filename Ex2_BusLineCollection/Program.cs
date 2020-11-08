﻿using System;
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

/*IEnumerable (time) (Gila)
 * IComparable (distance) (Eleora)
 * Indexer (Gila)
 * Passenger (Eleora)
 * Main (with exceptions) (Eleora)
 */

/*GENERAL NOTES:
 * have not dealt with the fact that a line has two directions so when adding route which line? should we add a member to chk?
 * have not fixed input for address is it made up or reverse geo-stuff?
 * B returns list of bus lines passing through a given station <-- is this still a needed method, if yes fix (line 402)
*/
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

            //private double latitude; //latitude

            //public double BusLatitude
            //{
            //    get { return latitude; }
            //    set { latitude = value; }
            //}

            //private double longitude; //longitude 
            //public double BusLongitude
            //{
            //    get { return longitude; }
            //    set { longitude = value; }
            //}

            /*CLASS CTORS*/
            
            public BusStop() //defualt ctor 
            {
                BusStationKey = 000000;
                BusLocation.Latitude= 0.0;
                BusLocation.Longitude = 0.0;
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
                int sKey = 0;
                while (enterKey == false)
                {
                    Console.WriteLine("Enter the bus station ID (must be 6 digits): ");
                    string key = Console.ReadLine();
                    string correct = @"^[0-9]\d{6}$";
                    bool keyVerified = false;
                    keyVerified = (Regex.IsMatch(key, correct));
                    while (keyVerified == false) //check if key is valid
                    {
                        Console.WriteLine("Error: Invalid Key - Must be 6 digits long\n");
                        key = Console.ReadLine();
                        keyVerified = (Regex.IsMatch(key, correct));
                    }
                    sKey = Convert.ToInt32(key); //assuming we can convert without the tryParse
                    int index = bus.FindIndex(item => item.BusStationKey == sKey);
                    if (index < 0)
                    {
                        Console.WriteLine("ERROR: bus station already exists.");
                    }
                    else { enterKey = true; }
                }
                stop.BusStationKey = sKey;
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
                return ("Bus Station Code: " + BusStationKey + ", " + BusAddress + " N " + BusLongitude + " E, " + BusAddress); 
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

            public BusRouteInfo():base() //default ctor
            {
                BusDistance = 0;
                var t = new TimeSpan(0,0,0,0);
                BusTime = t;
            }

            public BusRouteInfo(float dist, TimeSpan t , int key, double lat, double lon, string adr) : base(key, lat, lon, adr) // ctor
            {
                BusDistance = dist;
                BusTime = t;
            }
        }

        //*BUS_LINES*//
        public enum Areas { Unknown, North_Golan, North_Haifa, Center_TelAviv, Center_Jerusalem, South_BeerSheva, South_Eilat, National}; //unknown indicates area has not yet been set & National implies bus goes throughout country
        class BusLine : BusRouteInfo //object made: Lines (list of BusStops) w area info
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

            public List<BusRouteInfo> BusStations
            {
                get { return stations; }
                set { stations = value; }
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
                firstStop = BusStations[0];
                lastStop = BusStations[0];
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
            void addStop(List<BusRouteInfo> bus)
            {
                var stop = new BusStop();
                setKey(bus,stop); //sets key of station
                setLocation(stop); //sets location and speed
                setAddress(bus,stop); //sets address of station
                if (bus.First() == null) //if first element is empty
                {
                    BusStations.Add((BusRouteInfo)stop); //add the new stop to the list of stops
                    firstStop = (BusRouteInfo)stop; // the stop you added is also the first stop of route
                    lastStop = (BusRouteInfo)stop; // it is also the last stop on the route
                }
                else // the station you add now will become the last stop of the route
                {
                    BusStations.Add((BusRouteInfo)stop); //add the new stop to the list of stops
                    lastStop = (BusRouteInfo)stop;
                }

            }

            /* Method: removeStop
            * Description: Searches for busKey in a given route and if found removes the bus station with that bus key.
            * Return Type: void 
            */
            void removeStop(int busKey) 
            {
                bool notFound = true;

                for (int i = 0; i < BusStations.Count; i++) //search for stop
                {
                    if (busKey == BusStations[i].BusStationKey)
                    {
                        BusStations.RemoveAt(i); //remove stop
                        notFound = false;
                    }
                }

                if (notFound) // true that it hasn't been found
                {
                    Console.WriteLine("Error: Bus stop does not exist in the route.");
                }
            }

            /*C is station/stop on line*/

            /* Method: isStopOnRoute
            * Description: returns true if the stop is on the route
            * Return Type: bool
            */
            bool isStopOnRoute(int busKey) 
            {
                int index = BusStations.FindIndex(stop => stop.BusStationKey == busKey); 
                if (index >= 0) //if the bus station is on the route
                {
                    return true;
                }
                else return false;
            }

            /*D.	A method that returns the distance between 2 stations on the line ( not necessarily adjacent stops)
            E.	A method that returns the travel time between 2 stations on the line ( not necessarily adjacent stops)
            */

            /*D distance between stops*/

            /* Method: routeDistance
             * Description: returns the distance between two stops given their locations
             * Return Type: float
             */

            public double routeDistance(GeoCoordinate loc_1, GeoCoordinate loc_2)
            {
                var distance = loc_1.GetDistanceTo(loc_2);
                return distance;
            }

            /*E travel time between stops*/

            /* Method: routeTime
             * Description: returns the travel time between two stops given their keys
             * Return Type: TimeSpan
             */

            public TimeSpan routeTime(List<BusRouteInfo> busLines, int keyStart, int kayEnd)
            {
                //given the start of the line we want to send it just the line we are looking at 
                foreach (BusLine bus in busLines)
                {
                    if (bus.firstStop.BusStationKey == keyStart)
                    {
                        TimeSpan time = travelTime();
                    }
                    else { Console.WriteLine("Error: line does not exist"); }
                }
                
                return 0; //time value
            }
          

            /* Method: routeTime
            * Description: returns the travel time between 2 stations on the line
            * Return Type: DateTime
            */
            //TimeSpan routeTime() //added a field to the class 
            //{
            //    Console.WriteLine("Please enter the travel time between the 2 stations in the format: hh:mm:ss");
            //    int hours = Convert.ToInt32(Console.ReadLine());
            //    int minutes = Convert.ToInt32(Console.ReadLine());
            //    int seconds = Convert.ToInt32(Console.ReadLine());
            //    TimeSpan time = new TimeSpan(hours, minutes, seconds);
            //    return time;
            //}
            /*F subroute of route (AKA line)*/
            //still must do

            /*G comparing routes (IComparable)*/
            //still must do
        }

        //*BUS_ROUTES IN DATABASE*//
        class BusDatabase : BusLine //object made: database of bus routes (list of selected lines)
        {

            /*CLASS MEMBERS*/
            private List<BusLine> routes;

            public List<BusLine>  BusRoutes
            {
                get { return routes; }
                set { routes = value; }
            }


            /*CLASS CTOR*/
            BusDatabase() //default cctor
            {

            }
            public BusDatabase(int lineN, Areas area, List<BusRouteInfo> b, BusRouteInfo first, BusRouteInfo last, float dist, TimeSpan t, int key, double lat, double lon, string adr) :base (lineN,area,b, first, last,dist,t,key,lat,lon,adr) { }
           
            /*A add/remove line*/
            /* Method: addLine
            * Description: recieves a line number and adds the line to the database (keeping in mind that each line appears twice - to and from )
            * Return Type: void
            */
            void addLine() 
            {
                Console.WriteLine("Please enter the line number you wish to add: ");
                int lineNum = Convert.ToInt32(Console.ReadLine());
                foreach (BusLine bus in BusRoutes)
                {
                    if (bus.BusLineNum == lineNum)//if line already exists
                    {
                        Console.WriteLine("Error: bus line already exists in database.");
                    }
                    //bus line is new
                    BusLine busLine = new BusLine();
                    BusRoutes.Add(busLine);
                }
            }

           /* Method: removeLine
           * Description: removes a bus line from the collection
           * Return Type: void
           */ 
            void removeLine(int lineToRemove)
            {
                bool notFound = true;

                for (int i = 0; i < BusRoutes.Count; i++) //search for stop
                {
                    if (lineToRemove == BusRoutes[i].BusLineNum)
                    {
                        BusRoutes.RemoveAt(i); //remove line
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
            List<BusLine> linesThroughStation(int StationKey)
            {
                bool keyMatch = false;
                var LinesThruStation = new List<BusLine>();
                foreach (BusStop busStop in BusStations) // for every bus in our list of bus stations
                {
                    if (busStop.BusStationKey != StationKey) // if we don't find a matching key
                    {
                        keyMatch = true;
                    }
                    else//key found - now search Bus Routes to find any bus lines that go through that station
                    {
                        foreach (BusLine busRoutes in BusRoutes) //for every busline in our list of busroutes
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
            public void sortLines(List<BusLine> busRoutes)
            {
                prevLine
                foreach (BusLine bus in busRoutes)
                {
                    BusTime.CompareTo(prevLine);
                }

            }


            //TOGETHER
            //private DateTime BusTotalTime;

            //public DateTime totalTime
            //{
            //    set { BusTotalTime = totalTimeTravel(bus b); } // go over it
            //    get { return BusTotalTime; }
            //}
            //public DateTime totalTimeTravel(BusLine bus)
            //{
            //    int hours = 0;
            //    int minutes = 0;
            //    int seconds = 0;
            //    for (int i = 0; i < BusStations.Count; i++) //loop to get the total travel time of a line
            //    {
            //        hours += BusStations[i].BusTravelTime.Hour;
            //        minutes += BusStations[i].BusTravelTime.Minute;
            //        seconds += BusStations[i].BusTravelTime.Second;
            //    }
            //    DateTime total = new DateTime(hours, minutes, seconds);
            //    return total;
            //}



            //public class TimeComparer : IComparer<BusLine>
            //{
            //    public int Compare(BusLine x, BusLine y)
            //    {
            //        if (object.ReferenceEquals(x, y))
            //            return 0;
            //        if (x == null)
            //            return -1;
            //        if (y == null)
            //            return 1;
            //        return x.totalTime.CompareTo(y.totalTime); //go over it
            //    }

            //};
            //void sortLines() //have to add a total time
            //{

            //    BusDatabase.Sort((x, y) => x.totalTime.CompareTo(y.totalTime))
            //}

            /*D indexer - checks for line number*/
            //do indexer GILA --- INUMERABLE
        }

        public enum BusLineOptions { Insert =1 , Delete, Search, Print, Exit}; //idea can change
        static void Main(string[] args)
        {
            /*
             * In the main program randomly initialize the class of bus lines with at least 10 bus routes. Also randomly create at least 40 different bus stops for the various lines.
            •	There is no need for all lines to go through all the stations.
            •	Make sure at least one bus line passes through each station
            •	At least 10 stations have more than one bus line
            The main program will display a menu to the user with the following options:
 
            A. Insert:
            ·         New Bus Line
            ·         Add a station to a bus line
 
            B. Delete:
            ·         Remove a bus line
            ·         Delete a station from a bus route
 
            C. Search:
            ·         The bus lines that pass through a given station according to station number
            ·         Print the options for travelling between 2 stations, without changing a bus
            The user should enter a starting station and a destination station
            The results should be returned sorted by travel time.
 
            D. Print:
            ·         All the bus lines that are in the system.
            ·         List of all stations and line numbers that pass through them.
 
            E. Exit.

             */

//            •	There is no need for all lines to go through all the stations.
//•	Make sure at least one bus line passes through each station
//•	At least 10 stations have more than one bus line

            var BusDatabase = new List<BusLine>();
            for (int i = 0; i < 40; i++)
            {
                Random rd = new Random();
                int key = rd.Next(100000, 1000000); //creates a 6 digits key
                BusDatabase[i].BusStationKey = key;

                Random rlat = new Random();
                BusDatabase[i].BusLocation.Latitude = rlat.NextDouble() * (33.30 - 31.30) + 31.30;
                //longitude:
                Random rlong = new Random();
                BusDatabase[i].BusLocation.Longitude = rlong.NextDouble() * (35.50 - 34.30) + 34.30; //returns random variable between 34.3 and 35.5  
            }
            var BusCollection = new List<BusDatabase>();
        
            for
            for (int k = 0; k < 10; k++)
            {
                BusCollection.Add(BusDatabase[rand]);
            }
            Console.WriteLine(" 1. Add a Bus Line \n 2. Remove a Bus Line \n 3. Search for a Bus Line \n 4. Print Bus Lines \n 5. Exit");

            int choice;
            do
            {
                int.TryParse(Console.ReadLine(), out choice);
                int ch;
                switch ((BusLineOptions)choice)
                {
                    case BusLineOptions.Insert:
                        Console.WriteLine("Enter 0 to add a new bus line, enter 1 to add a station to a bus line: ");
                        ch = Convert.ToInt32(Console.ReadLine());
                        if (ch == 0)
                        {
                            BusCollection[indexer].addLine(); 

                        }
                        else if (ch == 1)
                        {
                            addStop(List < BusRouteInfo > bus)
                        }
                        else
                        {
                            while (ch != 0 && ch != 1)
                            {
                                 ch = Convert.ToInt32(Console.ReadLine());
                            }
                        }

                        break;
                    case BusLineOptions.Delete:
                        Console.WriteLine("Enter 0 to remove a bus line, enter 1 to delete a station from a bus route: ");
                        ch = Convert.ToInt32(Console.ReadLine());
                        if (ch == 0)
                        {
                            Console.WriteLine("Enter the number of the line to remove: ");
                            int line = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                BusCollection[0].removeLine(line);
                            } 
                            catch { Console.WriteLine("Error: BusCollection is empty."); }

                        }
                        else if (ch == 1)
                        {
                            Console.WriteLine("Enter the ID of the station to delete: ");
                            int key = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                BusCollection[0].removeStop(key);
                            }
                            catch { Console.WriteLine("Error: BusCollection is empty."); }
                           
                        }
                        else
                        {
                            while (ch != 0 && ch != 1)
                            {
                                ch = Convert.ToInt32(Console.ReadLine());
                            }
                        }

                        break;
                    case BusLineOptions.Search:

                        break;
                    case BusLineOptions.Print:

                        break;
                    case BusLineOptions.Exit:
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Select another option from the menu:\n");
            } while (choice!=0);

        }
    }
}
