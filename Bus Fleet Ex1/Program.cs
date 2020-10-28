using System;
using System.Collections.Generic;
using System.Configuration; //this can be used to validate strings
using System.Diagnostics.Eventing.Reader;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;//this can be used to validate strings

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
            set { license = value; }
            
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
            { mileage = value; }
        }

        private float fuel; //fuel value

        public float BusFuel
        {
            get { return fuel; }
            set { fuel = value; }
        }

        private DateTime maintenanceDate; //passing the date as a string

        public DateTime lastMaintenanceDate
        {
            get { return maintenanceDate; }
            set { maintenanceDate = value; }
        }

        public void setLicenseNum()
        {
            Console.WriteLine("Please enter the manufacture date of the Bus:");
            var userDate = Console.ReadLine();
            var manufactureDate = DateTime.Now;
            DateTime.TryParse(userDate, out manufactureDate);
            string licenseTry;
            var date2018 = DateTime.MinValue;
            var date = DateTime.MinValue;
            if (DateTime.TryParse("2018/01/01", out date))  //set the date to be the beginning of 2018
            {
                date2018 = date; ;
            }

            int compare = DateTime.Compare(manufactureDate, date2018);

            if (compare <= 0)  //manufactered before 2018
            {
                Console.WriteLine("Enter the license number following the format XX-XXX-XX: ");
                licenseTry = Console.ReadLine();
                string pattern1 = @"^[0-9][0-9][-][0-9][0-9][0-9][-][0-9][0-9]$";
                bool verified1 = false;
                verified1 = (Regex.IsMatch(licenseTry, pattern1)); //first check
                while (verified1 == false)
                {
                    Console.WriteLine("ERROR: license number is in the wrong format. ");
                    licenseTry = Console.ReadLine();
                    verified1 = (Regex.IsMatch(licenseTry, pattern1)); //if license is valid verified will be true and program will continue
                }
            }
            else  
            {
                Console.WriteLine("Enter the license number following the format XXX-XX-XXX: ");
                licenseTry = Console.ReadLine();
                string pattern2 = @"^[0-9][0-9][0-9][-][0-9][0-9][-][0-9][0-9][0-9]$";
                bool verified2 = false;
                verified2 = (Regex.IsMatch(licenseTry, pattern2)); //first check
                while (verified2 == false)
                {
                    Console.WriteLine("ERROR: license number is in the wrong format. ");
                    licenseTry = Console.ReadLine();
                    verified2 = (Regex.IsMatch(licenseTry, pattern2)); //if license is valid verified will be true and program will continue
                }
            }
        }
            
        /* this method returns true if the bus needs a maintenance*/
        public bool Maintenance(Bus bus)     
        {
            var dateToday = DateTime.MinValue;
            var today = DateTime.MinValue;
            if (DateTime.TryParse (DateTime.Now.ToString("dd/mm/yyyy"), out dateToday))
            {
                today = dateToday;
            }
            TimeSpan ts = today.Subtract(lastMaintenanceDate);                   //ADD METHOD

            if ((BusMileage % 20000 == 0) && (BusMileage != 0) && (ts.Days > 365))
            {
                Console.WriteLine("DANGER: the bus needs to go to maintenance!");
                lastMaintenanceDate = today;
                return true;
            }
            return false;
        }


        

        /* CLASS METHODS */

  
        /* Menu Option A: */

        /* Method: addBus
         * Description: adds a bus to the fleet (license and start date of the activity)
         * Return Type: void
         */

        public void addBus(List<Bus> Busfleet)
        {
            setLicenseNum();
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
        }

        /* Menu Option B: */

        /* Method: chooseBus
         * Description: user inputs a license number. If the bus exists and the trip is possible, then the fields are updated
         * Return Type: void 
         */
        public void chooseBus(List<Bus> Busfleet) //--> note: need to change method calls to new names to keep data encapsulation
        {
            bool exists = false;
            Console.WriteLine("Enter a bus license number: ");
            string busNum = Console.ReadLine();
            int busCount = 0;
            for (int i = 0; i < Busfleet.Count(); i++)
            {
                if (Busfleet[i].BusLicense.Equals(busNum))
                {
                    exists = true;
                    busCount = i;
                    i = (Busfleet.Count() - 1);
                }
                else
                {
                    exists = false;
                }

            }

            if (exists)
            {
                DateTime dateCurrent = DateTime.Now;
                Random rnd = new Random();
                int length = rnd.Next(1, 5000); //assuming the length of the trip is between 1 and 4999 km
                if ((!Maintenance(Busfleet[busCount])) || (BusFuel - length < 0)) //if there is not enough fuel or the mileage is too high 
                {
                    Console.WriteLine("The trip is not possible.");
                    return;
                }
                BusMileage = (BusMileage + length); //updates the files
                BusFuel = (BusFuel - length);

            }
            else // bus does not exists and user is sent back to the main menu
            {
                Console.WriteLine("ERROR: bus does not exist.");
             }  
        }


        /* Menu Option C: */

        /* Method: refuelMaintenance
         * Description: Bus License is entered by user. user selects either refueling or maintenance. 
         *  i. For refueling, bus didtance allowed is updated to allow for maximum distance traveled.
         *  ii. For maintenance, update the current date and the mileage of the vehicle, when the tune-up took place.
         * Return Type: void
         */

        public void refuelMaintenance(List<Bus> Busfleet)
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
                        Maintenance(Busfleet[i]);
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

        public void mileageDisplay()
        {
            // seeing as only the main program has the entire fleet of buses listed, I was thinking that in the
            // method all it should do is print the mileage and license number of this bus
            // in the switch we can then call this method on the count of the busses in the fleet

            Console.WriteLine("Bus License: ");
            Console.WriteLine(BusLicense); //calls on the getter method of the BusLicense which will return the license of bus
            Console.WriteLine("Mileage: ");
            Console.WriteLine(BusMileage); //calls on the getter method of the BusMileage which will return the mileage of bus
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

           
            int choice;
            do
            {
                int.TryParse(Console.ReadLine(), out choice);
                switch ((BusOptions)choice)
                {
                    case BusOptions.Add:
                        Console.WriteLine("Add"); //comment out
                        Bus addedBus = new Bus();
                        fleet.Add(addedBus);
                        addedBus.addBus(fleet);
                        break;
                    case BusOptions.Choose: // void chooseBus(List<Bus> Busfleet) 
                        Console.WriteLine("Choose"); //comment out
                        fleet[0].chooseBus(fleet);
                        break;
                    case BusOptions.Refuel: //  void refuelMaintenance(List<Bus> Busfleet)
                        Console.WriteLine("Refuel"); //comment out
                        fleet[0].refuelMaintenance(fleet);
                        break;
                    case BusOptions.Mileage: // void mileageDisplay()
                        Console.WriteLine("Mileage"); //comment out
                        for (int i = 0; i < fleet.Count; i++)
                        {
                            fleet[i].mileageDisplay();
                        }
                        break;
                    case BusOptions.Exit:
                        Console.WriteLine("close"); //comment out
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Error: Invalid Input");
                        break;
                }
                Console.WriteLine("Select another option from the menu:\n");
            }
            while (choice != 0);
        }
    }
}




