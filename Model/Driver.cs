using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiReservationProject;

namespace TaxiReservationProject.Model
{
    public class Driver : Person
    {
       public int LicencsID  {set; get;}
        public int Balance { set; get; }
       public bool Avilabe { set; get; }

      public  Driver(int licencsid, string firstname = "", string lastname = "", string username = "", string password = "", int balance = 0, bool avilable = true) : base(firstname, lastname, username, password)
        {
            LicencsID = licencsid;
            Balance = balance;
            Avilabe = avilable;

        }

        public  void Notify(object source, EventArgs args)
        {
            Console.WriteLine("Hello, I'm " + FirstName + " I've been notified from The Observer!!");
        }
       /* public  void Update()
        {
            Console.WriteLine("Hello, I'm "+FirstName+" I've been notified from The Observer!!");
        }*/
    }
  
}
