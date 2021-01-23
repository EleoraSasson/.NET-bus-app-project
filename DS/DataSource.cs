using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DALApi;
using System.Text.RegularExpressions;
using System.Device.Location;

namespace DS
{
	public static class DataSource
	{
		public static List<Bus> busList;
		public static List<BusLine> busLineList;
		public static List<BusOnTrip> busOnTripList;
		public static List<BusStop> busStopList;
		public static List<LineLeaving> lineLeavingList;
		public static List<LineStation> lineStationList;
		public static List<Staff> staffList;
		public static List<SuccessiveStations> succStationsList;
		public static List<User> usersList;
		public static List<Admin> adminList;
		static DataSource() //add aan init to start off witha basic bunch of buses
		{
			
			//Defines and a new list of SuccessiveStations and adds an object of type SuccessiceStations to the List:
			succStationsList = new List<SuccessiveStations>();

			//Defines and a new list of User and adds an object of type User to the List:
			usersList = new List<User>();

			////Defines and a new list of UserTrip and adds an object of type UserTrip to the List:
			//userTripList = new List<UserTrip>();

			#region INIT busList
			//Defines and a new list of Buses and adds an object of type Bus to the List:
			busList = new List<Bus>();
			initBusList(busList);

			void initBusList(List<Bus> busList)
			{

				//randomly initialize 20 buses
				for (int k = 0; k < 20; k++)
				{
					System.Threading.Thread.Sleep(20); //allows for randomised values to be random 
					Bus b = new Bus();
					b.BusRegDate = randDate();
					b.BusMaintenanceDate = DateTime.Now;
					b.BusLicense = randLicense(b);
					b.BusMileage = randMileage();
					System.Threading.Thread.Sleep(10);
					b.BusStatus = Status.Available;
					b.BusFuel = randFuel();
					busList.Add(b);
				}
			}

			//Methods to Randomise bus initialisation:
			DateTime randDate()
			{
				Random y = new Random();
				int year = y.Next(2010, 2021); //random year
				Random m = new Random();
				int month = m.Next(1, 13); //random month
				Random d = new Random();
				int day = d.Next(1, 32);
				var NewDate = new DateTime(year, month, day);
				return NewDate;
			}

			//returns random license number according to the manufacture year of the bus
			string randLicense(Bus bus)
			{
				Random rand = new Random();
				string license;
				if (bus.BusRegDate.Year < 2018) // license will have the format XX-XXX-XX
				{
					int l = rand.Next(10000000, 99999999);
					string Blicense = Convert.ToString(l);
					var num1 = Blicense.Substring(0, 2);
					var num2 = Blicense.Substring(2, 3);
					var num3 = Blicense.Substring(5, 2);
					license = num1 + "-" + num2 + "-" + num3;
					return license;
				}
				else // license will have the format XXX-XX-XXX
				{
					int l = rand.Next(10000000, 99999999);
					string Blicense = Convert.ToString(l);
					license = Regex.Replace(Blicense, @"^(...)(..)(...)$", "$1-$2-$3");
				}
				return license;
			}

			//returns random mileage
			int randMileage()
			{
				Random mile = new Random();
				int mi = mile.Next(0, 100000);
				return mi;
			}
			//returns random fuel
			int randFuel()
			{
				Random fu = new Random();
				int fue = fu.Next(0, 1200);
				return fue;
			}
			#endregion

			#region INIT BusLine
			//Defines and a new list of BusLines and adds an object of type BusLine to the List:
			// initialised with 8 buslines
			busLineList = new List<BusLine>()
			{
					new BusLine
					{
						BusLineID = RunningNumbers.LineRunNum.ToString(),
						BusLineNo = 1,
						BusRegion = Regions.South_Eilat,
						BusStart = "38831 ",
						BusEnd = "39001",
						LineErased = false
					},
					new BusLine
					{
						BusLineID = RunningNumbers.LineRunNum.ToString(),
						BusLineNo = 2,
						BusRegion = Regions.North_Haifa,
						BusStart = "389004 ",
						BusEnd = "39008",
					},
					new BusLine
					{
						BusLineID = RunningNumbers.LineRunNum.ToString(),
						BusLineNo = 3,
						BusRegion = Regions.Center_Jerusalem,
						BusStart = "39012 ",
						BusEnd = "39024",
						LineErased = false
					},
					new BusLine
					{
						BusLineID = RunningNumbers.LineRunNum.ToString(),
						BusLineNo = 4,
						BusRegion = Regions.Center_TelAviv,
						BusStart = "39028 ",
						BusEnd = "39050",
						LineErased = false
					},
					new BusLine
					{
						BusLineID = RunningNumbers.LineRunNum.ToString(),
						BusLineNo = 5,
						BusRegion = Regions.Center_TelAviv,
						BusStart = "39028  ",
						BusEnd = "39051",
						LineErased = false
					},
					new BusLine
					{
						BusLineID = RunningNumbers.LineRunNum.ToString(),
						BusLineNo = 6,
						BusRegion = Regions.National,
						BusStart = "38831  ",
						BusEnd = "39066 ",
						LineErased = false
					},
					new BusLine
					{
						BusLineID = RunningNumbers.LineRunNum.ToString(),
						BusLineNo = 7,
						BusRegion = Regions.South_BeerSheva,
						BusStart = "39093  ",
						BusEnd = "39043 ",
						LineErased = false
					},
					new BusLine
					{
						BusLineID = RunningNumbers.LineRunNum.ToString(),
						BusLineNo = 8,
						BusRegion = Regions.Center_TelAviv,
						BusStart = "39012  ",
						BusEnd = "39066",
						LineErased = false
					},
					 new BusLine
					{
						BusLineID = RunningNumbers.LineRunNum.ToString(),
						BusLineNo = 9,
						BusRegion = Regions.North_Golan,
						BusStart = "39049 ",
						BusEnd = "389005",
						LineErased = false
					},
					  new BusLine
					{
						BusLineID = RunningNumbers.LineRunNum.ToString(),
						BusLineNo = 10,
						BusRegion = Regions.National,
						BusStart = "39014 ",
						BusEnd = "389005",
						LineErased = false
					},
			};
			#endregion

			#region INIT BusStops
			//Defines and a new list of BusStops and adds an object of type BusStop to the List:
			busStopList = new List<BusStop>()
			{
					new BusStop
					{
						StopCode = "38831",
						StopName = " בי''ס בר לב / בן יהודה",
						StopAddress = " רחוב:בן יהודה 76 עיר: כפר סבא רציף: קומה",
						StopLocation = new GeoCoordinate(32.183921, 34.917806),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "38894",
						StopName = "פיינברג/שכביץ",
						StopAddress = " רחוב:פיינברג 4 עיר: גדרה רציף:   קומה",
						StopLocation = new GeoCoordinate(31.813285, 34.775928),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "38903",
						StopName = "קרוננברג/ארגמן",
						StopAddress = "רחוב:יוסף קרוננברג  עיר: רחובות רציף:   קומה",
						StopLocation = new GeoCoordinate(31.878667, 34.81138),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "38912",
						StopName = "השומר/האבות",
						StopAddress = "רחוב:השומר 22 עיר: ראשון לציון רציף:   קומה",
						StopLocation = new GeoCoordinate(31.959821, 34.814747),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "38916",
						StopName = "יוסף בורג/משואות יצחק",
						StopAddress = "רחוב:ד''ר יוסף בורג 9 עיר: ראשון לציון רציף:   קומה",
						StopLocation = new GeoCoordinate(31.968049, 34.818099),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "38922",
						StopName = "השר חיים שפירא/הרב שלום ג'רופי",
						StopAddress = "רחוב:השר חיים משה שפירא 16 עיר: ראשון לציון רציף:   קומה",
						StopLocation = new GeoCoordinate(31.990757, 34.755683),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39001",
						StopName = "שדרות יעקב/יוסף הנשיא",
						StopAddress = "רחוב:שדרות יעקב 65 עיר: ראשון לציון רציף:   קומה",
						StopLocation = new GeoCoordinate(31.950254, 34.819244),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39002",
						StopName = "שדרות יעקב/עזרא",
						StopAddress = "רחוב:שדרות יעקב 59 עיר: ראשון לציון רציף:   קומה",
						StopLocation = new GeoCoordinate(31.95111, 34.819766),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39004",
						StopName = "לייב יוספזון/יעקב ברמן",
						StopAddress = "רחוב:יהודה לייב יוספזון  עיר: רחובות רציף:   קומה",
						StopLocation = new GeoCoordinate(31.905052, 34.818909),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39005",
						StopName = "הרב יעקב ברמן/הרב יהודה צבי מלצר",
						StopAddress = " רחוב:הרב יעקב ברמן 4 עיר: רחובות רציף:   קומה",
						StopLocation = new GeoCoordinate(31.901879, 34.819443),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39006",
						StopName = "ברמן/מלצר",
						StopAddress = "רחוב:הרב יעקב ברמן  עיר: רחובות רציף:   קומה",
						StopLocation = new GeoCoordinate(31.90281, 34.818922),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39007",
						StopName = "הנשיא הראשון/מכון ויצמן",
						StopAddress = "רחוב:הנשיא הראשון 55 עיר: רחובות רציף:   קומה",
						StopLocation = new GeoCoordinate(31.904567, 34.815296),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39008",
						StopName = "הנשיא הראשון/קיפניס",
						StopAddress = "רחוב:הנשיא הראשון 56 עיר: רחובות רציף:   קומה",
						StopLocation = new GeoCoordinate(31.904755, 34.816661),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39012",
						StopName = "הירדן/הערבה",
						StopAddress = "רחוב:הירדן 23 עיר: באר יעקב רציף:   קומה",
						StopLocation = new GeoCoordinate(31.937387, 34.838609),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39013",
						StopName = "הירדן/חרוד",
						StopAddress = "רחוב:הירדן 22 עיר: באר יעקב רציף:   קומה",
						StopLocation = new GeoCoordinate(31.936925, 34.838341),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39014",
						StopName = "האלונים/הדקל",
						StopAddress = "רחוב:שדרות האלונים  עיר: באר יעקב רציף:   קומה",
						StopLocation = new GeoCoordinate(31.939037, 34.831964),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39017",
						StopName = "האלונים א/הדקל",
						StopAddress = "רחוב:שדרות האלונים  עיר: באר יעקב רציף:   קומה",
						StopLocation = new GeoCoordinate(31.939656, 34.832104),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39018",
						StopName = "פארק תעשיות שילת",
						StopAddress = "רחוב:דרך הזית  עיר: שילת רציף:   קומה",
						StopLocation = new GeoCoordinate(31.914324, 35.023589),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39019",
						StopName = "פארק תעשיות שילת",
						StopAddress = "רחוב:דרך הזית  עיר: שילת רציף:   קומה",
						StopLocation = new GeoCoordinate(31.914816, 35.023028),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39024",
						StopName = "עיריית מודיעין מכבים רעות",
						StopAddress = "רחוב:  עיר: מודיעין מכבים רעות רציף:   קומה",
						StopLocation = new GeoCoordinate(31.908499, 35.007955),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39028",
						StopName = "חיים ברלב/מרדכי מקלף",
						StopAddress = "רחוב:חיים ברלב 30 עיר: מודיעין מכבים רעות רציף:   קומה",
						StopLocation = new GeoCoordinate(31.907828, 35.000614),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39040",
						StopAddress = " רחוב:רבאט  עיר: רמלה רציף:   קומה:  ",
						StopName = "גן חק''ל/רבאט",
						StopLocation = new GeoCoordinate(31.931204, 34.884956),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39041",
						StopAddress = " רחוב:דוכיפת  עיר: רמלה רציף:   קומה:  ",
						StopName = "קניון צ. רמלה לוד/דוכיפת",
						StopLocation = new GeoCoordinate(31.933379, 34.887207),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39042",
						StopAddress = " רחוב:היצירה 2 עיר: רמלה רציף:   קומה:  ",
						StopName = "היצירה/התקווה",
						StopLocation = new GeoCoordinate(31.929318, 34.880069),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39043",
						StopAddress = " רחוב:היצירה  עיר: רמלה רציף:   קומה:  ",
						StopName = "היצירה/התקווה",
						StopLocation = new GeoCoordinate(31.929199, 34.879993),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39044",
						StopAddress = " רחוב:עמל  עיר: רמלה רציף:   קומה:  ",
						StopName = "עמל/היצירה",
						StopLocation = new GeoCoordinate(31.932402, 34.881442),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39049",
						StopAddress = " רחוב:ישראל פרנקל 10 עיר: רמלה רציף:   קומה:  ",
						StopName = "פרנקל/ויתקין",
						StopLocation = new GeoCoordinate(31.936159, 34.864906),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39050",
						StopAddress = " רחוב:ישראל פרנקל 11 עיר: רמלה רציף:   קומה:  ",
						StopName = "פרנקל/ויתקין",
						StopLocation = new GeoCoordinate(31.936022, 34.86495),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39051",
						StopAddress = " רחוב:ישראל פרנקל 17 עיר: רמלה רציף:   קומה:  ",
						StopName = "ישראל פרנקל/דוב הוז",
						StopLocation = new GeoCoordinate(31.935488, 34.863972),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39052",
						StopAddress = " רחוב:גיורא יוספטל 6 עיר: רמלה רציף:   קומה:  ",
						StopName = "יוספטל/הדס",
						StopLocation = new GeoCoordinate(31.936109, 34.857638),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39056",
						StopAddress = " רחוב:שמחה הולצברג  עיר: רמלה רציף:   קומה:  ",
						StopName = "אהרון בוגנים/משה שרת",
						StopLocation = new GeoCoordinate(31.933413, 34.853906),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39057",
						StopAddress = " רחוב:שמחה הולצברג 10 עיר: רמלה רציף:   קומה:  ",
						StopName = "גרשון ש''ץ/שמחה הולצברג",
						StopLocation = new GeoCoordinate(31.932532, 34.853223),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39058",
						StopAddress = " רחוב:שמחה הולצברג 4 עיר: רמלה רציף:   קומה:  ",
						StopName = "הולצברג/שץ",
						StopLocation = new GeoCoordinate(31.93166, 34.853149),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39059",
						StopAddress = " רחוב:לוי אשכול 11 עיר: רמלה רציף:   קומה:  ",
						StopName = "אשכול/הרב שפירא",
						StopLocation = new GeoCoordinate(31.929827, 34.857194),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39060",
						StopAddress = " רחוב:יהודה שטיין  עיר: רמלה רציף:   קומה:  ",
						StopName = "יהודה שטיין/קרן היסוד",
						StopLocation = new GeoCoordinate(31.926545, 34.855866),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39066",
						StopAddress = " רחוב:שמשון הגיבור  עיר: רמלה רציף:   קומה:  ",
						StopName = "שמשון הגיבור/המסגד",
						StopLocation = new GeoCoordinate(31.926441, 34.866014),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39068",
						StopAddress = " רחוב:ח.נ. ביאליק 19 עיר: רמלה רציף:   קומה:",
						StopName = "ביאליק/חניתה",
						StopLocation = new GeoCoordinate(31.924484, 34.870366),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39069",
						StopAddress = " רחוב:ח.נ. ביאליק 43 עיר: רמלה רציף:   קומה:",
						StopName = "ביאליק/ירמיהו",
						StopLocation = new GeoCoordinate(31.92055, 34.868205),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39070",
						StopAddress = " רחוב:א.ס. לוי 1 עיר: רמלה רציף:   קומה:  ",
						StopName = "א.ס לוי/סעדיה מרדכי",
						StopLocation = new GeoCoordinate(31.9209, 34.861221),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39071",
						StopAddress = " רחוב:שדרות דוד רזיאל 5 עיר: רמלה רציף:   קומה:  ",
						StopName = "רזיאל/זכריה",
						StopLocation = new GeoCoordinate(31.923666, 34.862813),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39072",
						StopAddress = " רחוב:חרוד  עיר: ישרש רציף:   קומה:  ",
						StopName = "חרוד א",
						StopLocation = new GeoCoordinate(31.912572, 34.850134),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39073",
						StopAddress = " רחוב:חרוד  עיר: ישרש רציף:   קומה:  ",
						StopName = "חרוד/הירדן",
						StopLocation = new GeoCoordinate(31.915977, 34.848217),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39075",
						StopAddress = " רחוב:הירדן  עיר: ישרש רציף:   קומה:  ",
						StopName = "הירדן/דן",
						StopLocation = new GeoCoordinate(31.915489, 34.852139),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39076",
						StopAddress = " רחוב:עוזי חיטמן 44 עיר: רמלה רציף:   קומה:  ",
						StopName = "עוזי חיטמן/שושנה דמארי",
						StopLocation = new GeoCoordinate(31.917022, 34.868261),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39091",
						StopAddress = "רחוב:דרך החרוב  עיר: שילת רציף:   קומה",
						StopName = "החרוב א",
						StopLocation = new GeoCoordinate(31.919207, 35.0234),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39092",
						StopAddress = "רחוב:  עיר: כפר רות רציף:   קומה",
						StopName = "כפר רות",
						StopLocation = new GeoCoordinate(31.910544, 35.034349),
						StopActive = true
					},
					new BusStop
					{
						StopCode = "39093",
						StopAddress = "רחוב:  עיר: כפר רות רציף:   קומה",
						StopName = "כפר רות",
						StopLocation = new GeoCoordinate(31.910485, 35.034441),
						StopActive = true
					}

			};
			#endregion

			#region INIT BusOnTrip
			//Defines and a new list of BusOnTrip and adds an object of type BusOnTrip to the List:

			busOnTripList = new List<BusOnTrip>()
			{
				new BusOnTrip
				{
					BusLineID = "11",
					BusRoadID = RunningNumbers.BusRunNum,
					BusFormalDT = new TimeSpan(8,30,00),
					BusArrivalT = new TimeSpan(11,45,00)
				},
				new BusOnTrip
				{
					BusLineID = "12",
					BusRoadID = RunningNumbers.BusRunNum,
					BusFormalDT = new TimeSpan(5,45,00),
					BusArrivalT = new TimeSpan(16,45,00)
				},
				new BusOnTrip
				{
					BusLineID = "13",
					BusRoadID = RunningNumbers.BusRunNum,
					BusFormalDT = new TimeSpan(6,15,00),
					BusArrivalT = new TimeSpan(11,30,00)
				},
				new BusOnTrip
				{
					BusLineID = "14",
					BusRoadID = RunningNumbers.BusRunNum,
					BusFormalDT = new TimeSpan(6,00,00),
					BusArrivalT = new TimeSpan(23,00,00)
				},
				new BusOnTrip
				{
					BusLineID = "15",
					BusRoadID = RunningNumbers.BusRunNum,
					BusFormalDT = new TimeSpan(9,30,00),
					BusArrivalT = new TimeSpan(16,33,00)
				},
				new BusOnTrip
				{
					BusLineID = "16",
					BusRoadID = RunningNumbers.BusRunNum,
					BusFormalDT = new TimeSpan(6,35,00),
					BusArrivalT = new TimeSpan(19,45,00)
				},
				new BusOnTrip
				{
					BusLineID = "17",
					BusRoadID = RunningNumbers.BusRunNum,
					BusFormalDT = new TimeSpan(7,30,00),
					BusArrivalT = new TimeSpan(12,45,00)
				},
				new BusOnTrip
				{
					BusLineID = "18",
					BusRoadID = RunningNumbers.BusRunNum,
					BusFormalDT = new TimeSpan(10,30,00),
					BusArrivalT = new TimeSpan(14,35,00)
				},
				new BusOnTrip
				{
					BusLineID = "19",
					BusRoadID = RunningNumbers.BusRunNum,
					BusFormalDT = new TimeSpan(7,30,00),
					BusArrivalT = new TimeSpan(22,15,00)
				},
				new BusOnTrip
				{
					BusLineID = "20",
					BusRoadID = RunningNumbers.BusRunNum,
					BusFormalDT = new TimeSpan(6,15,00),
					BusArrivalT = new TimeSpan(18,55,00)
				},

			};
			#endregion

			#region  INIT LineStation
			//Defines and a new list of LineStations and adds an object of type LineStation to the List:
			// initialised with 16 line Stations

			int i = 0;

			lineStationList = new List<LineStation>()
			{
				new LineStation
				{
					lineID = busLineList[i].BusLineID.ToString(),
					stationCode = "38831",
					stationNumber = 1
				},
				new LineStation
				{
					lineID = busLineList[i].BusLineID.ToString(),
					stationCode = "38894",
					stationNumber = 2
				},
				new LineStation
				{
					lineID = busLineList[i].BusLineID.ToString(),
					stationCode = "38903",
					stationNumber = 3
				},
				new LineStation
				{
					lineID = busLineList[i].BusLineID.ToString(),
					stationCode = "38912",
					stationNumber = 4
				},
				new LineStation
				{
					lineID = busLineList[i].BusLineID.ToString(),
					stationCode = "38916",
					stationNumber = 5
				},
				new LineStation
				{
					lineID = busLineList[i].BusLineID.ToString(),
					stationCode = "38922",
					stationNumber = 6
				},
				new LineStation
				{
					lineID = busLineList[i].BusLineID.ToString(),
					stationCode = "39001",
					stationNumber = 7
				},
				new LineStation
				{
					lineID = busLineList[i + 1].BusLineID.ToString(),
					stationCode = "389004 ",
					stationNumber = 1
				},
				new LineStation
				{
					lineID = busLineList[i + 1].BusLineID.ToString(),
					stationCode = "389006 ",
					stationNumber = 2
				},
				new LineStation
				{
					lineID = busLineList[i + 1].BusLineID.ToString(),
					stationCode = "389005 ",
					stationNumber = 3
				},
				new LineStation
				{
					lineID = busLineList[i + 1].BusLineID.ToString(),
					stationCode = "39007 ",
					stationNumber = 4
				},
				new LineStation
				{
					lineID = busLineList[i + 1].BusLineID.ToString(),
					stationCode = "39008",
					stationNumber = 5
				},
				new LineStation
				{
					lineID = busLineList[i + 2].BusLineID.ToString(),
					stationCode = "39012 ",
					stationNumber = 1
				},
				new LineStation
				{
					lineID = busLineList[i + 2].BusLineID.ToString(),
					stationCode = "39013 ",
					stationNumber = 2
				},
				new LineStation
				{
					lineID = busLineList[i + 2].BusLineID.ToString(),
					stationCode = "39014 ",
					stationNumber = 3
				},
				new LineStation
				{
					lineID = busLineList[i + 2].BusLineID.ToString(),
					stationCode = "39017 ",
					stationNumber = 4
				},
				new LineStation
				{
					lineID = busLineList[i + 2].BusLineID.ToString(),
					stationCode = "39018 ",
					stationNumber = 5
				},
				new LineStation
				{
					lineID = busLineList[i + 2].BusLineID.ToString(),
					stationCode = "39019 ",
					stationNumber = 6
				},
				new LineStation
				{
					lineID = busLineList[i + 2].BusLineID.ToString(),
					stationCode = "39024",
					stationNumber = 7
				},
				new LineStation
				{
					lineID = busLineList[i + 2].BusLineID.ToString(),
					stationCode = "39014 ",
					stationNumber = 3
				},
				new LineStation
				{
					lineID = busLineList[i + 2].BusLineID.ToString(),
					stationCode = "39017 ",
					stationNumber = 4
				},
				new LineStation
				{
					lineID = busLineList[i + 2].BusLineID.ToString(),
					stationCode = "39018 ",
					stationNumber = 5
				},
				new LineStation
				{
					lineID = busLineList[i + 2].BusLineID.ToString(),
					stationCode = "39019 ",
					stationNumber = 6
				},
				new LineStation
				{
					lineID = busLineList[i + 2].BusLineID.ToString(),
					stationCode = "39024",
					stationNumber = 7
				},
				new LineStation
				{
					lineID = busLineList[i + 3].BusLineID.ToString(),
					stationCode = "39028 ",
					stationNumber = 1
				},
				new LineStation
				{
					lineID = busLineList[i + 3].BusLineID.ToString(),
					stationCode = "39040 ",
					stationNumber = 2
				},
				new LineStation
				{
					lineID = busLineList[i + 3].BusLineID.ToString(),
					stationCode = "39024 ",
					stationNumber = 3
				},
				new LineStation
				{
					lineID = busLineList[i + 3].BusLineID.ToString(),
					stationCode = "39019 ",
					stationNumber = 4
				},
				new LineStation
				{
					lineID = busLineList[i + 3].BusLineID.ToString(),
					stationCode = "39017 ",
					stationNumber = 5
				},
				new LineStation
				{
					lineID = busLineList[i + 3].BusLineID.ToString(),
					stationCode = "39043 ",
					stationNumber = 6
				},
				new LineStation
				{
					lineID = busLineList[i + 3].BusLineID.ToString(),
					stationCode = "39051",
					stationNumber = 7
				},
				new LineStation
				{
					lineID = busLineList[i + 3].BusLineID.ToString(),
					stationCode = "39050",
					stationNumber = 8
				},
				new LineStation
				{
					lineID = busLineList[i + 4].BusLineID.ToString(),
					stationCode = "3939041",
					stationNumber = 1
				},
				new LineStation
				{
					lineID = busLineList[i + 4].BusLineID.ToString(),
					stationCode = "39044 ",
					stationNumber = 2
				},
				new LineStation
				{
					lineID = busLineList[i + 4].BusLineID.ToString(),
					stationCode = "39049",
					stationNumber = 3
				},
				new LineStation
				{
					lineID = busLineList[i + 4].BusLineID.ToString(),
					stationCode = "39043",
					stationNumber = 4
				},
				new LineStation
				{
					lineID = busLineList[i + 4].BusLineID.ToString(),
					stationCode = "39042",
					stationNumber = 5
				},
				new LineStation
				{
					lineID = busLineList[i + 4].BusLineID.ToString(),
					stationCode = "39050",
					stationNumber = 6
				},
				new LineStation
				{
					lineID = busLineList[i + 4].BusLineID.ToString(),
					stationCode = "39052",
					stationNumber = 7
				},
				new LineStation
				{
					lineID = busLineList[i + 4].BusLineID.ToString(),
					stationCode = "39051",
					stationNumber = 8
				},

				new LineStation
				{
					lineID = busLineList[i + 5].BusLineID.ToString(),
					stationCode = "38831",
					stationNumber = 1
				},
				new LineStation
				{
					lineID = busLineList[i + 5].BusLineID.ToString(),
					stationCode = " 39013",
					stationNumber = 2
				},
				new LineStation
				{
					lineID = busLineList[i + 5].BusLineID.ToString(),
					stationCode = " 39057",
					stationNumber = 3
				},
				new LineStation
				{
					lineID = busLineList[i + 5].BusLineID.ToString(),
					stationCode = " 39059",
					stationNumber = 4
				},
				new LineStation
				{
					lineID = busLineList[i + 5].BusLineID.ToString(),
					stationCode = " 39068",
					stationNumber = 5
				},
				new LineStation
				{
					lineID = busLineList[i + 5].BusLineID.ToString(),
					stationCode = " 39066",
					stationNumber = 6
				},

				new LineStation
				{
					lineID = busLineList[i + 6].BusLineID.ToString(),
					stationCode = "39093 ",
					stationNumber = 1
				},
				new LineStation
				{
					lineID = busLineList[i + 6].BusLineID.ToString(),
					stationCode = " 39092 ",
					stationNumber = 2
				},
				new LineStation
				{
					lineID = busLineList[i + 6].BusLineID.ToString(),
					stationCode = "39091 ",
					stationNumber = 3
				},
				new LineStation
				{
					lineID = busLineList[i + 6].BusLineID.ToString(),
					stationCode = "39076 ",
					stationNumber = 4
				},
				new LineStation
				{
					lineID = busLineList[i + 6].BusLineID.ToString(),
					stationCode = "39075",
					stationNumber = 5
				},
				new LineStation
				{
					lineID = busLineList[i + 6].BusLineID.ToString(),
					stationCode = " 39073",
					stationNumber = 6
				},
				new LineStation
				{
					lineID = busLineList[i + 6].BusLineID.ToString(),
					stationCode = " 390434",
					stationNumber = 7
				},

				new LineStation
				{
					lineID = busLineList[i + 7].BusLineID.ToString(),
					stationCode = "39012 ",
					stationNumber = 1
				},
				new LineStation
				{
					lineID = busLineList[i + 7].BusLineID.ToString(),
					stationCode = "39013 ",
					stationNumber = 2
				},
				new LineStation
				{
					lineID = busLineList[i + 7].BusLineID.ToString(),
					stationCode = "39066",
					stationNumber = 3
				},

				new LineStation
				{
					lineID = busLineList[i + 8].BusLineID.ToString(),
					stationCode = "39049 ",
					stationNumber = 1
				},
				new LineStation
				{
					lineID = busLineList[i + 8].BusLineID.ToString(),
					stationCode = "39043",
					stationNumber = 2
				},
				new LineStation
				{
					lineID = busLineList[i + 8].BusLineID.ToString(),
					stationCode = "39042",
					stationNumber = 3
				},
				new LineStation
				{
					lineID = busLineList[i + 8].BusLineID.ToString(),
					stationCode = " 389004 ",
					stationNumber = 4
				},
				new LineStation
				{
					lineID = busLineList[i + 8].BusLineID.ToString(),
					stationCode = "389006",
					stationNumber = 5
				},
				new LineStation
				{
					lineID = busLineList[i + 8].BusLineID.ToString(),
					stationCode = "389005",
					stationNumber = 6
				},


				new LineStation
				{
					lineID = busLineList[i + 9].BusLineID.ToString(),
					stationCode = "39014  ",
					stationNumber = 1
				},
				new LineStation
				{
					lineID = busLineList[i + 9].BusLineID.ToString(),
					stationCode = "39017 ",
					stationNumber = 2
				},
				new LineStation
				{
					lineID = busLineList[i + 9].BusLineID.ToString(),
					stationCode = "389006 ",
					stationNumber = 3
				},
				new LineStation
				{
					lineID = busLineList[i + 9].BusLineID.ToString(),
					stationCode = "389005",
					stationNumber = 4
				},

			};
			#endregion

			#region INIT LineLeaving
			//Defines and a new list of LineLeaving and adds an object of type LineLeaving to the List:
			lineLeavingList = new List<LineLeaving>()
			{
				new LineLeaving
				{
					BusLineID = "11",
					BusFirstLine =  new TimeSpan(8,30,00),
					BusLastLine =  new TimeSpan(16,30,00),
					BusDriver = "D111111",
				},
				new LineLeaving
				{
					BusLineID = "12",
					BusFirstLine = new TimeSpan(5,45,00),
					BusLastLine = new TimeSpan(2,45,00),
					BusDriver = "D111112",

				},
				new LineLeaving
				{
					BusLineID = "13",
					BusFirstLine = new TimeSpan(6,15,00),
					BusLastLine = new TimeSpan(5,45,00),
					BusDriver = "D111113",
				},
				new LineLeaving
				{
					BusLineID = "14",
					BusFirstLine = new TimeSpan(6,00,00),
					BusLastLine = new TimeSpan(12,55,00),
					BusDriver = "D111114",
				},

				new LineLeaving
				{
					BusLineID = "15",
					BusFirstLine = new TimeSpan(9,30,00),
					BusLastLine = new TimeSpan(14,55,00),
					BusDriver = "D111115",
				},
				new LineLeaving
				{
					BusLineID = "16",
					BusFirstLine = new TimeSpan(6,35,00),
					BusLastLine = new TimeSpan(14,45,00),
					BusDriver = "D111116",
				},
				new LineLeaving
				{
					BusLineID = "17",
					BusFirstLine = new TimeSpan(7,30,00),
					BusLastLine = new TimeSpan(13,00,00),
					BusDriver = "D111117",
				},
				new LineLeaving
				{
					BusLineID = "18",
					BusFirstLine = new TimeSpan(10,30,00),
					BusLastLine = new TimeSpan(12,55,00),
					BusDriver = "D111118",
				},
				new LineLeaving
				{
					BusLineID = "19",
					BusFirstLine = new TimeSpan(7,30,00),
					BusLastLine = new TimeSpan(14,45,00),
					BusDriver = "D111119",
				},
				new LineLeaving
				{
					BusLineID = "20",
					BusFirstLine = new TimeSpan(6,15,00),
					BusLastLine = new TimeSpan(16,30,00),
					BusDriver = "D111120",
				},
			};
			#endregion

			#region INIT User

			usersList = new List<User>()
			{
				new User
				{
					userName = "dave",
					userPassword = "pss123",
					userId = "234567",
					userFirst = "David",
					userLast = "Smith",
					adminPermission = false
				},

				new User
				{
					userName = "dan",
					userPassword = "mypass.2",
					userId = "345678",
					userFirst = "Daniel",
					userLast = "Brown",
					adminPermission = false
				},

				new User
				{
					userName = "Yossi",
					userPassword = "00password",
					userId = "456789",
					userFirst = "Yossef",
					userLast = "Garcia",
					adminPermission = false
				},

				new User
				{
					userName = "Hanna",
					userPassword = "login23",
					userId = "123456",
					userFirst = "Hanna",
					userLast = "Miller",
					adminPermission = false
				},

				new User
				{
					userName = "Nao",
					userPassword = "pass12word",
					userId = "567890",
					userFirst = "Naomi",
					userLast = "Davis",
					adminPermission = false
				},
			};
			#endregion

			#region INIT Staff
			staffList = new List<Staff>()
			{
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Mark",
					BusDriverLast = "Jones",
					BusDriverAge = 45,
					BusDriverCellNo = "053-293-2432",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Simon",
					BusDriverLast = "Cohen",
					BusDriverAge = 21,
					BusDriverCellNo = "053-111-2432",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 1,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Andy",
					BusDriverLast = "Tohmas",
					BusDriverAge = 45,
					BusDriverCellNo = "053-293-8978",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Sally",
					BusDriverLast = "Carles",
					BusDriverAge = 35,
					BusDriverCellNo = "053-123-2321",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Shira",
					BusDriverLast = "Hertz",
					BusDriverAge = 36,
					BusDriverCellNo = "053-659-2432",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "David",
					BusDriverLast = "Newman",
					BusDriverAge = 47,
					BusDriverCellNo = "053-909-2532",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Samuel",
					BusDriverLast = "Falkson",
					BusDriverAge = 38,
					BusDriverCellNo = "053-667-2432",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Sara",
					BusDriverLast = "Parker",
					BusDriverAge = 33,
					BusDriverCellNo = "053-293-8845",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Scheryl",
					BusDriverLast = "Black",
					BusDriverAge = 32,
					BusDriverCellNo = "053-446-3446",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Josh",
					BusDriverLast = "Davis",
					BusDriverAge = 29,
					BusDriverCellNo = "053-555-2432",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Julie",
					BusDriverLast = "Owens",
					BusDriverAge = 25,
					BusDriverCellNo = "053-293-0053",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Mark",
					BusDriverLast = "Cohen",
					BusDriverAge = 22,
					BusDriverCellNo = "053-293-2432",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Charlie",
					BusDriverLast = "Simons",
					BusDriverAge = 24,
					BusDriverCellNo = "053-293-8786",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Adam",
					BusDriverLast = "Nelson",
					BusDriverAge = 27,
					BusDriverCellNo = "058-293-2432",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Ruth",
					BusDriverLast = "Miller",
					BusDriverAge = 42,
					BusDriverCellNo = "053-293-9797",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Cathy",
					BusDriverLast = "Radford",
					BusDriverAge = 27,
					BusDriverCellNo = "053-777-2334",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Abigail",
					BusDriverLast = "Rosen",
					BusDriverAge = 36,
					BusDriverCellNo = "058-293-2422",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Robert",
					BusDriverLast = "Karlson",
					BusDriverAge = 35,
					BusDriverCellNo = "053-293-0003",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Betty",
					BusDriverLast = "Jones",
					BusDriverAge = 35,
					BusDriverCellNo = "053-293-8888",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				new Staff()
				{
					StaffPosition = Position.Driver,
					BusDriverFirst = "Moshe",
					BusDriverLast = "Hertzman",
					BusDriverAge = 25,
					BusDriverCellNo = "053-932-2432",
					BusDriverID = "D" + RunningNumbers.StaffRunNum.ToString(),
					isAdmin = true,
					StaffYrsWorked = 12,
					
				},
				
			};
			#endregion

			#region INIT Admin
			adminList = new List<Admin>()
			{
				new Admin()
				{
					adminName="Dina",
					adminPassword="idontlikecheese",
				},

				new Admin()
				{
					adminName =" Nick",
					adminPassword="ilikecheese",
				},
			};
            #endregion
        }

    }

}