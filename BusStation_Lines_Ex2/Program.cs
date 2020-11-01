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
                set 
                {
                    Random rlat = new Random();
                    double rand_latitude =  rlat.NextDouble() * (33.30 - 31.30) + 31.30; //returns random variable between 31.30 and 33.30                       
                    latitude = rand_latitude;
                }
            }
             
            private double longitude; //longitude 
            public double BusLongitude
            {
                get { return longitude; }
                set
                {
                    Random rlong = new Random();
                    double rand_longitude = rlong.NextDouble() * (35.50 - 34.30) + 34.30; //returns random variable between 34.3 and 35.5                     
                    longitude = rand_longitude;
                }

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


            /* Method: 
             * Description: 
             * Return Type:
             */
            void addStop() //Eleora
            {

            }

            /* Method: removeStop
            * Description: Searches for busKey in agiven route and if found removes the bus station with taht bus key.
            * Return Type: void 
            */
            void removeStop(int busKey) //Gila 
            {
                bool notFound = false;

                for (int i = 0; i < BusStations.Count; i++) //search for stop
                {
                    if (busKey == BusStations[i].BusStationKey)
                    {
                        BusStations.RemoveAt(i); //remove stop
                    }
                }

                if (notFound) // true that it hasn't been found
                {
                    Console.WriteLine("Error: Bus stop does not exists in route.");
                }
            }

            /* Method: 
            * Description: 
            * Return Type:
            */
            bool isStopOnRoute(int busLine) //Eleora
            {
                return true;
            }
            /* Method: 
            * Description: 
            * Return Type:
            */

            float routeDistance() //Gila
            {
                return 1; //does this have to be random or accurate???
            }
            /* Method: 
            * Description: 
            * Return Type:
            */
            DateTime routeTime() //Eleora
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
