
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
        BO.BusRoute bRoute;
        
        public AdminManagerWindow()
        {
            InitializeComponent();
            List<BusRoute> routeList = bl.GetAllBusRoutes().ToList();
            routeCollection = new ObservableCollection<BusRoute>(routeList);
            Cb_RouteID.DataContext = routeCollection;
            Cb_RouteID.DisplayMemberPath = "Route.BusLineID";
            Cb_RouteID.SelectionChanged += Cb_RouteID_SelectionChanged; //declaring the event handler
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

       
    }
}
