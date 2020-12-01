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


namespace Ex3b_GUI
{
    /// <summary>
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>

   
    public partial class AddBusWindow : Window
    {
       // public List<Bus> BusList = new List<Bus>();
        public AddBusWindow(ObservableCollection<Bus> BusList)
        {
            InitializeComponent();
        }

        //private void StartDateCalendar(object sender, TextChangedEventArgs e)
        //{
        //    //StartDateTextBox.Text = "Please enter bus starting date: ";
        //    startDate = StartDateCalendar.Value.Date;
        //}

        private void LicenseNumTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            licenseNum = LicenseNumTextBox.Text; //add check the format
        }

        private void DriverNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            driverName = DriverNameTextBox.Text;
        }

        private void MileageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //{ 
            //    e.Handled = true;
            //}
            mile = Convert.ToInt32(MileageTextBox.Text);
        }

        public DateTime startDate;
        public string licenseNum;
        public int mile;
        public int fuelLeft;
        // public Status status should be determined automatically?
        public string driverName;

        public void AddBus(string l, string n, int f, int m)
        {
            Bus b = new Bus();
            var date = StartDateCalendar.SelectedDate.Value.Date;
            b.BusStartDate = date;
            b.BusLicense = l;
            b.BusDriver = n;
            b.BusFuel = f;
            b.BusMileage = m;
            //add to bus collection somehow
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBus(licenseNum, driverName, fuelLeft, mile);
            this.Close(); //close the window when bus is added
        }
    }
}
