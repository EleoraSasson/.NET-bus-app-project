using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DALApi
{
    public interface IDAL
    {
        //this is the service contract "what can i do for you"
        //will add methods soon
        //in an interface we have just methods
        //will implement and be talked to by BL what is DL willing to give out

        //Create retrieve update and delete {CRUD}
        //define for methods for each of the above CRUD
        //        The methods in IDAL should be built for each of the DO entities according to CRUD scheme
        //CRUD = Create / Request / Update / Delete.Request can be for a single object or a list of object according to a filter)
        //A filter condition can be done by a separate method or by a method that is passed as a predicate to the parameter


        /* IMPLEMENTING CRUD METHODS FOR BUS DO ENTITY */
        #region Bus
        // Create
        void AddBus(Bus bus); //create
        //Retrieve
        Bus GetBus(string license); //retrieve

        //Update
        //Delete

        IEnumerable<DO.Bus> GetAllBuses();
        IEnumerable<object> GetBusListWithSelectedFields(Func<DO.Bus, object> generate);
        
                              // void UpdateBus(Bus bus);
        void UpdateBus(string license, Action<Bus> update); //method that knows to updt specific fie lds in Student
        void DeleteBus(string license); // removes only Student, does not remove the appropriate Person...
        #endregion

        //committing




    }
}

