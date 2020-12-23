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

        private bool permission;
         
        public bool adminPermission 
        {
            get { return permission; }
            set { permission = value; }
        }

        public User ()
        {
            userName = "Unknown";
            password = "Unknown";
            permission = false;
        }

        public User (string u, string p, bool perm)
        {
            userName = u;
            password = p;
            permission = perm;
        }


        public override string ToString()
        {
            return "User details: \n Username: " + userName + " \n Permission: " + adminPermission.ToString();
        }


    }
}
