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
        }

        private void B_Fuel_Click(object sender, RoutedEventArgs e)
        {
            string title = "Gilore Travels INFO: Refuel";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Information;
            // this.Close();
            MessageBoxResult result = MessageBox.Show("Bus has been sent for a Refuel", title, buttons, icon);
            if (result == MessageBoxResult.OK)
            {
                BackgroundWorker fuelThread = new BackgroundWorker();
                fuelThread.DoWork += FuelThread_DoWork;
                //fuelThread.ProgressChanged += FuelThread_ProgressChanged;
                //fuelThread.RunWorkerCompleted += FuelThread_RunWorkerCompleted;

                fuelThread.RunWorkerAsync(_theBus);
            }
            else if (result == MessageBoxResult.Cancel)
            {
                //do not change bus status and close messagebox window
            }
        }

        private void FuelThread_DoWork(object sender, DoWorkEventArgs e)
        {
            _theBus.Refuel(); //refueling the bus

            Thread fuel = new Thread(Refueling);
            fuel.Start(); //calls refueling func
            fuel.Abort(); //stops thread
        }


        //private void FuelThread_ProgressChanged; (object sender, ProgressChangedEventArgs e)
        //    {
        //    int progre
        //    }
        public void Refueling()
        {
            Thread.Sleep(12000);//time takes for the Bus to refuel "2hrs"
            string title = "Gilore Travels: Fuel Information";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show("Bus " + _theBus.BusLicense + "has been refueled. ", title, button, icon);
            _theBus.BusState = Status.Available; //Bus is now available
        }

        private void B_Maintenance_Click(object sender, RoutedEventArgs e)
        {
            string title = "Gilore Travels INFO: Maintenance";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Information;
            this.Close();
            MessageBoxResult result = MessageBox.Show("Bus has been sent for Maintenance",title,buttons,icon);
            if (result == MessageBoxResult.OK)
            {
                BackgroundWorker maintenanceThread = new BackgroundWorker();
                maintenanceThread.DoWork += MaintenanceThread_DoWork;
            }
            else if (result == MessageBoxResult.Cancel)
            {
                //do nothing other then close messageBox
            }
        }

        private void MaintenanceThread_DoWork(object sender, DoWorkEventArgs e)
        {
            _theBus.Maintenance();
            Thread maintenance = new Thread(AtService);
            maintenance.Start();
            maintenance.Abort();
         
        }

        public void AtService()
        {
            Thread.Sleep(144000);//time in at service //send msg box to tell user bus is back in service.
            string title = "Gilore Travels: Maintenance Information";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show("Bus " + _theBus.BusLicense + "has completed its maintenance. ", title, button, icon);
            _theBus.BusState = Status.Available; //Bus is now available
        }

        private void B_Travel_Click(object sender, RoutedEventArgs e)
        {
            TravelWindow tw = new TravelWindow(_theBus);
            tw.Show();
            this.Close();
        }
    }
}
