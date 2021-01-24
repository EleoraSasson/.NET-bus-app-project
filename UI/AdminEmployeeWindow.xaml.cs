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
using BO;
using BLApi;

namespace UI
{
    /// <summary>
    /// Interaction logic for AdminEmployeeWindow.xaml
    /// </summary>
    public partial class AdminEmployeeWindow : Window
    {
        public static IBL bl = BLFactory.GetBL();
        public AdminEmployeeWindow()
        {
            InitializeComponent();
            lb.DataContext = bl.GetAllBusRoutes().ToList();
        }

        private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void lb_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}