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

namespace Ex3b_GUI
{
    public enum StaffName { John, Sam, Sara, Dave, Libby }
    public enum StaffSurname { Owens, Riley, Snow, Perry, Ross }
    public enum Status {Available, Traveling , Refueling, AtService}
    public class Bus
    {
        /* CLASS MEMBERS */
        //information of each bus - each member is private with a public method that accesses and update the private member:
        private string license; //license number
        public string BusLicense
        {
            get { return license; }
            set { license = value; }

        }

        private string driver; // bus driver's name
        public string BusDriver
        {
            get { return driver; }
            set { driver = value; }
        }


        private Status state;

        public Status BusState
        {
            get { return state; }
            set { state = value; }
        }
        //returns true if the bus is ready
        public bool available()
        {
            if (this.BusState == 0)
                return true;
            else return false;
        }

        private DateTime startDate; //start date registered for bus (uses the struct DateTime to have correct format)

        public DateTime BusStartDate
        {
            get { return startDate; }
            set { startDate = DateTime.Now; }
        }

        private int mileage; //mileage - the total kilometerage

        public int BusMileage
        {
            get { return mileage; }
            set //increase milleage each time a certain distance is traveled... cannot subtract
            {
                if (value < 0) //stop mileage reader from being set backwards
                {
                    Console.WriteLine("Error: mileage cannot be decreased or negative");
                    mileage = 0;
                }
                else mileage = value;
            }
        }

        private int fuel; //fuel value

        public int BusFuel
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

        public string setLicenseNum()
        {
            Console.WriteLine("Please enter the manufacture date of the Bus in the format dd/mm/yyyy:");
            var userDate = Console.ReadLine();
            string patternDate = @"^(3[01]|[12][0-9]|0[1-9])[/](1[0-2]|0[1-9])[/]\d{4}$";
            bool dateVerified = false;
            dateVerified = (Regex.IsMatch(userDate, patternDate));
            while (dateVerified == false) //check if date is valid
            {
                Console.WriteLine("Error: Invalid Date - Must be in format dd/mm/yyyy.\n");
                userDate = Console.ReadLine();
                dateVerified = (Regex.IsMatch(userDate, patternDate));
            }
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
                string pattern1 = @"^\d{2}[-]\d{3}[-]\d{2}$";
                bool verified1 = false;
                verified1 = (Regex.IsMatch(licenseTry, pattern1)); //first check
                while (verified1 == false)
                {
                    Console.WriteLine("ERROR: license number is in the wrong format. ");
                    licenseTry = Console.ReadLine();
                    verified1 = (Regex.IsMatch(licenseTry, pattern1)); //if license is valid verified will be true and program will continue
                }
                return licenseTry;
            }
            else
            {
                Console.WriteLine("Enter the license number following the format XXX-XX-XXX: ");
                licenseTry = Console.ReadLine();
                string pattern2 = @"^\d{3}[-]\d{2}[-]\d{3}$";
                bool verified2 = false;
                verified2 = (Regex.IsMatch(licenseTry, pattern2)); //first check
                while (verified2 == false)
                {
                    Console.WriteLine("ERROR: license number is in the wrong format. ");
                    licenseTry = Console.ReadLine();
                    verified2 = (Regex.IsMatch(licenseTry, pattern2)); //if license is valid verified will be true and program will continue
                }
                return licenseTry;
            }
        }

        //returns random starting date
        public void randDate()
        {
            Random y = new Random();
            int year = y.Next(1999, 2020); //random year
            Random m = new Random();
            int month = m.Next(1, 13); //random month
            Random d = new Random();
            int day = d.Next(1, 32);
            var NewDate = new DateTime(year, month, day);
            this.BusStartDate = NewDate;
        }

        //returns random license number according to the manufacture year of the bus
        public string randLicense()
        {
            Random rand = new Random();
            if (this.BusStartDate.Year < 2018) // license will have the format XX-XXX-XX
            {
                int l = rand.Next(10000000, 99999999);
                string Blicense = Convert.ToString(l);
                string license = Regex.Replace(Blicense, @"^(..)(...)(..)$", "$1-$2-$3");
                return license;
            }
            else // license will have the format XXX-XX-XXX
            {
                int l = rand.Next(10000000, 99999999);
                string Blicense = Convert.ToString(l);
                string license = Regex.Replace(Blicense, @"^(...)(..)(...)$", "$1-$2-$3");
                return license;
            } 
        }
        //returns random driver name
        public string randDriver()
        {
            var rnd = new Random();
            StaffName sn = (StaffName)rnd.Next(Enum.GetNames(typeof(StaffName)).Length);
            var rd = new Random();
            StaffSurname ssn = (StaffSurname)rd.Next(Enum.GetNames(typeof(StaffSurname)).Length);
            string driver = sn.ToString() + " " +  ssn.ToString();
            return driver;
        }

        //returns random mileage
        public int randMileage()
        {
            Random mile = new Random();
            int mi = mile.Next(0, 100000);
            return mi;
        }
        //returns random fuel
        public int randFuel()
        {
            Random fu = new Random();
            int fue = fu.Next(0, 1200);
            return fue;
        }
        //returns random status
        public Status randStatus()
        {
            var rnd = new Random();
            return (Status)rnd.Next(Enum.GetNames(typeof(Status)).Length);
        }

        /* this method returns true if the bus needs a maintenance */
        public bool Maintenance(Bus bus)
        {
            var today = DateTime.Now;
            int days = (today - lastMaintenanceDate).Days;

            if (((BusMileage % 20000) == 0) || (BusMileage != 0) || (days > 365))
            {
                Console.WriteLine("DANGER: the bus needs to go to maintenance!");
                lastMaintenanceDate = today;
                return true;
            }
            return false;
        }


        public Bus () //default constructor
        {
            BusStartDate = DateTime.Now;
            BusMileage = 0;
            BusLicense = "00-000-00";
            BusFuel = 1200;
            BusState = 0;
            BusDriver = "Unknown";

        }


        /*ToString Override*/

        public override string ToString()
        {
            return (" Bus Information:" + "\n Bus License Number: " + BusLicense + "\n Manufacture Date: "+ BusStartDate+ "\n Mileage: "+ BusMileage + "\n Fuel Amount: " + BusFuel + "\n Current Status: " + BusState + "\n Assigned Driver: " + BusDriver);
        }
        /* CLASS METHODS */


        /* Menu Option A: */

        /* Method: addBus
         * Description: adds a bus to the fleet (license and start date of the activity)
         * Return Type: void
         */

        public void addBus(List<Bus> Busfleet)
        {
            string license = setLicenseNum();
            BusLicense = license; //update bus to have correct license as opposed to default 
            Console.WriteLine("Enter the mileage: ");
            string mileage = Console.ReadLine();
            int number = 0;
            int newMileage = 0;
            if (Int32.TryParse(mileage, out number))
            {
                if (number < 0 || number > 1000000)
                {
                    Console.WriteLine("ERROR: value is not possible. Mileage has been set to 0."); //if the value is not possible, set it to be the default value
                    newMileage = 0;
                }
                else
                {
                    newMileage = number;
                }
                BusMileage = newMileage; //update bus to have correct mileage as opposed to default 
            }

            BusMileage = newMileage;
            Console.WriteLine("Enter the distance of the trip that can be made with the current quantity of fuel: ");
            string fuel = Console.ReadLine();
            int num = 0;
            int newFuel = 1200;
            if (Int32.TryParse(fuel, out num))
            {
                if (num < 0 || num > 1200)
                {
                    Console.WriteLine("ERROR: value is not possible. Distance has been set to 1200."); //if the value is not possible, set it to be the default value
                    newFuel = 1200;
                }
                else
                {
                    newFuel = num;
                }
                BusFuel = newFuel;//update bus to have correct fuel as opposed to default 
            }
            Console.WriteLine("Enter the date of the last maintenance check (dd/mm/yyyy): ");
            string date = Console.ReadLine();
            string patternDate = @"^(3[01]|[12][0-9]|0[1-9])[/](1[0-2]|0[1-9])[/]\d{4}$";
            bool dateVerified = false;
            dateVerified = (Regex.IsMatch(date, patternDate));
            while (dateVerified == false) //check if date is valid
            {
                Console.WriteLine("Error: Invalid Date - Must be in format dd/mm/yyyy.\n");
                date = Console.ReadLine();
                dateVerified = (Regex.IsMatch(date, patternDate));
            }
            DateTime dateValue;
            if (DateTime.TryParse(date, out dateValue))
            {
                lastMaintenanceDate = dateValue; //update bus to have correct maintenance as opposed to default 
                BusStartDate = dateValue; //update bus to have correct startdate as opposed to default 

            }
            Console.WriteLine("Bus successfully added to fleet.");
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
                int length = rnd.Next(1, 1201); //assuming the length of the trip is between 1 and 1200 km
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
                    Console.WriteLine("Type '1' to select the refueling option or '2' to select maintenance option.");

                    if (Int32.TryParse(Console.ReadLine(), out int number))
                    {
                        if (number == 1)
                        {
                            BusFuel = 1200;
                            Console.WriteLine("The bus has been refueled.");
                        }
                        if (number == 2)
                        {
                            Maintenance(this);
                            Console.WriteLine("The bus has undergone a maintenance check.");
                        }
                        i = (Busfleet.Count() - 1); //exit for loop
                    }
                    else
                    {
                        Console.WriteLine("Error: invalid input");
                        i = (Busfleet.Count() - 1); //exit for loop
                    }
                }
            }
            if (notFound) //bus not in fleet
            {
                Console.WriteLine("ERROR: bus does not exist - refueling and maintenance options are not available.");
            }
        }


        /* Menu Option D: */

        /* Method: mileageDisplay
         * Description: displays the kilometerage and license numbers of all buses in the fleet.
         * Return Type: void
         */

        public void mileageDisplay()
        {
            Console.WriteLine("Bus License: ");
            Console.WriteLine(BusLicense);
            Console.WriteLine("Mileage: ");
            Console.WriteLine(BusMileage);
        }

        /*ADDITIONAL METHODS FOR WPF PROGRAM*/

        //Refuels a bus by setting the tank to "full" can travel 1200km
        public void Refuel()
        {
            this.BusState = Status.Refueling;
            this.BusFuel = 1200;
        }

        //Send bus for Maintenance by resetting the dat and refueling the bus
        public void Maintenance()
        {
            this.BusState = Status.AtService;
            this.BusFuel = 1200; //refuels
            DateTime current = DateTime.Today;
            this.BusStartDate = current; //gives new date        
        }

        public bool IsAvailable()
        {
            if (this.BusState == Status.Available)
            {
                return true;
            }
            else return false;
        }
    }
}