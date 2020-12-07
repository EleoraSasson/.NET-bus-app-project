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
    /// Interaction logic for TravelWindow.xaml
    /// </summary>
    public partial class TravelWindow : Window
    {
        int distance;
        private Bus theBus;
        public TravelWindow(Bus _theBus)
        {
            theBus = _theBus;
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
                    BackgroundWorker travelThread = new BackgroundWorker();
                    travelThread.DoWork += travelThread_DoWork;
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

        private void travelThread_DoWork(object sender, DoWorkEventArgs e)
        {
            theBus.Travel(distance); //refueling the bus
            Thread travel = new Thread(Travelling);
            travel.Start(); //calls refueling func
            travel.Abort(); //stops thread
        }

        public void Travelling()
        {
            int time = getTime(distance);
            Thread.Sleep(time);//time takes for the Bus to refuel "2hrs"
            theBus.BusState = Status.Available; //Bus is now available
        }

        //returns a random travel time for a speed between 20 and 50 km/h
        public int getTime (int dist)
        {
            Random spe = new Random();
            int speed = spe.Next(20, 49);
            int time = (dist / speed); //implicitely converts the result into an int, in an hour form
            int timeSecond = time * 3600; //converts time into seconds
            return timeSecond * 100; //converts into milliseconds 
        }

        private void TravelMileTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           // if (e.KeyValue == 13)
           //see how to do without button for bonus
        }
    }
}
