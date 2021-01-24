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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public IBL bl = BLFactory.GetBL(); //create bl instance 
        public static ObservableCollection<BusRoute> routeCollection;
        public static ObservableCollection<Stations> stationCollection;
        public static ObservableCollection<BusStations> stopCollection;
        public static ObservableCollection<BusRoute> sWithRouteCollection;
        public static ObservableCollection<Buses> fleetCollection;
        public static ObservableCollection<ScheduleOfRoute> companySchedule;

        BO.BusStations bStation;
        BO.BusRoute bRoute;

        public UserWindow()
        {
            InitializeComponent();

            List<BusRoute> routeList = bl.GetAllBusRoutes().ToList();
            routeCollection = new ObservableCollection<BusRoute>(routeList);
            Cb_RouteID.DataContext = routeCollection;
            Cb_RouteID.DisplayMemberPath = "Route.BusLineID";
            Cb_RouteID.SelectionChanged += Cb_RouteID_SelectionChanged;

            List<BusStations> stations = bl.GetAllBusStops().ToList();
            stopCollection = new ObservableCollection<BusStations>(stations);
            Cb_StationNo.DataContext = stopCollection;
            Cb_StationNo.DisplayMemberPath = "Stop.StopCode";
            Cb_StationNo.SelectionChanged += Cb_StationNo_SelectionChanged;

            List<ScheduleOfRoute> schedules = bl.GetAllScheduleOfRoutes().ToList();
            companySchedule = new ObservableCollection<ScheduleOfRoute>(schedules);
            lv_companySched.DataContext = companySchedule;

        }


        #region BusRoute
        private void b_AddRoute_Click(object sender, RoutedEventArgs e)
        {
            AddRouteWindow addRouteWin = new AddRouteWindow();
            addRouteWin.Show();
        }

        private void Cb_RouteID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bRoute = (Cb_RouteID.SelectedItem as BO.BusRoute);
            ExpanderGridRouteInfo.DataContext = bRoute;
            List<Stations> stationList = bl.GetAllStationsInBusRoute(bRoute).ToList();
            stationCollection = new ObservableCollection<Stations>(stationList);
            lb_BusStops.DataContext = stationCollection;

            if (bRoute != null)
            {
                ExpanderGridRouteInfo.DataContext = bl.GetBusRoute(bRoute);
            }
        }
        #endregion

        #region BusStations
        private void Cb_StationNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bStation = (Cb_StationNo.SelectedItem as BO.BusStations);
            ExpanderGrid.DataContext = bStation;
            lb_LineIDs.DataContext = bl.GetRoutesofStation(bStation);
        }

        private void b_AddStation_Click(object sender, RoutedEventArgs e)
        {
            AddStationWindow addSTat = new AddStationWindow();
            addSTat.Show();
        }
        #endregion

        #region BusSchedule

        #endregion
    }
}
