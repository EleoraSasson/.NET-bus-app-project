using BLApi;
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
using BO;

namespace UI
{
    /// <summary>
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        public IBL bl = BLFactory.GetBL();
        Buses newBus = new Buses();
        public AddBusWindow()
        {
            InitializeComponent();
            StartDateCalendar.DisplayDateEnd = DateTime.Today;
        }

        //Action done when Add Bus is selected:
        private void B_AddBus_Click(object sender, RoutedEventArgs e)
        {
            AddBus(newBus);
            bl.AddBus(license, (DateTime)StartDateCalendar.SelectedDate, (DateTime)StartDateCalendar.SelectedDate, mileage, fuel);
            this.Close(); //close the window when bus is added
        }


        //add bus method:
        public void AddBus(Buses b)
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
                AdminManagerWindow.fleetCollection.Add(b);
                
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
        }

        private void setLicense(string s)
        {
            string s1; string s2; string s3;

            if (s.Length < 7)
            {
                string title = "Gilore Travels ERROR: License number";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("License number must have 7 digits.", title, button, icon);
                this.Close();
                return;
            }

            if (StartDateCalendar.SelectedDate.Value.Year > 2018)
            {
                s1 = s.Substring(0, 3); //XXX
                s2 = s.Substring(2, 2); //XX
                s3 = s.Substring(4, 3); //XXX
                license = s1 + "-" + s2 + "-" + s3;
            }
            else
            {
                s1 = s.Substring(0, 2); //XX
                s2 = s.Substring(2, 3); //XXX
                s3 = s.Substring(5, 2); //XX
                license = s1 + "-" + s2 + "-" + s3;
            }
        }
        private void FuelTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            setLicense(LicenseNumTextBox.Text); //converts the license number into the right format

            int number;
            bool success = Int32.TryParse(FuelTxt.Text, out number);
            if (success)
            {
                fuel = number;
            }
            else
            {
                string title = "Gilore Travels ERROR: Fuel";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Wrong input format.", title, button, icon);
                this.Close();
            }
            if (fuel > 1200)
            {
                string title = "Gilore Travels ERROR: Fuel";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Fuel has been set to 1200.", title, button, icon); //bc it cannot be more. Or just error?
                fuel = 1200;
            }
        }

        private void MileageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number;
            bool success = Int32.TryParse(MileageTextBox.Text, out number);
            if (success)
            {
                mileage = number;
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

        //check if the textbox have been provided with some values
        public bool checkText()
        {
            if (LicenseNumTextBox.Text.Length == 0 || MileageTextBox.Text.Length == 0)
                return false;
            else return true;
        }

        private void LicenseNumTextBox_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        public string license;
        public int mileage;
        public int fuel;
    }
}
