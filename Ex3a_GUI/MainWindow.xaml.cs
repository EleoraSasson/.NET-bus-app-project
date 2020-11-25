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
                for (int k = 0; k < 10; k++) //randomly fills the 10 lines with 10 stations
                {
                    lineCollection.routes[i].randAddStop();
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
        //private void ShowBusLine(int index)
        //{
        //    currentDisplayBusLine = lineCollection.routes[index];
        //    UpGrid.DataContext = currentDisplayBusLine;

        //    lbBusLineStations.DataContext = currentDisplayBusLine.stations;
        //}


        //method which displays the information connected to the selected BusLineNum
        //private void ShowBusLine(int busLineNum) 
        //{
        //    //search for busLine:
        //    //var FindLine = lineCollection.routes.FindIndex(bus => bus.BusLineNum == busLineNum);
        //    bool found = false;
        //    foreach (BusLine line in lineCollection)
        //    {
        //        if (line.BusLineNum == busLineNum)
        //        {
        //            found = true;
        //            currentDisplayBusLine = line; //saves current line
        //            break;
        //        }
        //    }
        //    if (found)
        //    {
        //        UpGrid.DataContext = currentDisplayBusLine;
        //        lbBusLineStations.DataContext = currentDisplayBusLine.stations;
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Error: BusLine not in lineCollection.");
        //    }
        //    //if ((FindLine == (-1))) //index not found
        //    //{
        //    //    throw new ArgumentException("Error: BusLine not in lineCollection.");
        //    //}
        //    //else
        //    //{
        //    //    currentDisplayBusLine = lineCollection.routes[FindLine];
        //    UpGrid.DataContext = currentDisplayBusLine;
        //    lbBusLineStations.DataContext = currentDisplayBusLine.stations;
        //}
        //}

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
