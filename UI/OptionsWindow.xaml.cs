using BLApi;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
  public partial class OptionsWindow : Window
    {
        public IBL bl = BLFactory.GetBL(); //create bl instance 
        private BackgroundWorker fuelBW;
        private BackgroundWorker maintenanceBW;

        private Buses _theBus;
        private ListView _theBusListView;
        public OptionsWindow(Buses theBus, ListView theBusListView)
        {
            _theBus = theBus;
            _theBusListView = theBusListView;
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
            MessageBoxResult result = MessageBox.Show("Are you sure that you want Bus to be sent for Refueling?", title, buttons, icon);

            this.Close();
            if (result == MessageBoxResult.Yes)
            {
                string title1 = "Gilore Travels INFO: Refuel";
                MessageBoxButton buttons1 = MessageBoxButton.OK;
                MessageBoxImage icon1 = MessageBoxImage.Information;
                MessageBox.Show("Bus has been sent for Refueling", title1, buttons1, icon1);
                if (fuelBW.IsBusy != true)
                {
                    _theBusListView.Items.Refresh();
                    fuelBW.RunWorkerAsync(); //calls DoWork of fuelBW
                }
            }
            else if (result == MessageBoxResult.No)
            {
                string title2 = "Gilore Travels INFO: Refuel";
                MessageBoxButton buttons2 = MessageBoxButton.OK;
                MessageBoxImage icon2 = MessageBoxImage.Information;
                MessageBox.Show("Refueling for Bus has been canceled", title2, buttons2, icon2);
            }

        }

        private void fuelBW_DoWork(object sender, DoWorkEventArgs e) //DoWork of fuelBW
        {
            BackgroundWorker FhelperBW = sender as BackgroundWorker;
            Refueling(FhelperBW);
        }

        public void Refueling(BackgroundWorker bw)
        {
            bl.busRefuel(_theBus);
            Thread.Sleep(120000);//time takes for the Bus to refuel "2hrs"
            string title = "Gilore Travels: Fuel Information";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show("Bus has been refueled. ", title, button, icon);
            bl.statusToAvailable(_theBus); //Bus is now available    
            this.Dispatcher.Invoke(() =>
            {
                _theBusListView.Items.Refresh();
            });
        }


        private void B_Maintenance_Click(object sender, RoutedEventArgs e)
        {
            string title = "Gilore Travels - Maintenance";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show("Are you sure that you want Bus to be sent for Maintenance?", title, buttons, icon);

            this.Close();
            if (result == MessageBoxResult.Yes)
            {
                string title1 = "Gilore Travels INFO: Maintenance";
                MessageBoxButton buttons1 = MessageBoxButton.OK;
                MessageBoxImage icon1 = MessageBoxImage.Information;
                MessageBox.Show("Bus has been sent for Maintenance", title1, buttons1, icon1);
                if (maintenanceBW.IsBusy != true)
                {
                    _theBusListView.Items.Refresh();
                    maintenanceBW.RunWorkerAsync(_theBusListView); //calls DoWork of maintenanceBW
                }
            }
            else if (result == MessageBoxResult.No)
            {
                string title2 = "Gilore Travels INFO: Maintenance";
                MessageBoxButton buttons2 = MessageBoxButton.OK;
                MessageBoxImage icon2 = MessageBoxImage.Information;
                MessageBox.Show("Maintenance for Bus has been canceled", title2, buttons2, icon2);
            }
        }


        private void maintenanceBW_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker MhelperBW = sender as BackgroundWorker;
            AtService(MhelperBW);
        }

        public void AtService(BackgroundWorker bw)
        {
            bl.busMaintenance(_theBus);
            Thread.Sleep(144000);//time takes for the Bus to be repaired "24hrs" 
            string title = "Gilore Travels: Maintenance Information";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show("Bus has completed its maintenance. ", title, button, icon);
            bl.statusToAvailable(_theBus); //Bus is now available  
            this.Dispatcher.Invoke(() =>
            {
                _theBusListView.Items.Refresh();
            });

        }

        private void B_Travel_Click(object sender, RoutedEventArgs e)
        {
            TravelWindow tw = new TravelWindow(_theBus, _theBusListView);
            tw.Show();
            this.Close();
        }
    }
}

