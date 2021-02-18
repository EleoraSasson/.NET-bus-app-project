using BLApi;
using BO;
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

namespace UI
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public IBL bl = BLFactory.GetBL();
        UserPortal user = new UserPortal();

        private string first;
        private string last;
        private string id;
        private string username;
        private string password;

        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void firstTextChanged(object sender, TextChangedEventArgs e)
        {
            first = firsttbx.Text;
        }

        private void lastTextChanged(object sender, TextChangedEventArgs e)
        {
            last = lasttbx.Text;
        }

        private void usernameTextChanged(object sender, TextChangedEventArgs e)
        {
            username = usernametbx.Text;
        }

        private void passwordTextChanged(object sender, TextChangedEventArgs e)
        {
            password = passwordtbx.Text;
        }

        private void idTextChanged(object sender, TextChangedEventArgs e)
        {
            id = idtbx.Text;
        }

        public bool checkText()
        {
            if (firsttbx.Text.Length == 0 || lasttbx.Text.Length == 0 || usernametbx.Text.Length==0 || passwordtbx.Text.Length == 0 || idtbx.Text.Length==0)
                return false;
            else return true;
        }

        private void B_AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser(user);
            bl.AddUser(first, last, username, password, id);
            this.Close(); //close the window when bus is added
        }

        public void AddUser(UserPortal up)
        {
            if (!checkText())
            {
                string title = "Gilore Travels ERROR: Add User";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Error: Fields have been left blank.", title, button, icon);
                this.Close();
            }
            else
            {
                bl.SetUser(up, first, last, username, password, id);
                AdminManagerWindow.usersCollection.Add(up);
            }
        }
    }
}
