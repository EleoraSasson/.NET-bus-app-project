using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;//this can be used to validate strings

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
                set { stationKey = value; }
            }

            private float latitude; //latitude

            public float BusLatitude
            {
                get { return latitude; }
                set { latitude = value; }
            }

            private float longitude; //longitude 

            public float BusLongitude
            {
                get { return longitude; }
                set { longitude = value; }
            }

            public BusStop (int key, float lat, float lon) //had to add a constructor to create new objects later
            {
                BusStationKey = key;
                BusLatitude = lat;
                BusLongitude = lon;
            }
            // ToString method ??? to override for inheritance
        }
        class BusRoutes : BusStop //derived Class
        {
            public BusRoutes (int l, string a, List <BusStop> b, int key, float lat, float lon) : base (key, lat, lon)   //go over it
            {
                BusLine = l;
                BusArea = a;
                //BusStop b
                BusStationKey = key;
                BusLatitude = lat;
                BusLongitude = lon;
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
                int key = Convert.ToInt32(Console.ReadLine()); //assuming we can convert without the tryParse
                //random latitude
                Random rd = new Random();
                var num = rd.Next(31000000, 33300001);
                float lat = (float)num / 1000000;
                //random longitude
                Random r = new Random();
                var n = r.Next(34300000, 35500001);
                float lon = (float)n / 1000000;
                BusStop stop = new BusStop(BusStationKey = key, BusLatitude = lat, BusLongitude = lon);
                BusStations.Add(stop); //add the new stop to the list of stops

            }

            /* Method: 
            * Description: 
            * Return Type:
            */
            void removeStop(int busLine) //Gila
            {

            }

            /* Method: isStopOnRoute
            * Description: returns true if the stop is on the route
            * Return Type: bool
            */
            bool isStopOnRoute(int busLine) //Eleora
            {
                int index = BusStations.FindIndex(stop => stop.BusStationKey == busLine);
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
                return 1;
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
            void addLine()
            {

            }

            /* Method: 
            * Description: 
            * Return Type:
            */
            void removeLine(int busLine)
            {

            }

            /* Method: 
            * Description: 
            * Return Type:
            */
            List<BusRoutes> linesThroughStation( /*send station number*/)
            {
                return ;///return list of lines that pass through the station
            }

            /* Method: 
           * Description: 
           * Return Type:
           */
            void sortLines()
            {

            }

            /* Method: 
           * Description: 
           * Return Type:
           */
            int doesExist()
            {
                //?????
                return 0;
            }

            /* Method: 
           * Description: 
           * Return Type:
           */

            void printLines(List<BusRoutes> BusDatabase)
            {
                //traverse list with a foreach loop!
            }
        }
        static void Main(string[] args)
        {
            //Randomly create for different bus stops for the lines ?????

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
        }
    }
}