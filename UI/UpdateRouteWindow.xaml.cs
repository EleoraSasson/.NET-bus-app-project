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
using BLApi;
using BO;

namespace UI
{
    /// <summary>
    /// Interaction logic for UpdateRouteWindow.xaml
    /// </summary>
    public partial class UpdateRouteWindow : Window
    {
        public IBL bl = BLFactory.GetBL();
        BO.BusRoute newRoute = new BusRoute();
        BO.BusRoute oldRoute = new BusRoute();
        public int setValue = 0;

        ObservableCollection<Stations> currentStations;

        public UpdateRouteWindow()
        {
            InitializeComponent();

            List<BusRoute> routeList = bl.GetAllBusRoutes().ToList();
            AdminManagerWindow.routeCollection = new ObservableCollection<BusRoute>(routeList);
            cb_Routes.DataContext = AdminManagerWindow.routeCollection;
            cb_Routes.DisplayMemberPath = "Route.BusLineID";
            cb_Routes.SelectionChanged += Cb_Routes_SelectionChanged;
        }

        private void Cb_Routes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            oldRoute = (cb_Routes.SelectedItem as BO.BusRoute);
            Grid_UpdateRoute.DataContext = oldRoute;
            List<Stations> stationList = bl.GetAllStationsInBusRoute(oldRoute).ToList();
            currentStations = new ObservableCollection<Stations>(stationList);
            lv_stations.DataContext = stationList;

            if (oldRoute != null)
            {
                Grid_UpdateRoute.DataContext = bl.GetBusRoute(oldRoute);
            }
        }

        private void b_updateSystem_Click(object sender, RoutedEventArgs e)
        {
            
            AdminManagerWindow.routeCollection.Remove(oldRoute);
            bl.UpdateBusRoute(newRoute);
            AdminManagerWindow.routeCollection.Add(newRoute);
            this.Close();
        }

        private void cb_NewRegionRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lv_stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void remove_stations_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
