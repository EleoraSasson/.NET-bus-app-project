using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DALApi;

namespace DS
{
    public static class DataSource
    {
        //public static List<>; 
        static DataSource() //static ctor
        {
            busList = new List<Bus>(); 
            busList.Add(new Bus());

            // member  = new listname..
            //class in DO (?????)
        }
        //list of busses and list of lines etc
        //all the basic non so simple types
    }
}
