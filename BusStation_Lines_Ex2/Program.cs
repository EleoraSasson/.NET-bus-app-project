using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;//this can be used to validate strings
using System.Xml;
using System.Runtime.InteropServices;

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
            public BusStop(int key, double lat, double lon) //had to add a constructor to create new objects later
            {
                BusStationKey = key;
                BusLatitude = lat;
                BusLongitude = lon;
            }

            public virtual void Print() //this should be changed to a ethod that overrides the ToString Method (see comment below)
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
            public BusRoutes(int l, string a, List<BusStop> b, int key, double lat, double lon) : base (key, lat, lon)   //go over it
            {
                BusLine = l;
                BusArea = a;
                BusStop bus = new BusStop(key,lat,lon);
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
                // add check that key does not already exist

                //latitude:
                Random rlat = new Random();
                double rand_latitude = rlat.NextDouble() * (33.30 - 31.30) + 31.30; //returns random variable between 31.30 and 33.30                       

                //longitude:
                Random rlong = new Random();
                double rand_longitude = rlong.NextDouble() * (35.50 - 34.30) + 34.30; //returns random variable between 34.3 and 35.5                     
               
                BusStop stop = new BusStop(key, rand_latitude, rand_longitude);
                BusStations.Add(stop); //add the new stop to the list of stops
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
            DateTime routeTime() //
            {
                var time = Console.ReadLine();
                return time;
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

        class BusLines
        {
            List<BusRoutes> BusDatabase = new List<BusRoutes>();//collection of bus routes

            /* Method: 
            * Description: 
            * Return Type:
            */
            void addLine() //gila
            {

            }

            /* Method: 
            * Description: 
            * Return Type:
            */
            void removeLine(int busLine) //eleora
            {

            }

            /* Method: 
            * Description: 
            * Return Type:
            */
            List<BusRoutes> linesThroughStation( /*send station number*/) //gila
            {
                return ;///return list of lines that pass through the station
            }

            /* Method: 
           * Description: 
           * Return Type:
           */
            void sortLines() //eleora
            {

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

            /* Method: 
           * Description: 
           * Return Type:
           */

            void printLines(List<BusRoutes> BusDatabase) //eleora 
            {
                //traverse list with a foreach loop!
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