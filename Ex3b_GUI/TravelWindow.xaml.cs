﻿using System;
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
        public TravelWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string dist = TravelMileTextBox.Text;
            distance = Convert.ToInt32(dist);
            //send distance to function, then  if...
            //check the input
            string title = "Gilore Travels INFO: Travel";
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show("Bus has started its travel.", title, buttons, icon); 
            //update mileage, status...
            this.Close();
        }

        private void TravelMileTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //this.
            
        }
    }
}