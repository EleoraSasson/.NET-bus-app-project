using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;//this can be used to validate strings
using System.Xml;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Runtime.Remoting;
using System.Data;
using System.Device.Location; //is used for location coordinates
using System.Collections;
using System.CodeDom;


namespace Ex3a_GUI
{
    
    class BusStop //object made: Bus Station/Stop
    {
        /*CLASS MEMBERS*/
        private int stationKey; // bus key (to identify bus stop)

        public int BusStationKey
        {
            get { return stationKey; }
            set { stationKey = value; }
        }

        private GeoCoordinate location;  //location information of stops

        public GeoCoordinate BusLocation
        {
            get { return location; }
            set
            {
                location = value;
            }

        }

        private string address;//physical address

        public string BusAddress
        {
            get { return address; }
            set { address = value; }
        }

        /*CLASS CTORS*/

        public BusStop(int key)
        {
            BusStationKey = key; //as setKey was called before
            BusLocation = setLocation();
            BusAddress = setAddress();
        }

        //sets Locatation
        private GeoCoordinate setLocation()
        {
            Random lat = new Random();
            Random lon = new Random();
            var randLat = lat.NextDouble() * (33.30 - 31.30) + 31.30;
            var randLong = lon.NextDouble() * (35.50 - 34.30) + 34.30;
            var loc = new GeoCoordinate();
            BusLocation = loc;
            BusLocation.Latitude = randLat;
            BusLocation.Longitude = randLong;
            return loc;
        }

        public BusStop() //random ctor 
        {
            Random key = new Random();
            Random lat = new Random();
            Random lon = new Random();

            var randKey = key.Next(99999, 1000000);// 6 digit key
            var randLat = lat.NextDouble() * (33.30 - 31.30) + 31.30;
            var randLong = lon.NextDouble() * (35.50 - 34.30) + 34.30;

            var loc = new GeoCoordinate();
            BusLocation = loc;
            BusStationKey = randKey;
            BusLocation.Latitude = randLat;
            BusLocation.Longitude = randLong;
            BusAddress = "No Address Assigned";
        }

        public BusStop(int key, double lat, double lon, string adr) //ctor
        {
            BusStationKey = key;
            BusLocation.Latitude = lat;
            BusLocation.Longitude = lon;
            BusAddress = adr;
        }

        /*CLASS METHODS*/

        //tostring override
        public override string ToString()
        {
            return ("Bus Station Code: " + BusStationKey + ", " + BusLocation.Latitude + " N " + BusLocation.Longitude + " E, " + BusAddress);
        }

        //sets key
        public int setKey(List<BusRouteInfo> busStations)
        {
            {
                Console.WriteLine("Enter the bus station ID (must be 6 digits): ");
                int key = Convert.ToInt32(Console.ReadLine());
                while (keyExists(key, busStations) || keyLength(key, busStations))
                {
                    key = Convert.ToInt32(Console.ReadLine());
                }
                return key;
            }
        }
        //for the setKey method - checks to see if the BusStationKey is the correct length
        private static bool keyExists(int key, List<BusRouteInfo> busStations)
        {
            foreach (BusRouteInfo busS in busStations)
            {
                if (busS.BusStationKey == key)
                {
                    Console.WriteLine("Error: bus station already exists in this line, please re-enter bus key:");
                    return true;
                }
            }
            return false;
        }
        //for the setKey method - check to see if the bus station already is listed on this route
        private static bool keyLength(int key, List<BusRouteInfo> busStations)
        {
            if (key < 99999 || key > 1000000) //check if key is valid
            {
                Console.WriteLine("Error: Invalid Key - Must be 6 digits long\n");
                return true;
            }

            return false;
        }

        //sets address
        public string setAddress()
        {
            Console.WriteLine("Enter Physical Address:");
            var address = Console.ReadLine();
            return address;
        }


    }
    
}
