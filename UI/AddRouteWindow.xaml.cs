using BLApi;
using BO;
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
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for AddRouteWindow.xaml
    /// </summary>
    public partial class AddRouteWindow : Window
    {
        public IBL bl = BLFactory.GetBL();
        BusRoute newRoute = new BusRoute();
        public int setValue = 0;
        ObservableCollection<BusStations> stationsCurrent;
        public AddRouteWindow()
        {
            InitializeComponent();

            List<BusStations> statsAvail = bl.GetAllBusStops().ToList();
            stationsCurrent = new ObservableCollection<BusStations>(statsAvail);
            lv_stations.DataContext = stationsCurrent;
        }

        private void cb_regionRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (setValue == 0)
            {
                //don't change 
            }
            setValue--;
            Value.Text = setValue.ToString();
        }

        private void Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Value.Text = setValue.ToString();
        }
        private void Up_Click(object sender, RoutedEventArgs e)
        {
            setValue++;
            Value.Text = setValue.ToString();
        }



        private void b_addroutetoSystem_Click(object sender, RoutedEventArgs e)
        {
            string routeID = bl.AddNewRoute(cb_regionRoute.SelectedIndex, Value.Text, stationsList);
            newRoute = bl.GetBusRouteID(routeID);
            AdminManagerWindow.routeCollection.Add(newRoute);
            this.Close();
        }

        public List<BusStations> stationsList = new List<BusStations>();

        private void check_stations_Checked(object sender, RoutedEventArgs e)
        {
            BusStations selectedStation;

            ListViewItem lvi = GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

            if (lvi != null)
            {
                lv_stations.SelectedIndex = lv_stations.ItemContainerGenerator.IndexFromContainer(lvi);
                selectedStation = (BusStations)lv_stations.SelectedItem;
                stationsList.Add(selectedStation);
            }
        }

        public static DependencyObject GetAncestorByType(DependencyObject element, Type type)
        {
            if (element == null) return null;

            if (element.GetType() == type) return element;

            return GetAncestorByType(VisualTreeHelper.GetParent(element), type);
        }
    }




}