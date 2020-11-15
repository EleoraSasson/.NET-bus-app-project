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
        public enum BusLineOptions { Insert = 1, Delete, Search, Print, Exit }; //idea can change
        static void Main(string[] args)
        {
            var BusCollection = new BusDatabase(); // our collection of bus lines (AKA the companoes selected lines of travel)

            var busStops = new List<BusRouteInfo>();

            for (int i = 0; i < 40; i++)
            {
                var stop = new BusRouteInfo();
                busStops.Add(stop);
            }

            for (int i = 0; i < 10; i++)
            {
                BusCollection.addLine(); //adding 10 lines to BusCollection
            }

            
            //print to screen:
            Console.WriteLine("Select an option from the menu below:");
            Console.WriteLine(" 1.Insert \n 2.Delete \n 3.Search \n 4.Print \n 5.Exit");

            int choice;
            do
            {

                int.TryParse(Console.ReadLine(), out choice);
                int ch = 3;//indicates the users inputted choice
                switch ((BusLineOptions)choice)
                {
                    case BusLineOptions.Insert:
                        Console.WriteLine("INSERT:");//COMMENT OUT!!
                        Console.WriteLine("Enter 0 to add a bus stop or 1 to add a bus line:");
                        ch = Int32.Parse(Console.ReadLine());
                        if (ch == 0)
                        {
                            Console.WriteLine("Please Identify which line you would like to add the stop to, what is the line number?");
                            int line  = Int32.Parse(Console.ReadLine());

                            foreach (BusLine bline in BusCollection) 
                            {
                                if (bline.BusLineNum == line)
                                {
                                    BusCollection.addStop(bline.setStations);
                                    Console.WriteLine("Bus stop successfully added.\n");
                                }
                            }
                        }
                        else if (ch == 1)
                        {
                            BusCollection.addLine();
                            Console.WriteLine("Bus line successfully added.\n");
                        }
                        else
                        {
                            throw new ArgumentException("Error: Invalid Input.");
                        }
                        break;
                    case BusLineOptions.Delete:
                        Console.WriteLine("DELETE:");//COMMENT OUT!!
                        Console.WriteLine("Enter 0 to remove a bus stop or 1 to remove a bus line:");
                        ch = Int32.Parse(Console.ReadLine());
                        if (ch == 0)
                        {
                            Console.WriteLine("Enter the station key of the bus stop that you want removed:");
                            int key = Int32.Parse(Console.ReadLine());
                            try { BusCollection.removeStop(key); }
                            catch { throw new ArgumentException("Error: There are no bus stops listed to remove.\n"); }
                        }
                        else if (ch == 1)
                        {
                            Console.WriteLine("Ënter line number of the bus line that you want removed.\n");
                            int num = Int32.Parse(Console.ReadLine());
                            try { BusCollection.removeLine(num); }
                            catch { throw new ArgumentException("Error: There are no bus stops listed to remove.\n"); }
                        }
                        else
                        {
                            throw new ArgumentException("Error: Invalid Input.");
                        }
                        break;
                    case BusLineOptions.Search:
                        Console.WriteLine("SEARCH:");//COMMENT OUT!!
                        Console.WriteLine("Enter 0 to search for lines that go through a specific station or 1 to show potential travel options given a strat and end station:");
                        ch = Int32.Parse(Console.ReadLine());
                        if (ch == 0)
                        {
                            FindRoutes(BusCollection);
                        }
                        else if (ch == 1)
                        {
                            FindTravelOptions(BusCollection);
                        }
                        else
                        {
                            throw new ArgumentException("Error: Invalid Input.");
                        }
                        break;
                    case BusLineOptions.Print:
                        Console.WriteLine("PRINT:");//COMMENT OUT!!
                        Console.WriteLine("Bus Line in the BusCollection:\n");
                        foreach(BusLine line in BusCollection)
                        { 
                            Console.WriteLine(BusCollection); //calls on the ToString
                            foreach(BusRouteInfo stop in line)
                            {
                                var Lines = BusCollection.linesThroughStation(stop.BusStationKey); //find the line that go through the given station
                                Console.WriteLine("Stations and line numbers in BusCollection:\n");
                                Console.WriteLine(Lines); //prints lines that go through given station
                            }
                        }

                        break;
                    case BusLineOptions.Exit:
                        Console.WriteLine("EXIT:");//COMMENT OUT!!
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option.\n");
                        break;
                }
                Console.WriteLine("Select another option from the menu:");
                Console.WriteLine(" 1. Insert \n 2. Delete \n 3. Search \n 4. Print \n 5. Exit");
            } while (choice != 0);
        }

        //*METHODS USED IN THE MAIN*//

        /* Method: FindRoutes
            * Description: FInds all the routes that pass through a given station, returns a list of those routes
            * Return Type: List<BusLine>
            */
        private static void FindRoutes(BusDatabase collection)
        {
            Console.WriteLine("Enter the station key for the station that you are looking for:\n");
            int station = Int32.Parse(Console.ReadLine());
            List<int> lines = collection.linesThroughStation(station); //finding possible routes user can take
            Console.WriteLine("These are the lines which pass through your selected station:\n");
            for (int i = 0; i < lines.Count; i++)//print routes can take
            {
                Console.WriteLine(lines);
            }
        }

        /* Method: FindTravelOptions
            * Description: Given start and end stations, this method finds all possible travel options available and prints them out.
            * Return Type: void
            */
        private static void FindTravelOptions(BusDatabase collection)
        {
            Console.WriteLine("Enter the station key of the Start Station:\n");
            int startStation = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter the station key of the End Station:\n");
            int endStation = Int32.Parse(Console.ReadLine());

            var travelOptions = new List<BusLine>();
            foreach (BusLine route in collection)
            {
                if (route.isStopOnRoute(startStation))//is their startStaion on one of the lines
                {
                    if (route.isStopOnRoute(endStation))//is the endstation on the same line
                    {
                        travelOptions.Add(route); //add that line to options
                    }
                }
            }

            bool isEmpty = !travelOptions.Any();
            if (isEmpty)
            {
                throw new ArgumentException("Error: No travel options are available.\n");
            }

            collection.sortLines(travelOptions);//sort lines
            Console.WriteLine("Your travel options are:\n");
            Console.WriteLine(travelOptions);//print result
        }
    }
}
