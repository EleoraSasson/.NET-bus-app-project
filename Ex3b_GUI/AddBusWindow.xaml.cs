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
    /// <summary>
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>

   
    public partial class AddBusWindow : Window
    {
        private ObservableCollection<Bus> _BusList;

        public AddBusWindow(ObservableCollection<Bus> BusList)
        {
            _BusList = BusList;
            InitializeComponent();
        }

        private void LicenseNumTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex(@"^\d{2}[-]\d{3}[-]\d{2}$");
            Regex regex1 = new Regex(@"^\d{3}[-]\d{2}[-]\d{3}$"); //check according to the date?
            if (regex.IsMatch((LicenseNumTextBox.Text)) && regex1.IsMatch(LicenseNumTextBox.Text))
            {
                string title = "Gilore Travels ERROR: License number";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Wrong input format.", title, button, icon);
                this.Close();
            }
            licenseNum = LicenseNumTextBox.Text; //add check the format
        }

        private void DriverNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("[a-zA-Z]+");
            if (regex.IsMatch(DriverNameTextBox.Text))
            {
                string title = "Gilore Travels ERROR: Driver";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Wrong input format.", title, button, icon);
                this.Close();
            }
            driverName = DriverNameTextBox.Text;
        }

        private void MileageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(MileageTextBox.Text))
            {
                string title = "Gilore Travels ERROR: Mileage";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Wrong input format.", title, button, icon);
                this.Close();
            }
            mile = Convert.ToInt32(MileageTextBox.Text); //why come here?
        }

        public DateTime startDate;
        public string licenseNum;
        public int mile;
        public int fuelLeft; 
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
            b.BusState = Status.Available;
            _BusList.Add(b);

           // lv_BusList.ItemsSource = _BusList;
        }

        private void B_AddBus_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
           
            main.lv_BusList.SourceUpdated += Lv_BusList_SourceUpdated;
            this.Close(); //close the window when bus is added
        }

        private void Lv_BusList_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            //var collection = new ObservableCollection<FooBar>();
            //collection.Add(fooBar1);

            //_listBox.ItemsSource = collection;

        }
    }
}






