using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ex3a_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // public CollectionContainer BusCollection = new CollectionContainer();
        
        public MainWindow()
        {
            //creating our busCollection
            BusDatabase lineCollection = new BusDatabase();
            for (int i = 0; i < 10; i++) //randomly creates 10 lines
            {
                lineCollection.randAddLine();
            }

            foreach (BusLine bl in lineCollection)
            {
                for (int i = 0; i < 10; i++) //random number of stations as well??
                {
                    bl.randAddStop();
                }
            }

            //component initialisation:
            InitializeComponent();
            //adding the selectionChanged event for the cbBusLines:
            this.cbBusLines.SelectionChanged += cbBusLines_SelectionChanged; 

        }
        //combo box of BusLines - Selection Changed event 
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbBusLines.SelectedIndex = 0;
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusLineNum);//displays the selection of BusLineNum's
        }

        private void ShowBusLine(int busLineNum) //method which displays the information connected to the selected BusLineNum
        {
            //search for line you need
            
        }

        private void lbBusLineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       
    //Below is the main program class from our previous project use as needed.
    class Program
    {
        public enum BusLineOptions { Insert = 1, Delete, Search, Print, Exit }; //idea can change

        static void OldCode(string[] args)
        {
            var BusCollection = new BusDatabase();

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
                        Console.WriteLine("Enter 0 to add a bus stop or 1 to add a bus line:");
                        ch = Int32.Parse(Console.ReadLine());
                        if (ch == 0)
                        {
                            Console.WriteLine("Enter the line number in which you want to add a stop:");
                            int line = Int32.Parse(Console.ReadLine());
                            if (isLine(line, BusCollection))
                            {

                                int index = BusCollection.BusIndexer(line);
                                if (index >= 0)
                                {
                                    BusCollection.routes[index].addStop();

                                    Console.WriteLine("Bus stop successfully added.\n");
                                }


                            }
                            else Console.WriteLine("ERROR: Line does not exit."); break;
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
                        Console.WriteLine("Enter 0 to remove a bus stop or 1 to remove a bus line:");
                        ch = Int32.Parse(Console.ReadLine());
                        if (ch == 0)
                        {
                            Console.WriteLine("Enter the station key of the bus stop that you want removed:");
                            int key = Int32.Parse(Console.ReadLine());
                            foreach (BusLine b in BusCollection)
                            {
                                b.removeStop(key);
                                Console.WriteLine("Bus stop successfully deleted. ");
                            }
                        }
                        else if (ch == 1)
                        {
                            Console.WriteLine("Ënter line number of the bus line that you want removed.\n");
                            int num = Int32.Parse(Console.ReadLine());
                            try
                            {
                                BusCollection.removeLine(num);
                                Console.WriteLine("Bus line successfully deleted. ");
                            }
                            catch { throw new ArgumentException("Error: There are no bus stops listed to remove.\n"); }
                        }
                        else
                        {
                            throw new ArgumentException("Error: Invalid Input.");
                        }
                        break;
                    case BusLineOptions.Search:
                        Console.WriteLine("Enter 0 to search for lines that go through a specific station or 1 to show potential travel options given a start and end station:");
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
                        Console.WriteLine("Bus Lines in the BusCollection:");

                        foreach (BusLine bl in BusCollection)
                        {
                            Console.WriteLine((bl.BusLineNum) + " ");
                        }

                        Console.WriteLine("Stations and line numbers in BusCollection:");
                        for (int i = 0; i < BusCollection.routes.Count; i++)
                        {
                            Console.WriteLine(BusCollection.routes[i]); //calls on the ToString

                        }

                        break;
                    case BusLineOptions.Exit:
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
            * Description: Finds all the routes that pass through a given station, returns a list of those routes
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
                Console.WriteLine(lines[i]);
            }
        }

        /* Method: isLine
           * Description: returns true if the line exists in the bus database
           * Return Type: bool
           */

        private static bool isLine(int line, BusDatabase collection)
        {
            bool exists = false;
            foreach (BusLine b in collection)
            {
                if (b.BusLineNum == line)
                    exists = true;
            }
            return exists;
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
                if (route.isStopOnRoute(startStation) && route.isStopOnRoute(endStation))//is their startStaion on one of the lines
                {
                    travelOptions.Add(route); //add that line to options
                }
            }
            if (!(travelOptions.Any()))
            {
                Console.WriteLine("No travel option available.");
                return;
            }
            else
            {
                collection.sortLines(travelOptions);//sort lines
                Console.WriteLine("Your travel options are:\n");
                for (int i = 0; i < travelOptions.Count; i++)
                {
                    Console.WriteLine(travelOptions[i]);//print result
                }
            }
        }
    }
}
