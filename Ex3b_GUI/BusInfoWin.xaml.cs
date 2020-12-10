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
    public partial class BusInfoWin : Window
    {
        private Bus _theBus;
        public BusInfoWin(Bus bus)
        {
            _theBus = bus;
            InitializeComponent();
            tb_BusInfo.Text = _theBus.ToString(); //print out the bus information
        }

    }
}
