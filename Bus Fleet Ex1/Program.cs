﻿using System;
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
                if (BusStartDate < /* date 2018*/)
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
            set { startDate = value; }
        }


        private float mileage; //mileage

        public float BusMileage
        {
            get { return mileage; }
            set //increase milleage each time a certain distance is traveled... cannot subtract
            { mileage = 0; }
        }

        private float fuel; //fuel value

        public float BusFuel
        {
            get { return fuel; }
            set { fuel = value; }
        }

        public struct DateTime { } //date&time

        /* CLASS METHODS */

        /* Menu Option A: */

        /* Method: 
         * Discription:
         * Return Type: 
         */

        /* Menu Option B: */

        /* Method: chooseBus
         * Discription: user inputs a license number. If the bus exists and the trip is possible, then the fields are updated
         * Return Type: void 
         */
        void chooseBus(List<Bus> Busfleet) //--> note: need to change ethod calls to new names to keep data encapsulation
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

        /* Method: 
         * Discription:
         * Return Type: 
         */

        /* Menu Option D: */

        /* Method: mileageDisplay
         * Discription: displays the kilometerage and license numbers of all buses in the fleet.
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
         * Discription: closes the menu cosole (ends the program)
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
* A.adding a bus
* B. choosing a bus  
* C. refuel or maintenance 
* D. display of mileage since last maintenance 
* E. exit
*/

/* Objectives for 26/10/2020:
 * * dateTime struct (together)
 * * insure class members are written properly to hold correct info (together)
 * fix current methods so they operate
 * divvy up rest of work
 */


