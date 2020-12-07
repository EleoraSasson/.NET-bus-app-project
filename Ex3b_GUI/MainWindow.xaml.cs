using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        //public List <Bus> BusList = new List<Bus>();
        public ObservableCollection<Bus> BusList = new ObservableCollection<Bus>();

        private void randomBus()
        {
            for (int i = 0; i < 10; i++)
            {
                System.Threading.Thread.Sleep(20); //to allows for randomised values to be random
                Bus b = new Bus();
                b.randDate();
                //b.BusStartDate = b.randDate();
                b.BusLicense = b.randLicense();
                b.BusDriver = b.randDriver();
                b.BusMileage = b.randMileage();
                b.BusState = b.randStatus();
                b.BusFuel = b.randFuel();
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
            //fill bus list with random values:
            randomBus();
            //print list to console:
            lv_BusList.ItemsSource = BusList.OrderBy(state => state.BusState);

        }

        //Add Bus Button Click:
        private void B_AddBus_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow addWin = new AddBusWindow(BusList);
            addWin.Show();
            this.Close();
        }

        //Select more Options Button:
        private void B_Options_Click(object sender, RoutedEventArgs e)
        {
            Bus selectedBus;
            ListViewItem lvi = GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

            if (lvi != null)
            {
                lv_BusList.SelectedIndex = lv_BusList.ItemContainerGenerator.IndexFromContainer(lvi);
                selectedBus = (Bus)lv_BusList.SelectedItem;
                OptionsWindow optionsWin = new OptionsWindow(selectedBus);
                optionsWin.Show();
            }
        }

        // Double-Click on Bus
        private void lv_BusList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Bus selectedBus;

            ListViewItem lvitem = GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

            if (lvitem != null)
            {
                lv_BusList.SelectedIndex = lv_BusList.ItemContainerGenerator.IndexFromContainer(lvitem);
                selectedBus = (Bus)lv_BusList.SelectedItem;
                BusInfoWin infoWindow = new BusInfoWin(selectedBus);
                infoWindow.Show();
            }
        }
        public static DependencyObject GetAncestorByType(DependencyObject element, Type type)
        {
            if (element == null) return null;

            if (element.GetType() == type) return element;

            return GetAncestorByType(VisualTreeHelper.GetParent(element), type);
        }

        //hover over bus
        private void lv_BusList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Bus selectedBus;

            ListViewItem lvitem = GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

            if (lvitem != null)
            {
                lv_BusList.SelectedIndex = lv_BusList.ItemContainerGenerator.IndexFromContainer(lvitem);
                selectedBus = (Bus)lv_BusList.SelectedItem;
                //not finished
            }
        }

        //Bus selectedBus;
        //ListViewItem lvi = GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

        //if (lvi != null)
        //{
        //    lv_BusList.SelectedIndex = lv_BusList.ItemContainerGenerator.IndexFromContainer(lvi);
        //    selectedBus = (Bus)lv_BusList.SelectedItem;
        //    if (selectedBus.BusState == Status.Available)
        //    {
        //        IsEnabled = true;
        //    }
        //    else IsEnabled = false;
        //}
    }
}