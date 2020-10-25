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
        /*chooseBus:
         * user inputs a license number. If the bus exists and the trip is possible, then the fields are updated
         */
        void chooseBus ()
        {   
            Console.WriteLine("Enter a bus license number: ");
            string busNum = Console.ReadLine();
            foreach (string x in busStringArray) //check if the bus exists
                if (busNum.Contains(x))
                    break;
                else Console.WriteLine("ERROR: bus does not exist." );

            Random rnd = new Random();
            int length = rnd.Next(1, 5000); //assuming the length of the trip is between 1 and 4999 km
            if ((getMileage() + length > 20000) || (getFuel() - length < 0) //if there is not enough fuel or the mileage is too high
            {
                Console.WriteLine("The trip is not possible.");
                return;
            }
            setMileage(getMileage() + length); //updates the files
            setFuel(getFuel() - length);

        }
    }

    class Program
    {
        public enum BusOptions { Add = 1, Choose, Refuel, Mileage, Exit };
        static void Main(string[] args)
        {
            // create a list of buses
            var items = new List<Bus>();

            Console.WriteLine("Welcome to our Bus Fleet \nPlease select an option from the menu:");
       
            Console.WriteLine(" 1. Adding a Bus \n 2. Choosing a Bus a for Travel \n 3. Refueling/Bus Maintenance \n 4. Mileage Display of Bus Fleet \n 5. Exit");

            BusOptions b = (BusOptions) Console.Read(); //added this line so the switch could work

            switch (b) //works but nothing shows up on the console when we run it
            {
                case BusOptions.Add: 
                    Console.WriteLine("add");
                    Console.ReadKey();
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
                    Console.WriteLine("default");
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

