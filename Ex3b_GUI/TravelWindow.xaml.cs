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
    public partial class TravelWindow : Window
    {
        int distance;
        private Bus theBus;
        private BackgroundWorker travelBW;
        public TravelWindow(Bus _theBus)
        {
            theBus = _theBus;
            InitializeComponent();
            this.travelBW = new BackgroundWorker();
            this.travelBW.DoWork += travelBW_DoWork;
        }

        private void travelBW_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker ThelperBW = sender as BackgroundWorker;
            Travelling();
        }

        public void Travelling()
        {
            int time = getTime(distance);
            Thread.Sleep(time);//time takes for the Bus to refuel "2hrs"
            string title = "Gilore Travels: Travel Information";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show("Bus " + theBus.BusLicense + " has finished its travel. ", title, button, icon);
            theBus.BusState = Status.Available; //Bus is now available
        }

        //returns a random travel time for a speed between 20 and 50 km/h
        public int getTime (int dist)
        {
            Random spe = new Random();
            int speed = spe.Next(20, 50);
            int time = (dist / speed); //implicitely converts the result into an int, in an hour form
            int timeSecond = time * 3600; //converts time into seconds
            return timeSecond * 1000; //converts into milliseconds 
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                travel(theBus);
            }
        }

        private void travel(Bus b)
        {
            if (theBus.needMaintenance())
            {
                this.Close();
                string title = "Gilore Travels INFO: Travel";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show("Bus needs to be sent to maintenance.", title, buttons, icon);
                return;
            }

            string dist = TravelMileTextBox.Text;
            distance = Convert.ToInt32(dist);
            if (theBus.BusFuel > distance)
            {
                theBus.BusMileage += distance; //updates mileage
                theBus.BusFuel -= distance; //updates fuel
                string title = "Gilore Travels INFO: Travel";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Information;
                this.Close();
                MessageBoxResult result = MessageBox.Show("Bus has been sent on trip.", title, buttons, icon);
                if (result == MessageBoxResult.OK)
                {
                    travelBW.RunWorkerAsync(); //calls on the DoWork of travelBW
                }
            }
            else
            {
                string title = "Gilore Travels ERROR: Travel";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("The bus does not contain enough fuel for this route.", title, button, icon);
            }
            this.Close();
        }
    }
}
