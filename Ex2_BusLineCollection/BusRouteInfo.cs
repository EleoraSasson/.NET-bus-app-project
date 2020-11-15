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
    class BusRouteInfo : BusStop //object made: a bus stop with distance and time properties added
    {
        /*CLASS MEMBERS*/

        //distance from prev stop:
        private float distance; 

        public float BusDistance
        {
            get { return distance; }
            set { distance = value; }
        }

        //time since prev stop:
        private TimeSpan time; 

        public TimeSpan BusTime
        {
            get { return time; }
            set { time = value; }
        }

        /*CLASS CTOR*/

        public BusRouteInfo() : base() //default ctor
        {
            Random dist = new Random();
            Random time = new Random();

            var randDist = dist.Next(1, 800); //1 to 800 KM
            var RTime = time.Next(10, 120); //in minutes
            var randTime = TimeSpan.FromMinutes(RTime); //changing to TimeSpan type

            BusDistance = randDist;
            BusTime = randTime;
        }

        public BusRouteInfo(float dist, TimeSpan t, int key, double lat, double lon, string adr) : base(key, lat, lon, adr) // ctor
        {
            BusDistance = dist;
            BusTime = t;
        }
    }
}
