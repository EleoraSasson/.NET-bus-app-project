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
        private BackgroundWorker fuelBW;
        private BackgroundWorker maintenanceBW;

        private Bus _theBus;
        public OptionsWindow(Bus theBus)
        {
            _theBus = theBus;
            InitializeComponent();
            this.fuelBW = new BackgroundWorker();
            this.fuelBW.DoWork += fuelBW_DoWork;
            this.maintenanceBW = new BackgroundWorker();
            this.maintenanceBW.DoWork += maintenanceBW_DoWork;
        }

        private void B_Fuel_Click(object sender, RoutedEventArgs e)
        {
            string title = "Gilore Travels - Refuel";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show("Are you sure that you want Bus " + _theBus.BusLicense + " to be sent for Refueling?", title, buttons, icon);

            this.Close();
            if (result == MessageBoxResult.Yes)
            {
                string title1 = "Gilore Travels INFO: Refuel";
                MessageBoxButton buttons1 = MessageBoxButton.OK;
                MessageBoxImage icon1 = MessageBoxImage.Information;
                MessageBox.Show("Bus "+ _theBus.BusLicense + " has been sent for Refueling", title1, buttons1, icon1);
                if (fuelBW.IsBusy != true)
                {
                    fuelBW.RunWorkerAsync(); //calls DoWork of fuelBW
                }
            }
            else if (result == MessageBoxResult.No)
            {
                string title2 = "Gilore Travels INFO: Refuel";
                MessageBoxButton buttons2 = MessageBoxButton.OK;
                MessageBoxImage icon2 = MessageBoxImage.Information;
                MessageBox.Show("Refueling for Bus " +_theBus.BusLicense + " has been canceled" , title2, buttons2, icon2);
            }
        }

        private void fuelBW_DoWork(object sender, DoWorkEventArgs e) //DoWork of fuelBW
        {
            BackgroundWorker FhelperBW = sender as BackgroundWorker;
            Refueling(FhelperBW);
        }

        public void Refueling(BackgroundWorker bw)
        {
            _theBus.Refuel();
            Thread.Sleep(120000);//time takes for the Bus to refuel "2hrs"
            string title = "Gilore Travels: Fuel Information";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show("Bus " + _theBus.BusLicense + " has been refueled. ", title, button, icon);
            _theBus.BusState = Status.Available; //Bus is now available
        }

        private void B_Maintenance_Click(object sender, RoutedEventArgs e)
        {
            string title = "Gilore Travels - Maintenance";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show("Are you sure that you want Bus " + _theBus.BusLicense + " to be sent for Maintenance?", title, buttons, icon);

            this.Close();
            if (result == MessageBoxResult.Yes)
            {
                string title1 = "Gilore Travels INFO: Maintenance";
                MessageBoxButton buttons1 = MessageBoxButton.OK;
                MessageBoxImage icon1 = MessageBoxImage.Information;
                MessageBox.Show("Bus " + _theBus.BusLicense + " has been sent for Maintenance", title1, buttons1, icon1);
                if (maintenanceBW.IsBusy != true)
                {
                    maintenanceBW.RunWorkerAsync(); //calls DoWork of maintenanceBW
                }
            }
            else if (result == MessageBoxResult.No)
            {
                string title2 = "Gilore Travels INFO: Maintenance";
                MessageBoxButton buttons2 = MessageBoxButton.OK;
                MessageBoxImage icon2 = MessageBoxImage.Information;
                MessageBox.Show("Maintenance for Bus " + _theBus.BusLicense + " has been canceled", title2, buttons2, icon2);
            }
        }


        private void maintenanceBW_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker MhelperBW = sender as BackgroundWorker;
            AtService(MhelperBW);
        }

        public void AtService(BackgroundWorker bw)
        {
            _theBus.Maintenance();
            Thread.Sleep(144000);//time takes for the Bus to be repaired "24hrs"
            string title = "Gilore Travels: Maintenance Information";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show("Bus " + _theBus.BusLicense + " has completed its maintenance. ", title, button, icon);
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
