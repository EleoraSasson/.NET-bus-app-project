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


/*PLAN:
 * Take the bus system classes from exercise 1 
 * - randomise the busses there ensuring that at least 1 bus is after service date;
 * one is close to refueal & one close to reservice.
 * 
 * - add to bus class the following:
 *  * status field (use ENUMS)
 *  * (?) available/unavailable
 *  * add field for driver's name
 *  
 *  **DISPLAY:
 *  - Main window (MW) shows list of buses (with a distinction between available and unavailable)
 *  - MW has an "Add Bus" button [will call addBus method] this opens new window which adds bus
 *  - MW Option to select a bus for travel (radio button which is in an select bus for travel column)
 *     > if select bus for travel pop up window to enter travel distance etc.. [isbus fit for travel method]
 *     > enter sends bus on that trip  {message box to inform user that bus has been sent on trip}
 *     > if bus is not fit for travel message box error (should include erro symbol)
 *    MW if double click on bus gives all bus info {hover explanation}
 *    
 *  - NOTE: if a bus is sent ona trip, for refueling or service it is out of commision for a certain time(see word doc)
 *   [this uses threads]
 *    */
namespace Ex3b_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        /*RELEVANT VARIABLES & LISTS*/

        //private Bus currentDisplayBus; //current Bus we are looking at 

        public List <Bus> BusList = new List<Bus>(); //whyyy
        //put bus into collection
        private void randomBus()
        {
            for (int i = 0; i < 10; i++)
            {
                Bus b = new Bus();
                b.BusStartDate = b.randDate();
                b.BusDriver = b.randDriver();
                b.BusLicense = b.randLicense(b.BusStartDate);
                b.BusMileage = b.randMileage();
                b.BusFuel = b.randFuel();
                b.BusState = b.randStatus();
                BusList.Add(b);
            }

        }
        

        public DateTime startDate;
        public string licenseNum;
        public int mile;
        public int fuelLeft;
        public string driverName;

        /*Main Window*/
        public MainWindow()
        {
            InitializeComponent();
            this.DG_Buses.SelectionChanged += DG_Buses_SelectionChanged;
        }

        private void DG_Buses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Bus bus in BusList)
            {
                ShowBus(bus);
            }
        }

        private void ShowBus(Bus bus)
        {
            DataContext = bus;
        }

        private void B_AddBus_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow addWin = new AddBusWindow();
            addWin.Show();
        }


    }
}
