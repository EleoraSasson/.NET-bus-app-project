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
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void t_Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void t_Admin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void b_LoginUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_LoginAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminManagerWindow adminWin = new AdminManagerWindow();
            adminWin.Show();
            this.Close();
        }
    }
}
