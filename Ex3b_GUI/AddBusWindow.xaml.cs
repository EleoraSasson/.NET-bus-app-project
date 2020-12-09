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
using System.Text.RegularExpressions;


namespace Ex3b_GUI
{
    public partial class AddBusWindow : Window
    {
        //creating Bus to add to fleet:
        Bus NewBus = new Bus();
        
        //Intialising the AddBusWindow:
        public AddBusWindow()
        {
            InitializeComponent();
        }

        //Action done when Add Bus is selected:
        private void B_AddBus_Click(object sender, RoutedEventArgs e)
        {
            AddBus(NewBus);
            this.Close(); //close the window when bus is added
        }

        //add bus method:
        public void AddBus(Bus b)
        {
            if (!checkText())
            {
                string title = "Gilore Travels ERROR: Add Bus";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Error: Fields have been left blank.", title, button, icon);
                this.Close();
            }
            else
            {
                MainWindow.BusList.Add(b);
            }
        }


        private void LicenseNumTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (StartDateCalendar.SelectedDate == null)
            {
                string title = "Gilore Travels ERROR: License number";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Error: no date has been chosen", title, button, icon);
                LicenseNumTextBox.Clear();
                return;
            }
            if (CheckLicenseNum(LicenseNumTextBox.Text))
                NewBus.BusLicense = LicenseNumTextBox.Text;
            else
            {
                string title = "Gilore Travels ERROR: License number";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Wrong input format.", title, button, icon);
                this.Close();
            }
        }

        private bool CheckLicenseNum(string userInput)
        {

            Regex regex = new Regex(@"^\d{2}[-]\d{3}[-]\d{2}$");
            Regex regex1 = new Regex(@"^\d{3}[-]\d{2}[-]\d{3}$");
           

            if (StartDateCalendar.SelectedDate.Value.Date.Year < 2018) 
            {
                if (regex.IsMatch((userInput)))
                {
                    return true;
                }
            }
            else if (regex1.IsMatch(userInput))
            {
                return true;
            }
           
            return false;
        }

        private void FuelTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number;
            bool success = Int32.TryParse(FuelTextBlock.Text, out number);
            if (success)
            {
                if (number > 1200)
                {
                    string title = "Gilore Travels ERROR: Fuel";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBox.Show("Fuel has been set to 1200.", title, button, icon); //bc it cannot be more. Or just error?
                    this.Close();
                    number = 1200;
                }
                NewBus.BusFuel = number;
            }
            else
            {
                string title = "Gilore Travels ERROR: Fuel";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Wrong input format.", title, button, icon);
                this.Close();
            }
        }

        private void MileageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        { 
            int number;
            bool success = Int32.TryParse(MileageTextBox.Text, out number);
            if (success)
            {
                NewBus.BusMileage = number;
            }
            else
            {
                string title = "Gilore Travels ERROR: Mileage";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Wrong input format.", title, button, icon);
                this.Close(); 
            }
        }

        private void DriverNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number;
            bool success = Int32.TryParse(DriverNameTextBox.Text, out number);
            if (!success)
            {
                NewBus.BusDriver = DriverNameTextBox.Text;
            }
            else
            {
                string title = "Gilore Travels ERROR: Driver";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Wrong input format.", title, button, icon);
                this.Close();
            }

        }

        //check if the textbox have been provided with some values
        public bool checkText ()
        {
            if (LicenseNumTextBox.Text.Length == 0 || MileageTextBox.Text.Length == 0 || DriverNameTextBox.Text.Length == 0)
                return false;
            else return true;
        }

        private void LicenseNumTextBox_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

    }
}






