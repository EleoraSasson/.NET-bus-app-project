using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALApi;
using DO;

namespace BO
{
    public class EmployeePortal
    {

        private Staff staff;

        public Staff Employee
        {
            get { return staff; }
            set { staff = value; }
        }


    }
}
