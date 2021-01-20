
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
        //I believe we have to create a PO Object which can then be put into a collection?
        public IBL bl = BLFactory.GetBL(); //create bl instance 
        public static ObservableCollection<BusRoute> routeCollection;
        public static ObservableCollection<Stations> stationCollection;
        public static ObservableCollection<BusStations> stopCollection;
        public static ObservableCollection<BusRoute> sWithRouteCollection;
        public static ObservableCollection<Buses> fleetCollection;
        //public static int count = fleetCollection.Count();

        BO.BusStations bStations;
        BO.BusRoute bRoute;
        
        public AdminManagerWindow()
        {
            InitializeComponent();
            List<BusRoute> routeList = bl.GetAllBusRoutes().ToList();
            routeCollection = new ObservableCollection<BusRoute>(routeList);
            Cb_RouteID.DataContext = routeCollection;
            Cb_RouteID.DisplayMemberPath = "Route.BusLineID";
            Cb_RouteID.SelectionChanged += Cb_RouteID_SelectionChanged; //declaring the event handler

            List<BusStations> stations = bl.getAllBusStops().ToList();
            stopCollection = new ObservableCollection<BusStations>(stations);
            Cb_StationNo.DataContext = stopCollection;
            Cb_StationNo.DisplayMemberPath = "Stop.StopCode";
            Cb_StationNo.SelectionChanged += Cb_StationNo_SelectionChanged;

            List<Buses> buses = bl.GetAllBuses().ToList();
            fleetCollection = new ObservableCollection<Buses>(buses);
            lv_BusList.DataContext = fleetCollection;
            tb_busNum.Text = fleetCollection.Count().ToString();
        }

        private void Cb_StationNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ExpanderGrid.DataContext = (Cb_StationNo.SelectedItem as BO.BusStations);
            //bStations = (Cb_StationNo.SelectedItem as BO.BusStations);
            //string code = bl.GetBusStationsCode(bStations);
          //  BO.StationWithRoutes swr = bl.GetStationWithRoute(code);

            //List<BusRoute> lines = swr.CurrentLines;
            //sWithRouteCollection = new ObservableCollection<BusRoute>(lines);
            //lb_LineIDs.DataContext = sWithRouteCollection;

            //if (bStations != null)
            //{

            //}


            //ExpanderGridRouteInfo.DataContext = bStations;
            //List<StationWithRoutes> stopList = bl.get().ToList();
            //stationCollection = new ObservableCollection<Stations>(stationList);
            //lb_BusStops.DataContext = stationCollection;

            //if (bRoute != null)
            //{
            //    ExpanderGridRouteInfo.DataContext = bl.GetBusRoute(bRoute);
            //}
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
            //        AddBusWindow addWin = new AddBusWindow();
            //        addWin.Show();
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

            ////curStu = (cbStudentID.SelectedItem as BO.Student);
            ////gridOneStudent.DataContext = curStu;

            ////if (curStu != null)
            ////{
            ////    list of courses of selected student
            ////    RefreshAllRegisteredCoursesGrid();
            ////    list of all courses(that selected student is not registered to it)
            ////    RefreshAllNotRegisteredCoursesGrid();
            ////}

            //void RefreshAllRegisteredCoursesGrid()
            //{
            //    studentCourseDataGrid.DataContext = bl.GetAllCoursesPerStudent(curStu.ID);
            //}

            //void RefreshAllNotRegisteredCoursesGrid()
            //{
            //    List<BO.Course> listOfUnRegisteredCourses = bl.GetAllCourses().Where(c1 => bl.GetAllCoursesPerStudent(curStu.ID).All(c2 => c2.ID != c1.ID)).ToList();
            //    courseDataGrid.DataContext = listOfUnRegisteredCourses;
            //}
        }
        #endregion

        #region BusSchedule

        #endregion

        #endregion

        #region StaffTab
        #endregion

        #region UserTab
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
