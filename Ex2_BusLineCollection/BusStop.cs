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
namespace Ex2_BusLineCollection
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
            set { location = value; }
        }

        public void setLocation(BusStop stop)
        {
            Random rlat = new Random();
            stop.BusLocation.Latitude = rlat.NextDouble() * (33.30 - 31.30) + 31.30; //returns random variable between 31.30 and 33.30 and sets it as latitude
            Random rlong = new Random();
            stop.BusLocation.Longitude = rlong.NextDouble() * (35.50 - 34.30) + 34.30; //returns random variable between 34.3 and 35.5  
                                                                                       //stop.BusLocation.Speed = 6;
        }

        private string address;//physical address

        public string BusAddress
        {
            get { return address; }
            set { address = value; }
        }

        /*CLASS CTORS*/

        public BusStop() //defualt ctor 
        {
            BusStationKey = 000000;
            BusLocation = new GeoCoordinate(0, 0);
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

        //sets key
        public void setKey(List<BusRouteInfo> bus, BusStop stop)
        {
            bool enterKey = false;
            int key = 0;
            while (enterKey == false)
            {
                Console.WriteLine("Enter the bus station ID (must be 6 digits): ");
                key = Convert.ToInt32(Console.ReadLine());
                while (key < 99999|| key > 1000000) //check if key is valid
                {
                    Console.WriteLine("Error: Invalid Key - Must be 6 digits long\n");
                    key = Convert.ToInt32(Console.ReadLine());
                }

                foreach (var Bstop in bus)
                {
                    if (Bstop.BusStationKey == key)
                    {
                        Console.WriteLine("ERROR: bus station already exists.");
                    }
                    else { enterKey = true; }
                }
            }
            stop.BusStationKey = key;
        }
        //sets address
        public void setAddress(List<BusRouteInfo> bus, BusStop stop)
        {
            Console.WriteLine("Enter Physical Address:");
            var address = Console.ReadLine();
            stop.BusAddress = address;
        }
        //tostring override
        public override string ToString()
        {
            return ("Bus Station Code: " + BusStationKey + ", " + BusLocation.Latitude + " N " + BusLocation.Longitude + " E, " + BusAddress);
        }
    }
}
