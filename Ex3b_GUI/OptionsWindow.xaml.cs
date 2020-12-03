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

namespace Ex3b_GUI
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private Bus _theBus;
        public OptionsWindow(Bus theBus)
        {
            _theBus = theBus;
            InitializeComponent();
            MessageBox.Show("Bus licence number: " + _theBus.BusLicense);
        }

        private void B_Fuel_Click(object sender, RoutedEventArgs e)
        {
            string title = "Gilore Travels INFO: Refuel";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show("Bus has been sent for a Refuel",title,buttons,icon);
            if (result == MessageBoxResult.OK)
            {
                //change status of bus and use threading
            }
            else if (result == MessageBoxResult.Cancel)
            {
                //do not change bus status and close window
            }
        }

        private void B_Maintenance_Click(object sender, RoutedEventArgs e)
        {
            string title = "Gilore Travels INFO: Maintenance";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show("Bus has been sent for Maintenance",title,buttons,icon);
            if (result == MessageBoxResult.OK)
            {
                //change status of bus and use threading
                //SendforMaintenance();
            }
            else if (result == MessageBoxResult.Cancel)
            {
                //do nothing other then close messageBox
            }
        }

        private void SendforMaintenance(Bus bus)
        {
            
        }

        private void B_Travel_Click(object sender, RoutedEventArgs e)
        {
            TravelWindow tw = new TravelWindow();//this);
            tw.Show();
        }
    }
}
