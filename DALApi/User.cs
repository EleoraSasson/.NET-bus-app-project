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

        private string first;

        public string userFirst
        {
            get { return first; }
            set { first = value; }
        }

        private string last;

        public string userLast
        {
            get { return last; }
            set { last = value; }
        }


        private string password;

        public string userPassword  
        {
            get { return password; }
            set { password = value; }
        }

        private string Id;

        public string userId
        {
            get { return Id; }
            set { Id = value; }
        }

        private bool permission;
          
        public bool adminPermission 
        {
            get { return permission; }
            set { permission = value; }
        }

        /* OVERRIDE TOSTRING */
        public override string ToString()
        {
            return "User details: \n Username: " + userName + " \n Permission: " + adminPermission.ToString();
        }

    }
}
