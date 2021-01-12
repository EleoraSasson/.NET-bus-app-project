using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq; //added to allow use of XElement and XML Library
using System.IO; //for using files
using DALApi;
using DO;
using System.Xml.Serialization;

namespace DLXml
{
    class DLXml : IDAL
    {
        #region singelton
        static readonly DLXml instance = new DLXml();
        static DLXml() { }// static ctor to ensure instance init is done just before first usage
        DLXml() { } // default => private 
        public static DLXml Instance { get => instance; }// The public Instance property to use
        #endregion

        #region Setting up / Loading from XML Files:

        //XML FilePaths:
        static string busPath = @"Bus.xml";

        //XML Root XElements:
        static XElement busRoot;

        /// <summary>
        /// CheckXML - checks to see if the xml exists already or not. If not it creats the xml file; else it uploads the data from the file.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="rootName"></param>
        /// <param name="filePath"></param>
        private void CheckXML(XElement root, string rootName, string filePath)
        {
            if (!File.Exists(filePath))
                CreateFile(root,rootName,filePath);
            else
                LoadFile(root, filePath);
        }

        /// <summary>
        /// CreateFile - this is a generic method which creates an xml file
        /// </summary>
        /// <param name="root"></param>
        /// <param name="rootName"></param>
        /// <param name="filePath"></param>
        private void CreateFile(XElement root, string rootName, string filePath)
        {
            root = new XElement(rootName);
            root.Save(filePath);
        }
        /// <summary>
        /// LoadFile - this is a generic method which uploads data from a given xml file
        /// </summary>
        /// <param name="root"></param>
        /// <param name="filePath"></param>
        private void LoadFile(XElement root, string filePath)
        {
            try { root = XElement.Load(filePath); }
            catch { throw new Exception($"Error: File {filePath} upload failed"); }
        }
        /// <summary>
        /// GetBusList - returns a list of busses from the XML file using XElement technique.
        /// </summary>
        /// <returns></returns>
        public static List<Bus> GetBusList()
        {
            List<Bus> busList;
            try 
            {
                busList = (from bus in busRoot.Elements()
                           select new Bus()
                           {
                               BusLicense =bus.Element("license").Value,
                               BusRegDate =DateTime.Parse(bus.Element("regDate").Value),
                               BusMaintenanceDate = DateTime.Parse(bus.Element("maintenanceDate").Value),
                               BusMileage = int.Parse(bus.Element("mileage").Value),
                               BusFuel = int.Parse(bus.Element("fuel").Value),
                               // BusStatus = Enum.Parse(,bus.Element("status").Value), // can't figure out how to convert to an enum
                               BusErased = bool.Parse(bus.Element("isErased").Value),
                           }).ToList();
            }
            catch { busList = null; }
            return busList;
        }
        /// <summary>
        /// SaveBusList - saves the list of busses to an XML file using the XElement technique.
        /// </summary>
        /// <param name="busList"></param>
        public static void SaveBusList(List<Bus> busList)
        {
            busRoot = new XElement("buses", from bus in busList
                                            select new XElement("bus", new XElement("license", bus.BusLicense),
                                            new XElement("regDate", bus.BusRegDate),
                                            new XElement("maintenanceDate", bus.BusMaintenanceDate),
                                            new XElement("mileage", bus.BusMileage),
                                            new XElement("fuel", bus.BusFuel),
                                            new XElement("status", bus.BusStatus),
                                            new XElement("isErased", bus.BusErased)));
            busRoot.Save(busPath);
        }
      
        #endregion


        //* Implemetation of all CRUD methods of DO Entities using XML files *//

        #region BUS 
        //create
        public void AddBus(Bus bus)
        {
            //not yet done
           // CheckXML(busRoot, "Busses", busPath);
           // List<Bus> fleet = GetBusList();

            XElement busRoot = XMLTools.LoadListFromXMLElement(busPath);

            throw new NotImplementedException();
        }
        //XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

        //XElement per1 = (from p in personsRootElem.Elements()
        //                 where int.Parse(p.Element("ID").Value) == person.ID
        //                 select p).FirstOrDefault();

        //    if (per1 != null)
        //        throw new DO.BadPersonIdException(person.ID, "Duplicate person ID");

        //    XElement personElem = new XElement("Person",
        //                           new XElement("ID", person.ID),
        //                           new XElement("Name", person.Name),
        //                           new XElement("Street", person.Street),
        //                           new XElement("HouseNumber", person.HouseNumber.ToString()),
        //                           new XElement("City", person.City),
        //                           new XElement("BirthDate", person.BirthDate),
        //                           new XElement("PersonalStatus", person.PersonalStatus.ToString()));

        //personsRootElem.Add(personElem);
            
        //    XMLTools.SaveListToXMLElement(personsRootElem, personsPath);
        //retrieve
        public Bus GetBus(string license)
        {
            XElement busRoot = XMLTools.LoadListFromXMLElement(busPath);

            Bus bus;
            try
            {
                bus = (from b in busRoot.Elements()
                       where b.Element("license").Value == license
                       select new Bus()
                       {
                           BusLicense = b.Element("license").Value,
                           BusRegDate = DateTime.Parse(b.Element("regDate").Value),
                           BusMaintenanceDate = DateTime.Parse(b.Element("maintenanceDate").Value),
                           BusMileage = int.Parse(b.Element("mileage").Value),
                           BusFuel = int.Parse(b.Element("fuel").Value),
                           BusStatus = (Status)Enum.Parse(typeof(Status), b.Element("status").Value),
                           BusErased = bool.Parse(b.Element("isErased").Value),

                       }).FirstOrDefault();
            }
            catch { throw new InvalidBusLicenseException(license, $"bus {license} does not exist in system."); }
            return bus;
        }
        public IEnumerable<Bus> GetAllBuses()
        {
            XElement busRoot = XMLTools.LoadListFromXMLElement(busPath);

            return (from bus in busRoot.Elements()
                    select new Bus()
                    {
                        BusLicense = bus.Element("license").Value,
                        BusRegDate = DateTime.Parse(bus.Element("regDate").Value),
                        BusMaintenanceDate = DateTime.Parse(bus.Element("maintenanceDate").Value),
                        BusMileage = int.Parse(bus.Element("mileage").Value),
                        BusFuel = int.Parse(bus.Element("fuel").Value),
                        BusStatus = (Status)Enum.Parse(typeof(Status), bus.Element("status").Value),
                        BusErased = bool.Parse(bus.Element("isErased").Value),
                    });

        }
        public IEnumerable<object> GetBusListWithSelectedFields(Predicate<Bus> predicate)
        {
            XElement busRoot = XMLTools.LoadListFromXMLElement(busPath);

            return from bus in busRoot.Elements()
                    let b = new Bus()
                    {
                        BusLicense = bus.Element("license").Value,
                        BusRegDate = DateTime.Parse(bus.Element("regDate").Value),
                        BusMaintenanceDate = DateTime.Parse(bus.Element("maintenanceDate").Value),
                        BusMileage = int.Parse(bus.Element("mileage").Value),
                        BusFuel = int.Parse(bus.Element("fuel").Value),
                        BusStatus = (Status)Enum.Parse(typeof(Status), bus.Element("status").Value),
                        BusErased = bool.Parse(bus.Element("isErased").Value),
                    }
                    where predicate(b)
                    select b;
        }
        //update
        public void UpdateBus(Bus bus)
        {
            //not yet done
            throw new NotImplementedException();
        }
        //delete
        public void DeleteBus(string license)
        {
            //not yet done
            throw new NotImplementedException();
        }

        #endregion

        #region BusLine
        public void AddBusLine(BusLine busLine)
        {
            throw new NotImplementedException();
        }
        public void DeleteBusLine(int lineID)
        {
            throw new NotImplementedException();
        }
        public BusLine GetBusLine(int lineID)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<BusLine> GetAllBusLines()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<object> GetBusLineWithSelectedFields(Func<BusLine, object> generate)
        {
            throw new NotImplementedException();
        }

        public void UpdateBusLine(BusLine busline)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region BusOnTrip
        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            throw new NotImplementedException();
        }
        public void DeleteBusOnTrip(int roadID)
        {
            throw new NotImplementedException();

        }
        public BusOnTrip GetBusOnTrip(int roadID)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<object> GetBusOnTripWithSelectedFields(Func<BusOnTrip, object> generate)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<BusOnTrip> GetAllBusesOnTrip()
        {
            throw new NotImplementedException();
        }
        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region BusStop
        public void AddBusStop(BusStop busStop)
        {
            throw new NotImplementedException();
        }
        public void DeleteBusStop(int stopCode)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<BusStop> GetAllBusStops()
        {
            throw new NotImplementedException();
        }


        public BusStop GetBusStop(int stopCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetBusStopWithSelectedFields(Func<BusOnTrip, object> generate)
        {
            throw new NotImplementedException();
        }


        public void UpdateBusStop(BusStop busStop)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region LineLeaving
        public void AddLineLeaving(LineLeaving lineLeaving)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineLeaving(int lineID, TimeSpan startTime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LineLeaving> GetAllLinesLeaving()
        {
            throw new NotImplementedException();
        }

        public LineLeaving GetLineLeaving(int lineID, TimeSpan startTime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetLineLeavingWithSelectedFields(Func<LineLeaving, object> generate)
        {
            throw new NotImplementedException();
        }


        public void UpdateLineLeaving(LineLeaving lineLeaving)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region LineStation

        public void AddLineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }
        public void DeleteLineStation(string lineStationKey)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<LineStation> GetAllLineStations()
        {
            throw new NotImplementedException();
        }

        public LineStation GetLineStation(string lineStationKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetLineStationWithSelectedFields(Func<LineStation, object> generate)
        {
            throw new NotImplementedException();
        }
        public void UpdateLineStation(string lineStationKey)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Staff
        public void AddStaff(Staff staff)
        {
            throw new NotImplementedException();
        }
        public void DeleteStaff(string staffID)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Staff> GetAllStaff()
        {
            throw new NotImplementedException();
        }

        public void UpdateStaff(string staffID)
        {
            throw new NotImplementedException();
        }


        public Staff GetStaff(string staffID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetStaffWithSelectedFields(Func<Staff, object> generate)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region SuccessiveStations

        public void AddSuccessiveStations(SuccessiveStations successiveStations)
        {
            throw new NotImplementedException();
        }
        public void DeleteSuccessiveStations(string entityKey)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<SuccessiveStations> GetAllSuccessiveStations()
        {
            throw new NotImplementedException();
        }

        public SuccessiveStations GetSuccessiveStations(int stat1, int stat2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetSuccessiveStationsWithSelectedFields(Func<SuccessiveStations, object> generate)
        {
            throw new NotImplementedException();
        }
        public void UpdateSuccessiveStations(string entityKey)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region User
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }
        public void DeleteUser(string name)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }


        public User GetUser(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(string name)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region UserTrip
        public void AddUserTrip(UserTrip userTrip)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserTrip(int travelID)
        {
            throw new NotImplementedException();
        }
        public void UpdateUserTrip(int travelID)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserTrip> GetAllUserTrip()
        {
            throw new NotImplementedException();
        }

        public UserTrip GetUserTrip(int travelID)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<object> GetUserTripWithSelectedFields(Func<UserTrip, object> generate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetUserWithSelectedFields(Func<SuccessiveStations, object> generate)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
