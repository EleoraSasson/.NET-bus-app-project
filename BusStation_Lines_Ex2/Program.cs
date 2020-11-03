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

namespace BusStation_Lines_Ex2
{
    class Program
    {
        class BusStop //Base Class //Gila
        {
            private int stationKey; // bus key

            public int BusStationKey
            {
                get { return stationKey; }
                set { stationKey = value; } //NOTE: in order to avoid a case where a station is added that already exists 
                //this should be checked in the main as we cannot check here as busStop does not know info about other instances of BusStops
            }

            private double latitude; //latitude

            public double BusLatitude
            {
                get { return latitude; }
                set { latitude = value;}
            }
             
            private double longitude; //longitude 
            public double BusLongitude
            {
                get { return longitude; }
                set { longitude = value; }
            }

            private DateTime travelTime; //travel time between this stop and the next time

            public DateTime BusTravelTime
            {
                get { return travelTime; }
                set { travelTime = value; }
            }

            private string address; //added this as the updates hw sheet has an address input too -- do we want this to be legit from he lon and lat?

            public string StopAddress
            {
                get { return address; }
                set { address = value; }
            }
            public BusStop(int key, double lat, double lon, string adr) //had to add a constructor to create new objects later
            {
                BusStationKey = key;
                BusLatitude = lat;
                BusLongitude = lon;
                StopAddress = adr;
            }

            public virtual void Print() //this should be changed to a method that overrides the ToString Method (see comment below)
            {
                Console.WriteLine("Bus Station Code: ", BusStationKey,BusLatitude, "N " , BusLongitude, "E\n" ); //Bus Station Code: 123456, 31.766101N 35.192826 E
            }
            
            //public override string ToString()
            //{
            //    return base.ToString() + ": " + value.ToString();
            //}
        }
        class BusRoutes : BusStop //derived Class
        {
            public BusRoutes(int l, string a, List<BusStop> b, BusStop first, BusStop last, int key, double lat, double lon, string adr) : base (key, lat, lon, adr)   
            {
                BusLine = l;
                BusArea = a;
                firstStop = first;
                lastStop = last;
                BusStop bus = new BusStop(key,lat,lon, adr);
                BusStations.Add(bus);
            }
            private int line; //lines

            public int BusLine
            {
                get { return line; }
                set { line = value; }
            }

            private string area; //area

            public string BusArea
            {
                get { return area; }
                set { area = value; }
            }

            private BusStop first; //first station on bus route

            public BusStop firstStop 
            {
                get { return first; }
                set { first = value; }
            }

            private BusStop last; //last station on bus route

            public BusStop lastStop
            {
                get { return last; }
                set { last = value; }
            }

            private List<BusStop> stations; //stations

            public List<BusStop> BusStations
            {
                get { return stations; }
                set { stations = value; }
            }

            /*METHODS*/

            //overriding ToString?
            
            /* Method: addStop
             * Description: adds a bus stattion to a route
             * Return Type: void
             */
            void addStop(List <BusStop> bus) 
            {
                Console.WriteLine("Enter the bus station ID: ");
                //key:
                int key = Convert.ToInt32(Console.ReadLine()); //assuming we can convert without the tryParse
                int index = bus.FindIndex(item => item.BusStationKey == key);
                if (index < 0)
                {
                    Console.WriteLine("ERROR: bus station already exists.");
                    return;
                }

                //latitude:
                Random rlat = new Random();
                double rand_latitude = rlat.NextDouble() * (33.30 - 31.30) + 31.30; //returns random variable between 31.30 and 33.30                       

                //longitude:
                Random rlong = new Random();
                double rand_longitude = rlong.NextDouble() * (35.50 - 34.30) + 34.30; //returns random variable between 34.3 and 35.5                     

                //address:
                string address = "address"; //how do we want to deal with the address?
                BusStop stop = new BusStop(key, rand_latitude, rand_longitude, address);
                if (bus.First() == null) //if first element is empty
                {
                    BusStations.Add(stop); //add the new stop to the list of stops
                    firstStop = stop; // the stop you added is also the first stop of route
                    lastStop = stop; // it is also the last stop on the route
                }
                else // the station you add now will become the last stop of the route
                {
                    BusStations.Add(stop); //add the new stop to the list of stops
                    lastStop = stop;
                }
               
            }

            /* Method: removeStop
            * Description: Searches for busKey in a given route and if found removes the bus station with that bus key.
            * Return Type: void 
            */
            void removeStop(int busKey) //Gila 
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

            /* Method: isStopOnRoute
            * Description: returns true if the stop is on the route
            * Return Type: bool
            */
            bool isStopOnRoute(int busKey) //Eleora
            {
                int index = BusStations.FindIndex(stop => stop.BusStationKey == busKey);
                if (index >= 0) //if the bus station is on the route
                {
                    return true;
                }
                else return false;
            }

            /* Method: 
            * Description: 
            * Return Type:
            */

            float routeDistance() //Gila
            {
                return 1; //does this have to be random or accurate???
            }
            /* Method: routeTime
            * Description: returns the travel time between 2 stations on the line
            * Return Type: DateTime
            */
            DateTime routeTime() //added a field to the class
            {
                Console.WriteLine("Please enter the travel time between the 2 stations in the format: hh:mm:ss");
                var userTime = Console.ReadLine();
                string patternTime = @"^(3[01]|[12][0-9]|0[1-9])[/](1[0-2]|0[1-9])[/]\d{2}$";
                bool dateVerified = false;
                dateVerified = (Regex.IsMatch(userTime, patternTime));
                while (dateVerified == false) //check if date is valid
                {
                    Console.WriteLine("Error: Invalid Time - Must be in format hh:mm:ss.\n");
                    userTime = Console.ReadLine();
                    dateVerified = (Regex.IsMatch(userTime, patternTime));
                }
                DateTime travelTime = DateTime.ParseExact(userTime, "HH:mm:ss tt", null); //converts the string to a DateTime
                BusTravelTime = travelTime; //set the stop's field
                return travelTime;
            }
            /* Method: 
            * Description: 
            * Return Type:
            */
            int chooseRoute() //must implement IComparible //Together
            {
                return 1;
            }

        }

         class BusLines : BusRoutes //add a constructor?? a collection?
        {
            public BusLines (List <BusRoutes> b,   )//(int l, string a, List<BusStop> b, BusStop first, BusStop last, int key, double lat, double lon, string adr) : base(key, lat, lon, adr)
            {
                BusLine = l;
                BusArea = a;
                firstStop = first;
                lastStop = last;
                BusStop bus = new BusStop(key, lat, lon, adr);
                BusStations.Add(bus);
            }

            List<BusRoutes> BusDatabase = new List<BusRoutes>();//collection of bus routes

            /* Method: addLine
            * Description: recieves a line number and adds the line to the database (keeping in mind that each line appears twice - to and from )
            * Return Type: void
            */
            void addLine() //gila ... how do we want to deal with there and back being lines? maybe each line number should be 0___ and 1___ if back?
            {
                Console.WriteLine("Please enter the line number you wish to add: ");
                int lineNum = Convert.ToInt32(Console.ReadLine());
                foreach (BusRoutes busR in BusDatabase)
                {
                    if (busR.BusLine == lineNum) //if line already exists
                    {
                        Console.WriteLine("Error: bus line already exists in database.");
                    }
                    // bus line is new
                    Console.WriteLine("Enter Area   ");
                    BusRoutes busLine = new BusRoutes(lineNum,);
                    BusDatabase.Add(busLine);

                }

            }
           /* public BusRoutes(int l, string a, List<BusStop> b, BusStop first, BusStop last, int key, double lat, double lon, string adr) : base(key, lat, lon, adr)   //go over it
            {
                BusLine = l;
                BusArea = a;
                firstStop = first;
                lastStop = last;
                BusStop bus = new BusStop(key, lat, lon, adr);
                BusStations.Add(bus);
            }*/

            /* Method: removeLine
            * Description: removes a bus line from the collection
            * Return Type: void
            */
            void removeLine(int lineToRemove) //changed the name of the variable to make it less confusing
            {
                bool notFound = true;

                for (int i = 0; i < BusDatabase.Count; i++) //search for stop
                {
                    if (lineToRemove == BusDatabase[i].BusLine)
                    {
                        BusDatabase.RemoveAt(i); //remove line
                        notFound = false;
                    }
                }

                if (notFound) // it hasn't been found
                {
                    Console.WriteLine("Error: Bus stop does not exist in the route.");
                }
            }

            /* Method: 
            * Description: 
            * Return Type:
            */
            List<BusRoutes> linesThroughStation( /*send station number*/) //gila
            {
                return ;///return list of lines that pass through the station
            }

            /* Method: sortLines
           * Description: sorts the bus lines according to the total time of the route, from shortest time to longest time
           * Return Type: void
           */

            private DateTime BusTotalTime;

            public DateTime totalTime
            {
                set { BusTotalTime = totalTimeTravel(bus b); } // go over it
                get { return BusTotalTime; }
            }
            public DateTime totalTimeTravel (BusRoutes bus)
                {
                    int hours = 0;
                    int minutes = 0;
                    int seconds = 0;
                    for (int i = 0; i < BusStations.Count; i++) //loop to get the total travel time of a line
                    {
                        hours += BusStations[i].BusTravelTime.Hour;
                        minutes += BusStations[i].BusTravelTime.Minute;
                        seconds += BusStations[i].BusTravelTime.Second;
                    }
                    DateTime total = new DateTime(hours, minutes, seconds);
                    return total;
                }
                
           

            public class TimeComparer:IComparer <BusRoutes>
            {
                public int Compare (BusRoutes x, BusRoutes y)
                {
                    if (object.ReferenceEquals(x, y))
                        return 0;
                    if (x == null)
                        return -1;
                    if (y == null)
                        return 1;
                    return x.totalTime.CompareTo(y.totalTime); //go over it
                }

            };
            void sortLines() //have to add a total time
            {

                BusDatabase.Sort((x, y) => x.totalTime.CompareTo(y.totalTime))
            }

            /* Method: 
           * Description: 
           * Return Type:
           */
            int doesExist() //gila
            {
                //?????
                return 0;
            }

            public override string ToString()
            {
                return "Line number: " + BusLine; //what do we need to add?
            }
            /* Method: printLines
           * Description: prints objects of the class using a foreach loop
           * Return Type: void
           */

            void printLines(List<BusRoutes> BusDatabase)  
            {
                foreach (BusRoutes element in BusDatabase) //traverse list with a foreach loop!
                {
                    Console.WriteLine(element.ToString());
                }
        }
        
        public enum BusLineOptions { Add = 1, Remove, Search, Print, Exit };
        static void Main(string[] args)
        {
            //Randomly create for different bus stops for the lines ?????
            /*the class of bus line collection with at least 10 bus routes.Also randomly create at least 40 different bus stops for the various lines.
                •	There is no need for all lines to go through all the stations.
                •	Make sure at least one bus line passes through each station
                •	At least 10 stations have more than one bus line*/

            //switch statement (use similar structure to EX1)
            /*A. Insert:
    ·         New Bus Line
    ·         Add a station to a bus line

            B. Delete:
            ·         Remove a bus line
            ·         Delete a station from a bus route

            C. Search:
            ·         The lines that pass through the station according to station number
            ·         Print the options for travelling between 2 stations, without changing a bus
            The user should enter a starting station and a destination station
            The results should be returned sorted by travel time.

            D. Print:
            ·         All the bus lines that are in the system.
            ·         List of all stations and line numbers that pass through them.

            E. Exit.
         * 
         */
          
           
            var BusCollection = new List<BusRoutes>();

            Console.WriteLine(" 1. Add a Bus Line \n 2. Remove a Bus Line \n 3. Search for a Bus Line \n 4. Print Bus Lines \n 5. Exit");

            int choice;
            do
            {
                int.TryParse(Console.ReadLine(), out choice);
                switch ((BusLineOptions) choice)
                {
                    case BusLineOptions.Add:
                        break;
                    case BusLineOptions.Remove:
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
            }
            while (choice != 0);
        }
    }
}


////random latitude
//Random rd = new Random();
//var num = rd.Next(31000000, 33300001);
//float lat = (float)num / 1000000;
////random longitude
//Random r = new Random();
//var n = r.Next(34300000, 35500001);
//float lon = (float)n / 1000000;