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
            Regex regex = new Regex(@"^\d{2}[-]\d{3}[-]\d{2}$"); //
            Regex regex1 = new Regex(@"^\d{3}[-]\d{2}[-]\d{3}$"); //check according to the date?
            if (StartDateCalendar.SelectedDate.Value.Date.Year < 2018)
            {
                if (regex.IsMatch((LicenseNumTextBox.Text)))
                {
                    string title = "Gilore Travels ERROR: License number";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBox.Show("Wrong input format.", title, button, icon);
                    this.Close();
                }
            }
            else if (regex1.IsMatch((LicenseNumTextBox.Text)) && regex1.IsMatch(LicenseNumTextBox.Text))
            {
                string title = "Gilore Travels ERROR: License number";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Wrong input format.", title, button, icon);
                this.Close();
            }
            else this.Close();
            
            licenseNum = LicenseNumTextBox.Text; 
        }

        private void DriverNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number;
            bool success = Int32.TryParse(MileageTextBox.Text, out number);
            if (!success)
            {
                driverName = DriverNameTextBox.Text;
            }
            else
            {
                string title = "Gilore Travels ERROR: Driver";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Wrong input format.", title, button, icon);
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
            }
        }

        private void MileageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number;
            bool success = Int32.TryParse(MileageTextBox.Text, out number);
            if (success)
            {
                mile = number;
            }
            else
            {
                string title = "Gilore Travels ERROR: Mileage";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Wrong input format.", title, button, icon);
                MainWindow m = new MainWindow();
                m.Show();
                this.Close(); 
            }
        }

        public DateTime startDate;
        public string licenseNum;
        public int mile;
        public int fuelLeft; 
        public string driverName;

        //check if the textbox have been provided with some values
        public bool checkText ()
        {
            if (LicenseNumTextBox.Text.Length == 0 || MileageTextBox.Text.Length == 0 || DriverNameTextBox.Text.Length == 0)
                return false;
            else return true;
        }
        public void AddBus(string l, string n, int f, int m)
        {
            if (!checkText())
            {
                string title = "Gilore Travels ERROR: Add Bus";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Not all...", title, button, icon); //ask gila for the correct syntax :)
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            else
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
            }

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






