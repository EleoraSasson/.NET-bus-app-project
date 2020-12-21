using System;
using System.Collections.Generic;
using System.Device.Location; //added to be able to use a GeoCo-ordinate
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusStop : ICloneable
    {
        /* PROPERTIES */

        // Station Code:
        private int code;

        public int StopCode
        {
            get { return code; }
            set { code = value; }
        }

        // Position (Location) - Longitude & Latitude:
        private GeoCoordinate location;  //location information of stops

        public GeoCoordinate StopLocation
        {
            get { return location; }
            set
            {
                location = value;
                location.Latitude = 0;
                location.Longitude = 0;
            }

        }

        // Station Name:
        //look how to define - baed on address
        private string name;

        public string StopName
        {
            get { return name; }
            set { name = value; }
        }

        // Address:
        // use geocordinate and convert to a TosString physical address?? This is optional

        //additional Information
        // Active/Not_Active [tells us if the stop is available for buses to go to (active and in use) or not (undergoing repairs or no longer in use)]
        private bool active;

        public bool StopActive
        {
            get { return active; }
            set { active = value; }
        }

        // Digital Panel [Display what lines are coming and when]
        // not sure how we want to se this up?


        /* CONSTRUCTORS */
        // Default Ctor:
        public BusStop()
        {
            StopCode = 0;
            StopName = "NewStation";
            StopActive = false;
            //address
            //digital panel
        }
        // Ctor:
        public BusStop( int codeS, GeoCoordinate locationS, string nameS, bool activeS /*address, digital panel*/)
        {
            StopCode = codeS;
            StopLocation = locationS;
            StopName = nameS;
            StopActive = activeS;
            //address
            //digital panel
        }

        /* OVERRIDE TOSTRING */
        public override string ToString()
        {
            return "Bus Stop Details: \n Station Name:" + StopName +  "\n Station Code:" + StopCode + "\n Location:" + StopLocation.ToString() + "\n Physical Address:" + /*address +*/ "\n Active:" + StopActive; 
        }
    }
}
