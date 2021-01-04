using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALApi;
using DO;

namespace PLConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IDAL dal = DLFactory.GetDL();
            Console.WriteLine("Helloww");
            Bus b = new Bus();
            dal.AddBus(b);
            Console.WriteLine("Hopefully this will show up");
        }
    }
}
