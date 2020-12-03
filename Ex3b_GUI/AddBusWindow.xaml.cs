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

            lv_BusList.ItemsSource = _BusList;
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
            var collection = new ObservableCollection<FooBar>();
            collection.Add(fooBar1);

            _listBox.ItemsSource = collection;

        }
    }
}
