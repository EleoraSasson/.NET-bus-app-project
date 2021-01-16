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


namespace DAL
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

            if (busReturn != null)
            { throw new DO.InvalidBusLicenseException(bus.BusLicense, $"Duplicate bus - bus {bus.BusLicense} already exists in system."); }

            XElement busElement = new XElement("bus", new XElement("license", bus.BusLicense),
                                           new XElement("regDate", bus.BusRegDate),
                                           new XElement("maintenanceDate", bus.BusMaintenanceDate),
                                           new XElement("mileage", bus.BusMileage),
                                           new XElement("fuel", bus.BusFuel),
                                           new XElement("status", bus.BusStatus.ToString()),
                                           new XElement("isErased", bus.BusErased));
            busRoot.Add(busElement);
            XMLTools.SaveListToXMLElement(busRoot, busPath);
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

            if (busFind != null)
            {
                busFind.Remove();
                XMLTools.SaveListToXMLElement(busRoot, busPath);
            }
            else throw new DO.InvalidBusLicenseException(license, $"Error deleting bus from fleet: bus {license} cannot be found in the system.");
        }
        #endregion

        #region BusLine
        //create
        public int AddBusLine(BusLine busLine)
        {
            List<BusLine> list = XMLTools.LoadListFromXMLSerializer<BusLine>(linePath);

            busLine.BusLineID = DO.RunningNumbers.LineRunNum;
            if (list.FirstOrDefault(l => l.BusLineID == busLine.BusLineID) != null)
                throw new DO.InvalidBusLineException(busLine.BusLineID.ToString(), $"Duplicate! line {busLine.BusLineID} already exists.");
            
            list.Add(busLine);
            XMLTools.SaveListToXMLSerializer(list, linePath);
            return busLine.BusLineID; //returning running number
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
        public int AddBusOnTrip(BusOnTrip busOnTrip)
        {
            List<BusOnTrip> list = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(onTripPath);

            busOnTrip.BusRoadID = DO.RunningNumbers.BusRunNum;
            if (list.FirstOrDefault(b => b.BusRoadID == busOnTrip.BusRoadID) != null)
                throw new DO.InvalidBusOnTripIDException(busOnTrip.BusRoadID.ToString(), $"Duplicate! line {busOnTrip.BusRoadID} already exists.");

            list.Add(busOnTrip);
            XMLTools.SaveListToXMLSerializer(list, onTripPath);
            return busOnTrip.BusRoadID; //returning running number
        }
        //retrieve
        public BusOnTrip GetBusOnTrip(int roadID)
        {
            List<BusOnTrip> list = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(onTripPath);

            DO.BusOnTrip bonTrip = list.Find(l => l.BusRoadID == roadID);

            if (bonTrip != null)
                return bonTrip;
            else
                throw new DO.InvalidBusOnTripIDException(bonTrip.BusRoadID.ToString(), $"Line {bonTrip.BusRoadID} cannot be found in system.");
        }

        public IEnumerable<object> GetBusOnTripWithSelectedFields(Func<BusOnTrip, object> generate)
        {
            List<BusOnTrip> list = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(onTripPath);

            return from bonTrip in list
                   select generate(bonTrip);
        }

        public IEnumerable<BusOnTrip> GetAllBusesOnTrip()
        {
            List<BusOnTrip> list = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(onTripPath);

            return from bonTrip in list
                   select bonTrip;
        }
        //update
        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            List<BusOnTrip> list = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(onTripPath);

            DO.BusOnTrip bonTrip = list.Find(l => l.BusRoadID == busOnTrip.BusRoadID);
            if (bonTrip != null)
            {
                list.Remove(bonTrip);
                list.Add(bonTrip);
            }
            else 
                throw new DO.InvalidBusOnTripIDException(bonTrip.BusRoadID.ToString(), $"Line {bonTrip.BusRoadID} cannot be found in system.");

            XMLTools.SaveListToXMLSerializer(list, onTripPath);
        }
        //delete
        public void DeleteBusOnTrip(int roadID)
        {
            List<BusOnTrip> list = XMLTools.LoadListFromXMLSerializer<BusOnTrip>(onTripPath);

            DO.BusOnTrip bonTrip = list.Find(l => l.BusRoadID == roadID);
            if (bonTrip != null)
            {
                list.Remove(bonTrip);
            }
            else
                throw new DO.InvalidBusOnTripIDException(roadID.ToString(), $"Line {roadID} cannot be found in system.");

            XMLTools.SaveListToXMLSerializer(list, onTripPath);

        }

        #endregion

        #region BusStop
        //create
        public void AddBusStop(BusStop busStop)
        {
            List<BusStop> list = XMLTools.LoadListFromXMLSerializer<BusStop>(stopPath);

            if (list.FirstOrDefault(b => b.StopCode == busStop.StopCode) != null)
                throw new DO.InvalidStopCodeException(busStop.StopCode.ToString(), $"Duplicate! Stop {busStop.StopCode} already exists.");

            list.Add(busStop);
            XMLTools.SaveListToXMLSerializer(list, stopPath); 
        }

        //retrieve
        public IEnumerable<BusStop> GetAllBusStops()
        {
            List<BusStop> list = XMLTools.LoadListFromXMLSerializer<BusStop>(stopPath);

            return from bStop in list
                   select bStop;
        }

        public BusStop GetBusStop(int stopCode)
        {
            List<BusStop> list = XMLTools.LoadListFromXMLSerializer<BusStop>(stopPath);

            DO.BusStop bStop = list.Find(l => l.StopCode == stopCode);

            if (bStop != null)
                return bStop;
            else
                throw new DO.InvalidStopCodeException(bStop.StopCode.ToString(), $"Line {bStop.StopCode} cannot be found in system.");
        }

        public IEnumerable<object> GetBusStopWithSelectedFields(Func<BusStop, object> generate)
        {
            List<BusStop> list = XMLTools.LoadListFromXMLSerializer<BusStop>(stopPath);

            return from bStop in list
                   select generate(bStop);
        }
        //update
        public void UpdateBusStop(BusStop busStop)
        {
            List<BusStop> list = XMLTools.LoadListFromXMLSerializer<BusStop>(stopPath);

            DO.BusStop bStop = list.Find(l => l.StopCode == busStop.StopCode);
            if (bStop != null)
            {
                list.Remove(bStop);
                list.Add(bStop);
            }
            else
                throw new DO.InvalidStopCodeException(bStop.StopCode.ToString(), $"Line {bStop.StopCode} cannot be found in system.");

            XMLTools.SaveListToXMLSerializer(list, stopPath);
        }
        //delete
        public void DeleteBusStop(int stopCode)
        {
            List<BusStop> list = XMLTools.LoadListFromXMLSerializer<BusStop>(stopPath);

            DO.BusStop bStop = list.Find(l => l.StopCode == stopCode);
            if (bStop != null)
            {
                list.Remove(bStop);
            }
            else
                throw new DO.InvalidBusOnTripIDException(stopCode.ToString(), $"Line {stopCode} cannot be found in system.");

            XMLTools.SaveListToXMLSerializer(list, stopPath);
        }
        #endregion

        #region LineLeaving
        public void AddLineLeaving(LineLeaving lineLeaving)
        {
            List<LineLeaving> list = XMLTools.LoadListFromXMLSerializer<LineLeaving>(leavingPath);
            string key = lineLeaving.BusLineID + lineLeaving.BusFirstLine.ToString();
            if (list.FirstOrDefault(b => b.BusLineID + b.BusFirstLine.ToString() == key) != null)
                throw new DO.InvalidLineLeavingKeyException(key, $"Line leaving {key} already exists.");

            if (list.FirstOrDefault(l => l.BusLineID == lineLeaving.BusLineID) != null)
                throw new DO.InvalidBusLineException(lineLeaving.BusLineID.ToString(),$"Duplicate! line {lineLeaving.BusLineID} already exists.");

            list.Add(lineLeaving);
            XMLTools.SaveListToXMLSerializer(list, leavingPath);
        }

        public void DeleteLineLeaving(int lineID, TimeSpan startTime)
        {
            List<LineLeaving> list = XMLTools.LoadListFromXMLSerializer<LineLeaving>(leavingPath);
            string key = lineID.ToString() + startTime.ToString();
            DO.LineLeaving leaving = list.Find(b => b.BusLineID + b.BusFirstLine.ToString() == key);
            if (leaving != null)
            {
                list.Remove(leaving);
            }
            else
                throw new DO.InvalidLineLeavingKeyException(key, $"Line leaving {key} does not exist.");

            XMLTools.SaveListToXMLSerializer(list, leavingPath);
        }

        public IEnumerable<LineLeaving> GetAllLinesLeaving()
        {
            List<LineLeaving> list = XMLTools.LoadListFromXMLSerializer<LineLeaving>(leavingPath);

            return from leaving in list
                   select leaving;
        }

        public LineLeaving GetLineLeaving(int lineID, TimeSpan startTime)
        {
            List<LineLeaving> list = XMLTools.LoadListFromXMLSerializer<LineLeaving>(leavingPath);
            string key = lineID.ToString() + startTime.ToString();
            DO.LineLeaving line = list.Find(b => b.BusLineID + b.BusFirstLine.ToString() == key);

            if (line != null)
                return line;
            else
                throw new DO.InvalidLineLeavingKeyException(key, $"Line leaving {key} does not exist.");
        }

        public IEnumerable<object> GetLineLeavingWithSelectedFields(Func<LineLeaving, object> generate)
        {
            List<LineLeaving> list = XMLTools.LoadListFromXMLSerializer<LineLeaving>(leavingPath);

            return from leaving in list
                   select generate(leaving);
        }

        public void UpdateLineLeaving(LineLeaving lineLeaving)
        {
            List<LineLeaving> list = XMLTools.LoadListFromXMLSerializer<LineLeaving>(leavingPath);
            string key = lineLeaving.BusLineID + lineLeaving.BusFirstLine.ToString();
            DO.LineLeaving line = list.Find(b => b.BusLineID + b.BusFirstLine.ToString() == key);
           
            if (line != null)
            {
                list.Remove(line);
                list.Add(line);
            }
            else
                throw new DO.InvalidLineLeavingKeyException(key, $"Line leaving {key} does not exist.");

            XMLTools.SaveListToXMLSerializer(list, leavingPath);
        }

        #endregion

        #region LineStation

        public void AddLineStation(LineStation lineStation, int lineID)
        {
            lineStation.lineID = lineID.ToString(); //assigning the LineID to Linestation
            List<LineStation> list = XMLTools.LoadListFromXMLSerializer<LineStation>(stationPath);

            if (list.FirstOrDefault(s => s.lineID == lineStation.lineID) != null)
                throw new DO.InvalidBusLineException(lineStation.lineID.ToString(), $"Duplicate! line {lineStation.lineID} already exists.");

            list.Add(lineStation);
            XMLTools.SaveListToXMLSerializer(list, stationPath);
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
            //List</**/> list = XMLTools.LoadListFromXMLSerializer</**/>(linePath);

            //if (list.FirstOrDefault(l => l.BusLineID == /**/) != null)
            //    throw new DO.InvalidBusLineException(/**/, $"Duplicate! line {/**/} already exists.");

            //if (Get/**/(/**/D) == null)
            //    throw new DO.InvalidBusLineException(/**/, $"Line {/**/} cannot be found in system.");

            //list.Add(/**/);
            //XMLTools.SaveListToXMLSerializer(list, /**/);
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
            //    List</**/> list = XMLTools.LoadListFromXMLSerializer</**/>(linePath);

            //    if (list.FirstOrDefault(l => l.BusLineID == /**/) != null)
            //        throw new DO.InvalidBusLineException(/**/, $"Duplicate! line {/**/} already exists.");

            //    if (Get/**/(/**/D) == null)
            //        throw new DO.InvalidBusLineException(/**/, $"Line {/**/} cannot be found in system.");

            //    list.Add(/**/);
            //    XMLTools.SaveListToXMLSerializer(list, /**/);
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
            //    List</**/> list = XMLTools.LoadListFromXMLSerializer</**/>(linePath);

            //    if (list.FirstOrDefault(l => l.BusLineID == /**/) != null)
            //        throw new DO.InvalidBusLineException(/**/, $"Duplicate! line {/**/} already exists.");

            //    if (Get/**/(/**/D) == null)
            //        throw new DO.InvalidBusLineException(/**/, $"Line {/**/} cannot be found in system.");

            //    list.Add(/**/);
            //    XMLTools.SaveListToXMLSerializer(list, /**/);
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
        public int AddUserTrip(UserTrip userTrip)
        {
            List<UserTrip> list = XMLTools.LoadListFromXMLSerializer<UserTrip>(userTripPath);

            userTrip.userTravelID = DO.RunningNumbers.UserRunNum;

            if (list.FirstOrDefault(ut => ut.userTravelID == userTrip.userTravelID) != null)
                throw new DO.ExsistingUserTripException(userTrip.userTravelID.ToString(), $"Duplicate user {userTrip.userTravelID}.");

            list.Add(userTrip);
            XMLTools.SaveListToXMLSerializer(list, userTripPath);

            return userTrip.userTravelID; //returning running number
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
