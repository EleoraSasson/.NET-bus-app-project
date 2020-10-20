using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_00_8775_0079
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome8775();
            Welcome0079();
            Console.ReadKey();
        }

        
        private static void Welcome0079()
        {
            Console.WriteLine("I am also here ");

        }
        private static void Welcome8775()
        {
            Console.WriteLine("Enter your name :"); //cout
            string userName = Console.ReadLine(); //cin char* name; cin<<name; 
            Console.WriteLine("{0}, welcome to my first console application", userName); //cout>> name >> "welcome to my first console.."
        }
    }
}
