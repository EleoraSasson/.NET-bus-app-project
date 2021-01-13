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


namespace DL
{
    class DLXml : IDAL
    {
        #region singelton
        static readonly DLXml instance = new DLXml();
        static DLXml() { }// static ctor to ensure instance init is done just before first usage
        DLXml() { } // default => private 
        public static DLXml Instance { get => instance; }// The public Instance property to use
        #endregion

        #region Setting roots and paths of XML Files:

        //XML FilePaths:
        static string busPath = @"Bus.xml";
        static string linePath = @"BusLine.xml";
        static string onTripPath = @"BusOnTrip.xml";
        static string stopPath = @"BusStop.xml";
        static string leavingPath = @"LineLeaving.xml";
        static string stationPath = @"LineStation.xml";
        static string staffPath = @"Staff.xml";
        static string succSPath = @"SuccessiveStations.xml";
        static string userPath = @"Users.xml";
        static string userTripPath = @"UserTrips.xml";

        //XML Root XElements:
        static XElement busRoot;
        static XElement lineRoot;
        static XElement onTripRoot;
        static XElement stopRoot;
        static XElement leavingRoot;
        static XElement stationRoot;
        static XElement staffRoot;
        static XElement succSRoot;
        static XElement userRoot;
        static XElement userTripRoot;

        #endregion

        //* Implemetation of all CRUD methods of DO Entities using XML files *//

        #region Bus [This region uses XElement Method of writing and reading xml files]
        //create
        public void AddBus(Bus bus)
        {

            XElement busRoot = XMLTools.LoadListFromXMLElement(busPath);

            XElement busReturn = (from b in busRoot.Elements()
                             where b.Element("license").Value == bus.BusLicense
                             select b).FirstOrDefault();

            if(busReturn != null)
            { throw new DO.InvalidBusLicenseException(bus.BusLicense, $"Duplicate bus - bus {bus.BusLicense} already exists in system."); }

             XElement busElement  = new XElement ("bus", new XElement("license", bus.BusLicense),
                                            new XElement("regDate", bus.BusRegDate),
                                            new XElement("maintenanceDate", bus.BusMaintenanceDate),
                                            new XElement("mileage", bus.BusMileage),
                                            new XElement("fuel", bus.BusFuel),
                                            new XElement("status", bus.BusStatus.ToString()),
                                            new XElement("isErased", bus.BusErased));
            busRoot.Add(busElement);
            XMLTools.SaveListToXMLElement(busRoot,busPath);
        }
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
            XElement busRoot = XMLTools.LoadListFromXMLElement(busPath);

            XElement busFind = (from b in busRoot.Elements()
                                  where b.Element("license").Value == bus.BusLicense
                                  select b).FirstOrDefault();

            if (busFind != null)
            {
                //no need to update fields license and registaration date as those are specific to newly added busses and never need changing.
                busFind.Element("maintenanceDate").Value = bus.BusMaintenanceDate.ToString();
                busFind.Element("mileage").Value = bus.BusMileage.ToString();
                busFind.Element("fuel").Value = bus.BusFuel.ToString();
                busFind.Element("status").Value = bus.BusStatus.ToString();
                busFind.Element("isErased").Value = bus.BusErased.ToString();
            }
            else throw new DO.InvalidBusLicenseException(bus.BusLicense, $"Error updating bus fleet: bus {bus.BusLicense} cannot be found in the system.");
          
            XMLTools.SaveListToXMLElement(busRoot, busPath);
        }
        //delete
        public void DeleteBus(string license)
        {
            XElement busRoot = XMLTools.LoadListFromXMLElement(busPath);

            XElement busFind = (from b in busRoot.Elements()
                          where b.Element("license").Value == license
                          select b).FirstOrDefault();

            if(busFind != null)
            {
                busFind.Remove();
                XMLTools.SaveListToXMLElement(busRoot, busPath);
            }
            else throw new DO.InvalidBusLicenseException(license, $"Error deleting bus from fleet: bus {license} cannot be found in the system.");
        }
        #endregion

        #region BusLine
        //create
        public void AddBusLine(BusLine busLine)
        {
            List<BusLine> list = XMLTools.LoadListFromXMLSerializer<BusLine>(linePath);

            if (list.FirstOrDefault(l => l.BusLineID == busLine.BusLineID) != null)
                throw new DO.InvalidBusLineException(busLine.BusLineID.ToString(), $"Duplicate! line {busLine.BusLineID} already exists.");

            //if(GetBusLine(busLine.BusLineID) == null)
            //    throw new DO.InvalidBusLineException(busLine.BusLineID.ToString(), $"Line {busLine.BusLineID} cannot be found in system.");

            list.Add(busLine);
            XMLTools.SaveListToXMLSerializer(list,linePath);
        }
        //retrieve
        public BusLine GetBusLine(int lineID)
        {
            List<BusLine> listLines = XMLTools.LoadListFromXMLSerializer<BusLine>(linePath);

            DO.BusLine bline = listLines.Find(l => l.BusLineID == lineID);

            if (bline != null)
                return bline;
            else 
                throw new DO.InvalidBusLineException(lineID.ToString(), $"Line {lineID} cannot be found in system.");
        }
        public IEnumerable<BusLine> GetAllBusLines()
        {
            List<BusLine> listLines = XMLTools.LoadListFromXMLSerializer<BusLine>(linePath);

            return from bline in listLines
                   select bline;
        }
        public IEnumerable<object> GetBusLineWithSelectedFields(Func<BusLine, object> generate)
        {
            List<BusLine> listLines = XMLTools.LoadListFromXMLSerializer<BusLine>(linePath);

            return from bline in listLines
                   select generate(bline);
        }
        //update
        public void UpdateBusLine(BusLine busline)
        {
            List<BusLine> listLines = XMLTools.LoadListFromXMLSerializer<BusLine>(linePath);

            DO.BusLine bline = listLines.Find(l => l.BusLineID == busline.BusLineID);
            if (bline != null)
            {
                listLines.Remove(bline);
                listLines.Add(busline);
            }
            else new DO.InvalidBusLineException(busline.BusLineID.ToString(), $"Line {busline.BusLineID} cannot be found in system.");

            XMLTools.SaveListToXMLSerializer(listLines, linePath);    
        }
        //delete
        public void DeleteBusLine(int lineID)
        {
            List<BusLine> listLines = XMLTools.LoadListFromXMLSerializer<BusLine>(linePath);

            DO.BusLine bline = listLines.Find(l => l.BusLineID == lineID);
            if (bline != null)
            {
                listLines.Remove(bline);
            }
            else new DO.InvalidBusLineException(lineID.ToString(), $"Line {lineID} cannot be found in system.");

            XMLTools.SaveListToXMLSerializer(listLines, linePath);
        }
            #endregion

        #region BusOnTrip
        //create
        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            List<BusOnTrip> list = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(onTripPath);

            if (list.FirstOrDefault(b => b.BusRoadID == busOnTrip.BusRoadID) != null)
                throw new DO.InvalidBusOnTripIDException(busOnTrip.BusRoadID.ToString(), $"Duplicate! line {busOnTrip.BusRoadID} already exists.");

            if (GetBusOnTrip(busOnTrip.BusRoadID) == null)
                throw new DO.InvalidBusOnTripIDException(busOnTrip.BusRoadID.ToString(), $"Line {busOnTrip.BusRoadID} cannot be found in system.");

            list.Add(busOnTrip);
            XMLTools.SaveListToXMLSerializer(list, onTripPath);
        }
        //retrieve
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
        //update
        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            throw new NotImplementedException();
        }
        //delete
        public void DeleteBusOnTrip(int roadID)
        {
            throw new NotImplementedException();

        }

        #endregion

        #region BusStop
        //create
        public void AddBusStop(BusStop busStop)
        {
            List<BusStop> list = XMLTools.LoadListFromXMLSerializer<BusStop>(stopPath);

            if (list.FirstOrDefault(b => b.StopCode == busStop.StopCode) != null)
                throw new DO.InvalidStopCodeException(busStop.StopCode.ToString(), $"Duplicate! Stop {busStop.StopCode} already exists.");

            if (GetBusStop(busStop.StopCode) == null)
                throw new DO.InvalidStopCodeException(busStop.StopCode.ToString(), $"Stop {busStop.StopCode} cannot be found in system.");

            list.Add(busStop);
            XMLTools.SaveListToXMLSerializer(list, stopPath); ;
        }
        
        //retrieve
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
        //update
        public void UpdateBusStop(BusStop busStop)
        {
            throw new NotImplementedException();
        }
        //delete
        public void DeleteBusStop(int stopCode)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region LineLeaving
        public void AddLineLeaving(LineLeaving lineLeaving)
        {
            List</**/> list = XMLTools.LoadListFromXMLSerializer</**/>(linePath);

            if (list.FirstOrDefault(l => l.BusLineID == /**/) != null)
                throw new DO.InvalidBusLineException(/**/, $"Duplicate! line {/**/} already exists.");

            if (Get/**/(/**/D) == null)
                throw new DO.InvalidBusLineException(/**/, $"Line {/**/} cannot be found in system.");

            list.Add(/**/);
            XMLTools.SaveListToXMLSerializer(list, /**/);
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
            List</**/> list = XMLTools.LoadListFromXMLSerializer</**/>(linePath);

            if (list.FirstOrDefault(l => l.BusLineID == /**/) != null)
                throw new DO.InvalidBusLineException(/**/, $"Duplicate! line {/**/} already exists.");

            if (Get/**/(/**/D) == null)
                throw new DO.InvalidBusLineException(/**/, $"Line {/**/} cannot be found in system.");

            list.Add(/**/);
            XMLTools.SaveListToXMLSerializer(list, /**/);
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
            List</**/> list = XMLTools.LoadListFromXMLSerializer</**/>(linePath);

            if (list.FirstOrDefault(l => l.BusLineID == /**/) != null)
                throw new DO.InvalidBusLineException(/**/, $"Duplicate! line {/**/} already exists.");

            if (Get/**/(/**/D) == null)
                throw new DO.InvalidBusLineException(/**/, $"Line {/**/} cannot be found in system.");

            list.Add(/**/);
            XMLTools.SaveListToXMLSerializer(list, /**/);
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
            List</**/> list = XMLTools.LoadListFromXMLSerializer</**/>(linePath);

            if (list.FirstOrDefault(l => l.BusLineID == /**/) != null)
                throw new DO.InvalidBusLineException(/**/, $"Duplicate! line {/**/} already exists.");

            if (Get/**/(/**/D) == null)
                throw new DO.InvalidBusLineException(/**/, $"Line {/**/} cannot be found in system.");

            list.Add(/**/);
            XMLTools.SaveListToXMLSerializer(list, /**/);
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
            List</**/> list = XMLTools.LoadListFromXMLSerializer</**/>(linePath);

            if (list.FirstOrDefault(l => l.BusLineID == /**/) != null)
                throw new DO.InvalidBusLineException(/**/, $"Duplicate! line {/**/} already exists.");

            if (Get/**/(/**/D) == null)
                throw new DO.InvalidBusLineException(/**/, $"Line {/**/} cannot be found in system.");

            list.Add(/**/);
            XMLTools.SaveListToXMLSerializer(list, /**/);
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
            List</**/> list = XMLTools.LoadListFromXMLSerializer</**/>(linePath);

            if (list.FirstOrDefault(l => l.BusLineID == /**/) != null)
                throw new DO.InvalidBusLineException(/**/, $"Duplicate! line {/**/} already exists.");

            if (Get/**/(/**/D) == null)
                throw new DO.InvalidBusLineException(/**/, $"Line {/**/} cannot be found in system.");

            list.Add(/**/);
            XMLTools.SaveListToXMLSerializer(list, /**/);
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
