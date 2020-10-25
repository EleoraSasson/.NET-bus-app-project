using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Fleet_Ex1
{
  
    class Bus
    {
        //information of each bus:
        string licenseNum; 
        // start date
        float mileage;
        float fuel;

        //methods:
        
    }

    class Program
    {
        enum BusOptions { Add = 1, Choose, Refuel, Mileage, Exit };
        static void Main(string[] args)
        {
            // create a list of buses
            var items = new List<Bus>();

            Console.WriteLine("Welcome to our Bus Fleet \nPlease select an option from the menu:");
       
            Console.WriteLine(" A. Adding a Bus \n B. Choosing a Bus a for Travel \n C. Refueling/Bus Maintenance \n D. Mileage Display of Bus Fleet \n E. Exit");
            
            var userChoice = Console.ReadLine();

            switch (BusOptions)
            {
                case BusOptions.Add: 
                    Console.WriteLine("add");
                    break;
                case BusOptions.Choose:
                    Console.WriteLine("choose");
                    break;
                case BusOptions.Refuel:
                    Console.WriteLine("refuel");
                    break;
                case BusOptions.Mileage:
                    Console.WriteLine("mileage");
                    break;
                case BusOptions.Exit:
                    Console.WriteLine("exit");
                    break;
                default:
                    break;
            }

        }
    }
}


//main
/*define list of buses (class)
 * switch statement for the main (menu of options for the user)
 * print the menu so the user can select the function
 */

//bus class
/*license number
 * mileage
 * fuel information
 */

//menu:
/* A.adding a bus
* B. choosing a bus (Eleora) 
* C. refuel or maintenance 
* D. display of mileage since last maintenance (Gila)
* E. exit
*/

/* 
 WHAT TO DO:
- switch statement
- class bus 
        - date and time struct TOGETHER 
        - license ... follows format in EX
        - milleage ... 
        - fuel and maintenance

 
 */