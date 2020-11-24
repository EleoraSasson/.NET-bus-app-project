
using System;
using System.Collections;
using System.Collections.Generic;
namespace Ex3a_GUI
{

    //*BUS_ROUTES IN DATABASE*//

    class BusDatabase : IEnumerable//object made: database of bus routes (list of selected lines)
    {

        /*CLASS CTOR*/
        public BusDatabase() { routes = new List<BusLine>(); } //default ctor

        /*CLASS MEMBERS*/

        public List<BusLine> routes;

        //IEnumerator and IEnumerable Implementation:
        public IEnumerator GetEnumerator()
        {
            return new AIEnumerator(this);
        }

        private class AIEnumerator : IEnumerator
        {
            private BusDatabase instance;
            private int position = -1;

            public AIEnumerator(BusDatabase inst)
            {
                this.instance = inst;
            }

            //IEnumerator
            public bool MoveNext()
            {
                position++;
                return (position < instance.routes.Count);
            }
            //IEnumerable
            public void Reset()
            {
                position = -1;
            }
            //IEnumerable
            public object Current
            {
                get { return instance.routes[position]; }
            }
        }

        /*CLASS METHODS*/

        /*A add/remove line*/
        /* Method: addLine
        * Description: recieves a line number and adds the line to the database (keeping in mind that each line appears twice - to and from )
        * Return Type: void
        */
        public void addLine()
        {
            Console.WriteLine("Please enter the line number you wish to add: ");
            int lineNum = Convert.ToInt32(Console.ReadLine());
            foreach (BusLine line in routes)
            {
                if (line.BusLineNum == lineNum)
                {
                    throw new ArgumentException("Error: line already exists.\n");
                }
            }
            Console.WriteLine("Please indicate which area your BusLine is in:");
            Console.WriteLine("1) North Golan, 2) North Haifa, 3) Center Tel-Aviv, 4) Center Jerusalem, 5) South Beer-Sheva, 6) South Eilat, 7) National");
            int area = Convert.ToInt32(Console.ReadLine());
            while (area < 1 || area > 7)
            {
                Console.WriteLine("ERROR: invalid area, please indicate area again: ");
                area = Convert.ToInt32(Console.ReadLine());

            }


            BusLine busLine = new BusLine(lineNum, (Areas)area);
            routes.Add(busLine);
        }


        public void randAddLine()
        {
            Random num = new Random();
            int randNum = num.Next(1, 100); //maybe check if already exists?
            BusLine bus = new BusLine(randNum);
            routes.Add(bus);
        }

        /* Method: removeLine
        * Description: removes a bus line from the collection
        * Return Type: void
        */
        public void removeLine(int lineToRemove)
        {
            bool notFound = true;

            for (int i = 0; i < routes.Count; i++) //search for stop
            {
                if (lineToRemove == routes[i].BusLineNum)
                {
                    routes.RemoveAt(i); //remove line
                    notFound = false;
                }
            }

            if (notFound) // it hasn't been found
            {
                Console.WriteLine("Error: Bus stop does not exist in the route.");
            }
        }

        /*B returns list of bus lines passing through a given station*/

        /* Method: linesThroughStation
            * Description: recieves bus station key and returns a list of bus routes that pass through that station
            * Return Type: List<BusRoutes>
            */
        public List<int> linesThroughStation(int StationKey)
        {
            var LinesThruStation = new List<int>();

            for (int i = 0; i < routes.Count; i++)
            {
                //foreach (BusLine bl in routes[i])
                for (int k = 0; k < routes[i].stations.Count; k++)
                {
                    //foreach (BusStop stop in bl)
                    foreach (BusStop stop in routes[i].stations)
                    {
                        if (stop.BusStationKey == StationKey)
                        {
                            if (!LinesThruStation.Contains(routes[i].BusLineNum))
                            {
                                LinesThruStation.Add(routes[i].BusLineNum); //assuming each station is only once in routes
                            }
                        }

                    }
                }
            }

            if (LinesThruStation.Count < 0)
            {
                throw new ArgumentException("Error: no line goes through given station - station not found.");
            }

            return LinesThruStation;
        }

        /*C sorts lines according to time*/
        /* Method: sortLines
        * Description: sorts the bus lines according to the total time of the route, from shortest time to longest time
        * Return Type: void
        */
        public void sortLines(List<BusLine> busRoutes)
        {
            for (int i = 0; i < (routes.Count - 1); i++)
            {
                var longerRoute = busRoutes[i].CompareTo(busRoutes[i + 1]);

            }
        }

        /*D indexer - checks for line number*/

        /* Method: BusIndexer
        * Description: Indexer that receives a line number and returns the instance. If no such line exits it returns throws an exception.
        * Return Type: int
        */
        public int BusIndexer(int lineNum) //if this work we should call it in our main and other methods aswell
        {
            int index = 0;
            try //locate line and find index
            {
                index = routes.FindIndex(bus => bus.BusLineNum == lineNum);
            } //line not found
            catch { Console.WriteLine("Error: no such line exists"); index = -1; }
            return index;
        }
    }
    
}
