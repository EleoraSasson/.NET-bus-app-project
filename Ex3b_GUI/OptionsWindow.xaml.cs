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
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
        }

        private void B_Fuel_Click(object sender, RoutedEventArgs e)
        {
            string title = "Gilore Travels INFO: Refuel";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show("Bus has been sent for a Refuel",title,buttons,icon);
        }

        private void B_Maintenance_Click(object sender, RoutedEventArgs e)
        {
            string title = "Gilore Travels INFO: Maintenance";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBox.Show("Bus has been sent for Maintenance",title,buttons,icon);
        }
    }
}
