using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiReservationProject.Model
{
    public class Person
    {


        public int UserID { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }

       public Person(string firstname = "", string lastname = "" ,string username = "", string password = "")
        {
            UserID = generateId();
            FirstName = firstname;
            LastName = lastname;
            Username = username;
            Password = password;
        }

        static int generateId()
        {
            int id = 1;   //This resets the value to 1 every time you enter.
            return id += 1;
        }
    }
}
