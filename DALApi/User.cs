using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class User :ICloneable
    {
        private string name;

        public string userName
        {
            get { return name; }
            set { name = value; }
        }

        private string password;

        public string userPassword   //should we add properties for a password?
        {
            get { return password; }
            set { password = value; }
        }

        private int permission;
         
        public int adminPermission //??
        {
            get { return permission; }
            set { permission = value; }
        }



    }
}
