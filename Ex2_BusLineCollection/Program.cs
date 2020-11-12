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
            var BusCollection = new List<BusDatabase>();//list of selected lines/routes you chose
            var BusRoutes = new List<BusLine>(); //list of all lines/routes available
            var BusLines = new List<BusRouteInfo>(); //list of stations in possible lines/routes
            Random rd = new Random();
            Random rlat = new Random();
            Random rlong = new Random();
            Random spd = new Random();

            //random list of 40 stations:
            for (int i = 0; i < 40; i++)
            {
                var stop = new BusRouteInfo();
                BusLines.Add(stop);
                //setting station key (6 digits)
                int busKey = rd.Next(100000, 1000000);
                BusLines[i].BusStationKey = busKey;
                //setting latitude (31.30 - 33.30)
                BusLines[i].BusLocation.Latitude = rlat.NextDouble() * (33.30 - 31.30) + 31.30;
                //setting longitude (34.30 - 35.50)
                BusLines[i].BusLocation.Longitude = rlong.NextDouble() * (35.50 - 34.30) + 34.30;
                //set speed (10km/h - 200km/h)
                BusLines[i].BusLocation.Speed = (spd.Next(1, 201));
                //setting address to default unknown
            }

            //creating 10 lines:
            for (int i = 0; i < 10; i++)
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

            //adding all the lines to our collection:
            for (int i = 0; i < 10; i++)
            {
                BusCollection.Add(BusLines[i] as BusDatabase);
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
                            BusLine newBus = new BusLine();
                            newBus.addStop(BusLines);
                            BusRoutes.Add(newBus);
                            Console.WriteLine("Bus stop successfully added.\n");
                        }
                        else if (ch == 1)
                        {
                            BusDatabase newLine = new BusDatabase();
                            newLine.addLine();
                            BusCollection.Add(newLine);
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
                        Console.WriteLine("Enter 0 to search for lines that go through a specific station or 1 to show potential travel options given a strat and end station:");
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
                Console.WriteLine("Select another option from the menu:");
                Console.WriteLine(" 1. Insert \n 2. Delete \n 3. Search \n 4. Print \n 5. Exit");
            } while (choice != 0);
        }

        //*METHODS USED IN THE MAIN*//

        /* Method: FindRoutes
            * Description: FInds all the routes that pass through a given station, returns a list of those routes
            * Return Type: List<BusLine>
            */
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

        /* Method: FindTravelOptions
            * Description: Given start and end stations, this method finds all possible travel options available and prints them out.
            * Return Type: void
            */
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

