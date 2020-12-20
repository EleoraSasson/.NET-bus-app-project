using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALApi
{
    public class DALFactory
    {
        //we want in BL so that when we get to take things from DL layer
        //we don't want the BL to know with teh decisions of DL
        //all decisions should be in the dl region layer
        //design pattern is used to solve a problem
        public static IDAL GetDAL() { return (IDAL)new object(); }//need it to return type og DAL pbject

        //it is supposed to create a new object of type IDAL aka DLObject
        //check if it is in simple factory pattern

    }
}
