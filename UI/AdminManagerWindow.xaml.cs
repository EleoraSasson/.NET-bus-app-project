
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using BLApi;
using BO;

namespace UI
{
    /// <summary>
    /// Interaction logic for AdminManagerWindow.xaml
    /// </summary>
    public partial class AdminManagerWindow : Window
    {
        public IBL bl = BLFactory.GetBL(); //create bl instance 
        public static ObservableCollection<BusRoute> routeCollection;
        public static ObservableCollection<Stations> stationCollection;
        public static ObservableCollection<BusStations> stopCollection;
        public static ObservableCollection<BusRoute> sWithRouteCollection;
        public static ObservableCollection<Buses> fleetCollection;
        public static ObservableCollection<UserPortal> usersCollection;

        public static ObservableCollection<ScheduleOfRoute> companySchedule;
        BO.BusStations bStation;
        BO.BusRoute bRoute;

        public AdminManagerWindow()
        {
            InitializeComponent();

            List<BusRoute> routeList = bl.GetAllBusRoutes().ToList();
            routeCollection = new ObservableCollection<BusRoute>(routeList);
            Cb_RouteID.DataContext = routeCollection;
            Cb_RouteID.DisplayMemberPath = "Route.BusLineID";
            Cb_RouteID.SelectionChanged += Cb_RouteID_SelectionChanged; //declaring the event handler

            List<BusStations> stations = bl.GetAllBusStops().ToList();
            stopCollection = new ObservableCollection<BusStations>(stations);
            Cb_StationNo.DataContext = stopCollection;
            Cb_StationNo.DisplayMemberPath = "Stop.StopCode";
            Cb_StationNo.SelectionChanged += Cb_StationNo_SelectionChanged;

            List<Buses> buses = bl.GetAllBuses().ToList();
            fleetCollection = new ObservableCollection<Buses>(buses);
            lv_BusList.DataContext = fleetCollection;
            tb_busNum.Text = fleetCollection.Count().ToString();

            List<ScheduleOfRoute> routesSchedules = new List<ScheduleOfRoute>();
            foreach (var route in routeList)
            {
                ScheduleOfRoute singleRouteSched = bl.GetScheduleOfRoute(route);
                routesSchedules.Add(singleRouteSched);
            }
            companySchedule = new ObservableCollection<ScheduleOfRoute>(routesSchedules);
            Dg_BusSchedule.DataContext = companySchedule; 
            

            cb_Simulation.DataContext = stopCollection;
            cb_Simulation.DisplayMemberPath = "Stop.StopCode";
            cb_Simulation.SelectionChanged += Cb_Simulation_SelectionChanged;

            List<UserPortal> users = bl.GetAllUsers().ToList();
            usersCollection = new ObservableCollection<UserPortal>();
            lv_Users.DataContext = usersCollection;
        }

        private void Cb_Simulation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            bStation = (Cb_StationNo.SelectedItem as BO.BusStations);
            List<BusRoute> routesThroughStop = bl.GetRoutesofStation(bStation).ToList();
            foreach (var route in routesThroughStop)
            {
                ScheduleOfRoute sched = bl.GetScheduleOfRoute(route);
                companySchedule.Add(sched);
            }
            
            dg_Simulation.DataContext = companySchedule;
        }

     

        #region ManagerTab
        #endregion

        #region BusTab
        #region BusFleet

        private void lv_BusList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void b_AddBus_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow addWin = new AddBusWindow();
            addWin.Show();
        }

        #endregion

        #region BusRoute
        private void b_AddRoute_Click(object sender, RoutedEventArgs e)
        {

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
        #endregion

        #region BusSchedule

        #endregion

        #endregion

        #region StaffTab
        #endregion

        #region UserTab
        #endregion

        #region Simulation

        #endregion


        private void Dg_BusSchedule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Cb_Region_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Cb_Search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tb_SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lb_BusStops_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lb_LineIDs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UserLastName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
