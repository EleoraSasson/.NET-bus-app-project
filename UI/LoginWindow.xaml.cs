
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

namespace UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public IBL bl = BLFactory.GetBL(); //create bl instance 
        public string userName;
        public string password;
        public string adminName;
        public string adminPass;

        public LoginWindow()
        {
            InitializeComponent();
        }

        #region User Login
        private void t_Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            userName = t_Username.Text;
        }

        private void t_PasswordUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            password = t_PasswordUser.Password;
        }

        private void b_LoginUser_Click(object sender, RoutedEventArgs e)
        {
            if (checkUser())
            {
                //if (bl.UserSearch(t_Username.Text, t_PasswordUser.Password))
                //{
                //    UserWindow userWin = new UserWindow();
                //    userWin.Show();
                //    this.Close();
                //}
                //else
                //{
                //    string title = "Gilore Travels ERROR: Login Window";
                //    MessageBoxButton button = MessageBoxButton.OK;
                //    MessageBoxImage icon = MessageBoxImage.Error;
                //    MessageBox.Show("Error: wrong username or password.", title, button, icon);
                //    this.Close();
                //}

                try
                {
                    BO.UserPortal loginUser = bl.GetUser(t_Username.Text, t_PasswordUser.Password);
                    UserWindow userWin = new UserWindow();
                    userWin.Show();
                    this.Close();
                }
                catch
                {
                    string title = "Gilore Travels ERROR: Login Window";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBox.Show("Error: wrong username or password.", title, button, icon);
                    t_Username.Text = "";
                    t_PasswordUser.Password = "";
                }
            }
            else
            {
                string title = "Gilore Travels ERROR: Login Window";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Error: Fields have been left blank.", title, button, icon);
                t_Username.Text = "";
                t_PasswordUser.Password = "";
            }
        }

        public bool checkUser()
        {
            if (t_PasswordUser.Password.Length == 0 || t_Username.Text.Length == 0)
                return false;
            else return true;
        }

        #endregion



        private void t_Admin_TextChanged(object sender, TextChangedEventArgs e)
        {
            adminName = t_Username.Text;
        }

        private void t_PasswordAdmin_TextChanged(object sender, TextChangedEventArgs e)
        {
            adminPass = t_PasswordUser.Password;
        }

        public bool checkAdmin()
        {
            if (t_PasswordAdmin.Password.Length == 0 || t_Admin.Text.Length == 0)
                return false;
            else return true;
        }

        private void b_LoginAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (checkAdmin())
            {
                //if (bl.GetAdmin(t_Admin.Text, t_PasswordAdmin.Password) != null)
                //{

                //    }
                //    else
                //    {
                //        string title = "Gilore Travels ERROR: Login Window";
                //        MessageBoxButton button = MessageBoxButton.OK;
                //        MessageBoxImage icon = MessageBoxImage.Error;
                //        MessageBox.Show("Error: wrong username or password.", title, button, icon);
                //        this.Close();
                //    }

                try
                {
                    BO.AdminPortal loginAdmin = bl.GetAdmin(t_Admin.Text, t_PasswordAdmin.Password);
                    AdminManagerWindow adminWin = new AdminManagerWindow(loginAdmin);
                    adminWin.Show();
                    this.Close();
                }
                catch
                {
                    string title = "Gilore Travels ERROR: Login Window";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBox.Show("Error: Incorrect username or password. Please try re-entering your information", title, button, icon);
                    t_Admin.Text = "";
                    t_PasswordAdmin.Password = "";
                }

            }
            else
            {
                string title = "Gilore Travels ERROR: Login Window";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show("Error: Fields have been left blank.", title, button, icon);
                t_Admin.Text = "";
                t_PasswordAdmin.Password = "";
            }
           
        }

        private void b_addUser_click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUser = new AddUserWindow();
            addUser.Show();
        }
    }
}