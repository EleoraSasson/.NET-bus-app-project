using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex2_BusLineCollection
{
    class Program
    {
        class BusStop
        {
            //class members
            /*
             * stationKey [int]
             * location: [maybe struct]
             *  latitude [double]
             *  longitude [double]
             *  address [string]
             */ 
            //class methods
             //override ToString for prnting info accurately
        }

        class DistanceTime : BusStop
         /*contains the BusStop info and additional info of Distnace and Time */
        {
            //class members
            /*
             * inherits from BusStop
             */
            //class methods
            /*
             * distance - return distance between two points
             * time between stations
             */
        }

        public enum Areas { North = 1, South, Centre, General}; //idea can change
        class BusRoutes
        {
            //class members
            /*
             * routeNumber [int] 
             * area [calls an enum type Area]
             * firstStop [BusStop] //can be updated as you add &remove stops
             * lastStop [BusStop] //can be updated as you add &remove stops
             * stations [List<BusStop>]
             */
            //class methods
            /*
             * A) override ToString
             * B1) add stop
             * B2) remove stop
             * C) isStation on Route?
             * D) returns distance (passes the two station to the class DistanceTime)
             * E) returns travel time (passes the two station to the class DistanceTime)
             * F) subroute of route
             * G) passenger compare two route (see hw sheet) IComparable
             */ //remember to throw exceptions where no data
        }

        class BusLines
        {
            //class members
            /*
             * lineDataBase [List<BusRoutes>]
             */
            //class methods
            /*
             * A1) add bus line to collection
             * A2) remove bus line from collection
             * B) returns list of bus lines passing through a given station
             * C) sorts lines according to time
             * D) indexer - checks for line number (NOTE can use in other methods!)
             */ //note IEnumerable
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

            var BusCollection = new List<BusRoutes>();

            Console.WriteLine(" 1. Add a Bus Line \n 2. Remove a Bus Line \n 3. Search for a Bus Line \n 4. Print Bus Lines \n 5. Exit");

            int choice;
            do
            {
                int.TryParse(Console.ReadLine(), out choice);
                switch ((BusLineOptions)choice)
                {
                    case BusLineOptions.Insert:
                        break;
                    case BusLineOptions.Delete:
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
