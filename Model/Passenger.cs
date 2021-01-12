using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiReservationProject.Model
{
    class Passenger : Person
    {
        public int Wallet { get; set; }

        public Passenger(string child, string firstname = "", string lastname = "", string username = "", string password = "") : base(firstname, lastname, username, password)
        {
            Wallet = 0;
        }
        void addWalletMoney(int amount)
        {
            Wallet += amount;
        }

    }
}
