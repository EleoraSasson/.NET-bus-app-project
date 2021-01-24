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
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        public IBL bl = BLFactory.GetBL();
        BusStations stat = new BusStations();
        public AddStationWindow()
        {
            InitializeComponent();
        }

        private void tbx_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            name = tbx_name.Text;
        }

        private void tbx_address_TextChanged(object sender, TextChangedEventArgs e)
        {
            addr = tbx_address.Text;
        }

        private void tbx_lon_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            
        }

        private void tbx_lat_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void tbx_code_TextChanged(object sender, TextChangedEventArgs e)
        {
            code = tbx_code.Text;
        }

        private void addStatButton_click(object sender, RoutedEventArgs e)
        {
            addStation(stat);
            bl.AddBusStations(stat);
            this.Close(); //close the window when bus is added
        }

        private void addStation(BusStations s)
        {
            latitude = float.Parse(tbx_lat.Text);
            longitude = float.Parse(tbx_lon.Text);
            if (!checkText())
            {
                string title = "Gilore Travels ERROR: Add Station";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Error: Fields have been left blank.", title, button, icon);
                this.Close();
            }
            else
            {
                bl.SetBusStop(s, code, name, addr, longitude, latitude);
                AdminManagerWindow.stopCollection.Add(s);

            }
        }

        private bool checkText()
        {
            if (tbx_address.Text.Length == 0 || tbx_code.Text.Length == 0 || tbx_lat.Text.Length == 0 || tbx_lon.Text.Length == 0 || tbx_name.Text.Length == 0)
                return false;
            else if (Convert.ToInt32(tbx_lon.Text) < 34.30 || Convert.ToInt32(tbx_lon.Text) > 35.50)
                {
                    string title = "Gilore Travels ERROR: Longitude";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBox.Show("Error: longitude must be between 34.30 and 35.50.", title, button, icon);
                    tbx_lon.Clear();
                    return false;
                }
            else if (Convert.ToInt32(tbx_lat.Text) < 31.30 || Convert.ToInt32(tbx_lat.Text) > 33.30)
            {
                string title = "Gilore Travels ERROR: Latitude";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Error: latitude must be between 31.30 and 33.30.", title, button, icon);
                tbx_lat.Clear();
                return false;
            }


           
                else return true;
        }

        public float longitude;
        public float latitude;
        public string addr;
        public string code;
        public string name;
    }
}
