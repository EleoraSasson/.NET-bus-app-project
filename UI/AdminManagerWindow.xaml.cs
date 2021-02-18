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
using System.Diagnostics;

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
        public static ObservableCollection<AdminPortal> adminCollection;
        public static ObservableCollection<EmployeePortal> employeeCollection;
        public static ObservableCollection<ScheduleOfRoute> companySchedule;


        
        BO.BusStations bStation;
        BO.BusRoute bRoute;
        BackgroundWorker timerBworker;
        private Stopwatch stopwatch;
        private int speedOfSimulation;
        private bool isTimerRun;

        public AdminManagerWindow(AdminPortal loginAdmin)
        {
            InitializeComponent();

            tb_adminName.DataContext = loginAdmin;

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

            List<Buses> buses = bl.GetAllBuses().ToList();
            fleetCollection = new ObservableCollection<Buses>(buses);
            lv_BusList.DataContext = fleetCollection;
          

            List<UserPortal> users = bl.GetAllUsers().ToList();
            usersCollection = new ObservableCollection<UserPortal>(users);
            lv_Users.DataContext = usersCollection;

            List<EmployeePortal> employees = bl.GetAllEmployees().ToList();
            employeeCollection = new ObservableCollection<EmployeePortal>(employees);
            lv_Staff.DataContext = employeeCollection;

            List<ScheduleOfRoute> schedules = bl.GetAllScheduleOfRoutes().ToList();
            companySchedule = new ObservableCollection<ScheduleOfRoute>(schedules);
            lv_companySched.DataContext = companySchedule;

            cb_Simulation.DataContext = stopCollection;
            cb_Simulation.DisplayMemberPath = "Stop.StopCode";
            cb_Simulation.SelectionChanged += Cb_Simulation_SelectionChanged;

            stopwatch = new Stopwatch();
            timerBworker = new BackgroundWorker(); 
            timerBworker.DoWork += TimerBworker_DoWork;
            timerBworker.RunWorkerCompleted += TimerBworker_RunWorkerCompleted;
            timerBworker.ProgressChanged += TimerBworker_ProgressChanged;
            timerBworker.WorkerReportsProgress = true;
            timerBworker.WorkerSupportsCancellation = true;
        }


        private void TimerBworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            stopwatch.Reset();
        }
        private void TimerBworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string timerText = stopwatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            this.Tb_Clock.Text = timerText;
        }

       
        private void sl_Simulation_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speedOfSimulation = (int)sl_Simulation.Value;
        }

        private void TimerBworker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(isTimerRun)
            {
                timerBworker.ReportProgress(0);
                Thread.Sleep(1000);
            }
            
        }

        private void b_StartClock_Click(object sender, RoutedEventArgs e)
        {
            if (!isTimerRun)
            {
                stopwatch.Start();
                isTimerRun = true;
                timerBworker.RunWorkerAsync(); //starts the background worker
            }
           
        }

        private void b_StopClock_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRun)
            {
                stopwatch.Stop();
                isTimerRun = false;
            }
        }
        private void Cb_Simulation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bStation = (Cb_StationNo.SelectedItem as BO.BusStations);
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

        //Select more Options Button:
        private void B_Options_Click(object sender, RoutedEventArgs e)
        {
            Buses selectedBus;
            ListViewItem lvi = GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

            if (lvi != null)
            {
                lv_BusList.SelectedIndex = lv_BusList.ItemContainerGenerator.IndexFromContainer(lvi);
                selectedBus = (Buses)lv_BusList.SelectedItem;
                OptionsWindow optionsWin = new OptionsWindow(selectedBus, lv_BusList);
                optionsWin.Show();
            }
        }

        public static DependencyObject GetAncestorByType(DependencyObject element, Type type)
        {
            if (element == null) return null;

            if (element.GetType() == type) return element;

            return GetAncestorByType(VisualTreeHelper.GetParent(element), type);
        }



        #endregion

        #region BusRoute
        private void b_AddRoute_Click(object sender, RoutedEventArgs e)
        {
            AddRouteWindow addRouteWin = new AddRouteWindow();
            addRouteWin.Show();
        }

        private void b_UpdateRoute_Click(object sender, RoutedEventArgs e)
        {
            UpdateRouteWindow updateRouteWin = new UpdateRouteWindow();
            updateRouteWin.Show();
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

        private void b_AddStation_Click (object sender, RoutedEventArgs e)
        {
            AddStationWindow addSTat = new AddStationWindow();
            addSTat.Show();
        }
        #endregion

        #region BusSchedule

        private void b_RefreshSchedule_Click(object sender, RoutedEventArgs e)
        {
            List<ScheduleOfRoute> schedules = bl.GetAllScheduleOfRoutes().ToList();
            companySchedule = new ObservableCollection<ScheduleOfRoute>(schedules);
            lv_companySched.DataContext = companySchedule;
        }
        #endregion

        #endregion

        #region StaffTab
        private void lv_Staff_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EmployeePortal selectedEmployee;

            ListViewItem listViewItem = GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;

            if (listViewItem != null)
            {
                lv_Staff.SelectedIndex = lv_Staff.ItemContainerGenerator.IndexFromContainer(listViewItem);
                selectedEmployee = (EmployeePortal)lv_Staff.SelectedItem;
                gridSelectedStaff.DataContext = selectedEmployee;
            }
        }

        private void lv_Staff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region UserTab
        private void lv_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserLastName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
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

    }
}