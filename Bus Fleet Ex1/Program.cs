using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bus_Fleet_Ex1
{

    class Bus
    {
        /* CLASS MEMBERS */

        //information of each bus - each member is private with a public method that accesses and update the private member:
        private string license; //license number

        public string BusLicense
        {
            get { return license; }
            set 
            {
                if (BusStartDate < 2018) /* date 2018*/
                {
                    /*7 didgits format --> 12-345-67 */ 
                    license = value;
                }
                else // >2018
                {
                    /*8 didgits format --> 12-345-678 */
                    license = value;
                }
            }
        }

        private DateTime startDate; //start date registered for bus (uses the struct DateTime to have correct format)

        public DateTime BusStartDate
        {
            get { return startDate; }
            set { startDate = DateTime.Now; }
        }


        private float mileage; //mileage - the total kilometerage

        public float BusMileage
        {
            get { return mileage; }
            set //increase milleage each time a certain distance is traveled... cannot subtract
            { mileage = value;}
        }

        private float fuel; //fuel value

        public float BusFuel
        {
            get { return fuel; }
            set { fuel = value; }
        }

        private float maintenance; //memeber keeps track of bus maintenace, specifically how long since last maintenance and if bus is dangerous

        public float BusMaintenance
        {
            get { return maintenance; }
            set { maintenance = value; }
        }

        /* CLASS METHODS */

        /* Menu Option A: */

        /* Method: addBus
         * Description: adds a bus to the fleet (license and start date of the activity)
         * Return Type: void
         */

        void addBus(List<Bus> Busfleet)
        {
            Console.WriteLine(" Enter the license number: ");
            license = Console.ReadLine();
            Console.WriteLine("Enter the mileage: ");  //can we say that in english??
            string mileage = Console.ReadLine();
            int number = 0; 
            int newMileage = 0;
            if (Int32.TryParse(mileage, out number))
            {
                if (number < 0 || number > 1000000)
                {
                    Console.WriteLine("ERROR: value is not possible."); //if the value is not possible, set it to be the default value
                    newMileage = 0;
                }
                else
                {
                    newMileage = number;
                }
            }
            Console.WriteLine("Enter the distance of the trip that can be made with the current quantity of fuel: ");
            string fuel = Console.ReadLine();
            int num = 0;
            int newFuel = 1200;
            if (Int32.TryParse(fuel, out num))
            {
                if (num < 0 || num > 1200)
                {
                    Console.WriteLine("ERROR: value is not possible."); //if the value is not possible, set it to be the default value
                    newFuel = 1200;
                }
                else
                {
                    newFuel = num;
                }
            }

            Console.WriteLine(" Enter the start date of the activity (DD/MM/YYYY): ");
            string date = Console.ReadLine();
            DateTime datevalue;
            if (DateTime.TryParse(date, out datevalue))
            {
               Busfleet.Add(new Bus() { BusLicense = license, BusStartDate = datevalue, BusMileage = newMileage, BusFuel = newFuel }); //create a new Bus and add it to the list
            }
        }



        /* Menu Option B: */

        /* Method: chooseBus
         * Description: user inputs a license number. If the bus exists and the trip is possible, then the fields are updated
         * Return Type: void 
         */
        void chooseBus(List<Bus> Busfleet) //--> note: need to change method calls to new names to keep data encapsulation
        {
            bool exists = false;
            Console.WriteLine("Enter a bus license number: ");
            string busNum = Console.ReadLine();
            for (int i = 0; i < Busfleet.Count(); i++)
            {
                if (Busfleet[i].BusLicense.Equals(busNum))
                {
                    exists = true;
                    i = (Busfleet.Count() - 1);
                }
                else   
                { 
                    exists = false;
                }
             
            }

            if (exists) 
            {
                Random rnd = new Random();
                int length = rnd.Next(1, 5000); //assuming the length of the trip is between 1 and 4999 km
                if ((BusMileage + length > 20000) || (BusFuel - length < 0)) //if there is not enough fuel or the mileage is too high 
                {
                    Console.WriteLine("The trip is not possible.");
                    return;
                }
               BusMileage = (BusMileage + length); //updates the files
               BusFuel = (BusFuel - length);

            }
            else { Console.WriteLine("ERROR: bus does not exist."); }
            
            // bus does not exists and user is sent back to the main menu
        }


        /* Menu Option C: */

        /* Method: refuelMaintenance
         * Discription: Bus License is entered by user. user selects either refueling or maintenance. 
         *  i. For refueling, bus didtance allowed is updated to allow for maximum distance traveled.
         *  ii. For maintenance, update the current date and the mileage of the vehicle, when the tune-up took place.
         * Return Type: void
         */

        void refuelMaintenance(List<Bus> Busfleet)
        {
            bool notFound = true;
            Console.WriteLine("Enter the Bus License number:");
            string Blicense = Console.ReadLine();
            for (int i = 0; i < Busfleet.Count(); i++)
            {
                if (Busfleet[i].BusLicense.Equals(Blicense)) //bus is found in fleet
                {
                    notFound = false;
                    Console.WriteLine("Type 'A' to select the refueling option or 'B'to select maintenance option.");
                    TryParse(Console.ReadLine(), out int AorB);
                    if (AorB == 65) //option A
                    {
                        BusFuel = 1200;
                        //1200KM max distance allowed to travel after refueling
                        Console.WriteLine("The bus has been refueled.");
                    }
                    if (AorB == 66) //option B
                    {

                        //need to complete this
                        //ii. For maintenance, update the current date and the mileage of the vehicle, when the tune-up took place.
                    }
                    i = (Busfleet.Count() - 1); //exit for loop
                }
            }
            if (notFound) //bus not in fleet
            {
                Console.WriteLine("ERROR: bus does not exist - refueling and maintenance options are not available.");
            }
        }

        private int TryParse(string v, out int result) // so that we can use the TryParse Method
        {
            throw new NotImplementedException();
        }

        /* Menu Option D: */

        /* Method: mileageDisplay
         * Description: displays the kilometerage and license numbers of all buses in the fleet.
         * Return Type: void
         */

        void mileageDisplay()
        {
            // seeing as only the main program has the entire fleet of buses listed, I was thinking that in the
            // method all it should do is print the mileage and license number of this bus
            // in the switch we can then call this method on the count of the busses in the fleet

            Console.WriteLine("Bus License: ");
            Console.WriteLine(BusLicense); //calls on the getter method of the BusLicense which will return the license of bus
            Console.WriteLine("Mileage: ");
            Console.WriteLine(BusMileage); //calls on the getter method of the BusMileage which will return the mileage of bus
        }

        /* Menu Option E: */

        /* Method: exit
         * Description: closes the menu console (ends the program)
         * Return Type: void
         */

        void exit()
        {
            //return 0; 
        }

    }

    class Program
    {
        public enum BusOptions { Add = 1, Choose, Refuel, Mileage, Exit };
        static void Main(string[] args)
        {
            // create a list of buses
            var fleet = new List<Bus>();

            Console.WriteLine("Welcome to our Bus Fleet \nPlease select an option from the menu:");
       
            Console.WriteLine(" 1. Adding a Bus \n 2. Choosing a Bus a for Travel \n 3. Refueling/Bus Maintenance \n 4. Mileage Display of Bus Fleet \n 5. Exit");

            BusOptions b = (BusOptions) Console.Read(); //added this line so the switch could work // that is Brilliant!!! Thank you Eleora!

            //NOTE: we have to loop through the menu and allow user to select an option more than once! Therefore need a while statement!
            // also need to add function calls
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
                case BusOptions.Mileage: // calling on the mileageDisplay method for every bus in the fleet
                    for (int i = 0; i < fleet.Count; i++)
                    {
                        //call on method  
                    }
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



//*PLAN WHAT TO DO*//

//main
/*define list of buses (class)
 * switch statement for the main (menu of options for the user)
 * print the menu so the user can select the function
 */

//bus class
/*license number
 * mileage
 * fuel information
 * start date 
 * date and time
 */

//menu:
/* 
* A.adding a bus (Eleora)
* B. choosing a bus  DONE YAY :)
* C. refuel or maintenance (Gila)
* D. display of mileage since last maintenance STILL CHECK
* E. exit
*/

/* Objectives for 26/10/2020:
 * * dateTime struct (together)
 * * insure class members are written properly to hold correct info (together)
 * fix current methods so they operate
 * divvy up rest of work
 */


