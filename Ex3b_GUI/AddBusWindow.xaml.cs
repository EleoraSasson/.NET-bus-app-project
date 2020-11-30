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
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        public AddBusWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StartDateTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //StartDateTextBox.Text = "Please enter bus starting date: ";
        }

        private void LicenseNumTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
