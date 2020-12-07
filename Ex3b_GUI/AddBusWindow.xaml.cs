﻿using System;
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
                MessageBox.Show("Error: Fields have been left Blank.", title, button, icon);
                this.Close();
            }
            else
            {
                MainWindow.BusList.Add(b);
            }
        }

        //Below are the reading in of user input from various elements in the add bus window:
        private void LicenseNumTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //read in string
            var userInput = LicenseNumTextBox.Text; //find out why it is reading only the first character and not allowing a whole string ... event called Validation
            //check format
            CheckLicenseNum(userInput);
            //assign new license to the NewBus:
            NewBus.BusLicense = LicenseNumTextBox.Text;
        }

        private void CheckLicenseNum(string userInput)
        {

            Regex regex = new Regex(@"^\d{2}[-]\d{3}[-]\d{2}$");
            Regex regex1 = new Regex(@"^\d{3}[-]\d{2}[-]\d{3}$");

            if (StartDateCalendar.SelectedDate.Value.Date.Year < 2018)
            {
                if (regex.IsMatch((userInput)))
                {
                    string title = "Gilore Travels ERROR: License number";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBox.Show("Wrong input format.", title, button, icon);
                    this.Close();
                }
            }
            else if (regex1.IsMatch((userInput)) && regex1.IsMatch(LicenseNumTextBox.Text))
            {
                string title = "Gilore Travels ERROR: License number";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Wrong input format.", title, button, icon);
                this.Close();
            }
            else this.Close();
        }

        private void DriverNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number;
            bool success = Int32.TryParse(MileageTextBox.Text, out number);
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

        //check if the textbox have been provided with some values
        public bool checkText ()
        {
            if (LicenseNumTextBox.Text.Length == 0 || MileageTextBox.Text.Length == 0 || DriverNameTextBox.Text.Length == 0)
                return false;
            else return true;
        }

        
    }
}






