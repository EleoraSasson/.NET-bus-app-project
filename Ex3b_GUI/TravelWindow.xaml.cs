using System;
using System.Collections.Generic;
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
                MessageBoxButton button = MessageBoxButton.OK; 
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show("Bus has been sent on trip", title, button, icon);
            }
            else
            {
                string title = "Gilore Travels ERROR: Travel";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("The bus does not contain enough fuel for this route.", title, button, icon);
            }
           
            //check the input
            //Then update the status after the thread 


            //string title = "Gilore Travels INFO: Travel";
            //MessageBoxButton buttons = MessageBoxButton.OK;
            //MessageBoxImage icon = MessageBoxImage.Information;
            //MessageBoxResult result = MessageBox.Show("Bus has started its travel.", title, buttons, icon); 
            //update mileage, status...
            this.Close();
        }

        private void TravelMileTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //this.
            
        }
    }
}
