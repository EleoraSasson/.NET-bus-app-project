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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public static IBL bl = BLFactory.GetBL(); 
        private static ObservableCollection<BusRoute> routes = new ObservableCollection<BusRoute>();

        public Window1()
        {
            InitializeComponent();
            //routes = 
            cb_RouteID.DataContext = bl.GetAllBusRoutes().ToList(); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busRouteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busRouteViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busRouteViewSource.Source = [generic data source]
        }

        private void cb_RouteID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void busLineIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void busLineIDComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
