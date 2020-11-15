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
            private set { location = value; }
        }

        private string address;//physical address

        public string BusAddress
        {
            get { return address; }
            set { address = value; }
        }

        /*CLASS CTORS*/

        public BusStop() //default ctor 
        {
            Random key = new Random();
            Random lat = new Random();
            Random lon = new Random();

            var randKey = key.Next(99999,100000);// 6 digit key
            var randLat = lat.NextDouble() * (33.30 - 31.30) + 31.30; 
            var randLong = lon.NextDouble() * (35.50 - 34.30) + 34.30;

            BusStationKey = randKey;
            BusLocation.Latitude = randLat;
            BusLocation.Longitude = randLong;
            BusAddress = "No Address Assigned";
        }

        public BusStop(int key, double lat, double lon, string adr) //ctor
        { 
            BusStationKey = key;
            BusLocation.Latitude = lat;
            Console.WriteLine(lat);
            BusLocation.Longitude = lon;
            BusAddress = adr;
        }

        /*CLASS METHODS*/

        //tostring override
        public override string ToString()
        {
            return ("Bus Station Code: " + BusStationKey + ", " + BusLocation.Latitude + " N " + BusLocation.Longitude + " E, " + BusAddress);
        }
    }
}
