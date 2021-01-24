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

//Gila and Eléora - Exercise 3B

namespace Ex3b_GUI
{
    public partial class MainWindow : Window
    {
        /*RELEVANT VARIABLES & LISTS*/
       
        public static ObservableCollection<Bus> BusList = new ObservableCollection<Bus>();

        private void randomBus()
        {
            for (int i = 0; i < 10; i++)
            {
                System.Threading.Thread.Sleep(20); //to allows for randomised values to be random
                Bus b = new Bus();
                b.BusStartDate = b.randDate();
                b.BusLastMaintenanceDate = b.BusStartDate;
                b.BusLicense = b.randLicense();
                b.BusDriver = b.randDriver();
                b.BusMileage = b.randMileage();
                System.Threading.Thread.Sleep(10);
                b.BusState = Status.Available;
                b.BusFuel = b.randFuel();
                BusList.Add(b);
            }
        }

        /*Main Window*/
        public MainWindow()
        {
            InitializeComponent();
            //fill bus list with random values:
            randomBus();
            //print list to console:
            lv_BusList.DataContext = BusList;// .OrderBy(state => state.BusState);                                                                                
        }

        //Add Bus Button Click:
        private void B_AddBus_Click(object sender, RoutedEventArgs e)
        { 
            AddBusWindow addWin = new AddBusWindow();
            addWin.Show();
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
                OptionsWindow optionsWin = new OptionsWindow(selectedBus, lv_BusList);
                optionsWin.Show();
            }
        }

        //Double-Click on Bus
       
        public static DependencyObject GetAncestorByType(DependencyObject element, Type type)
        {
            if (element == null) return null;

            if (element.GetType() == type) return element;

            return GetAncestorByType(VisualTreeHelper.GetParent(element), type);
        }

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
        private void lv_BusList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}