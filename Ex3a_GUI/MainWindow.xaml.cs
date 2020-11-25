/* Mini Project in Windows System - Eleora & Gila
 This WPF Program displays the BuslineCollection Information. The user is able to select a bus line from the drop-down menu.
This Results in the Bus Station Information and Area Information to be displayed.*/

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ex3a_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // defining our lineCollection:
        BusDatabase lineCollection = new BusDatabase();
        private BusLine currentDisplayBusLine;
        public MainWindow()
        {
            //component initialisation:
            InitializeComponent();

            //filling LineCollection:
            RandomiseListCollection();
            //adding the selectionChanged event for the cbBusLines:
            cbBusLines.ItemsSource = lineCollection;
            cbBusLines.DisplayMemberPath = "BusLineNum";
            cbBusLines.SelectedIndex = 0;
            this.cbBusLines.SelectionChanged += cbBusLines_SelectionChanged;

        }

        private void RandomiseListCollection()
        {

            //filling our busCollection
            for (int i = 0; i < 10; i++) //randomly creates 10 lines
            {
                lineCollection.randAddLine();
                
                /*pause program for a small amount of time so that subsequent random numbers 
                 * that are generated will be different as use a new seed*/
                System.Threading.Thread.Sleep(50); 


                for (int k = 0; k < 10; k++) //randomly fills the 10 lines with 10 stations
                {
                    lineCollection.routes[i].randAddStop();

                    /*pause program for a small amount of time so that subsequent random numbers 
                * that are generated will be different as use a new seed*/

                    System.Threading.Thread.Sleep(50);
                }
            }
        }


        //combo box of BusLines - Selection Changed event:
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusLineNum);
        }

        private void ShowBusLine(int bl)
        {
            var index = lineCollection.BusIndexer(bl);
            if (index >= 0)
            {
                currentDisplayBusLine = lineCollection.routes[index];
                UpGrid.DataContext = currentDisplayBusLine;
                lbBusLineStations.DataContext = currentDisplayBusLine.stations;
            }
            else throw new KeyNotFoundException();
        }

        private void tbBusLines_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbAreaText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void TbArea_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lbBusLineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }

}
