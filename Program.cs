using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Timers;
using System.Web.Http;
using System.Web.Http.SelfHost;
using TaxiReservationProject.Model;

namespace TaxiReservationProject
{
    class Program
    {
        private static Timer aTimer;
     
        static void Main(string[] args)
        {
           /* var config = new HttpSelfHostConfiguration("http://localhost:8080");

            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }*/



            Passenger passenger = new Passenger("ali", "abuasleh", "ali98", "132");
            Driver driver = new Driver(123, "ali", "abuasleh", "ali98", "132", 10, true);
            Driver driver2 = new Driver(123, "test2", "abuasleh", "ali98", "132", 10, true);
            Ride ride = new Ride(passenger, "here", "moon");
            Ride ride2 = new Ride(passenger, "here", "moon");
            Ride ride3 = new Ride(passenger, "here", "moon");
            ReservationService reservationService = new ReservationService();
            reservationService.addToDriverQueue(driver);
            reservationService.addToDriverQueue(driver2);
            reservationService.addToPassengerQueue(passenger);
            
            reservationService.NewRide += driver.Notifyme;
            reservationService.NewRide += driver2.Notifyme;
            Console.WriteLine("First Ride to Add");
            reservationService.addToRideQueue(ride);
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("matching ride !");
            

            // this code for running the matchRide() function periodically 
            aTimer = new System.Timers.Timer(2000);
            aTimer.Elapsed += reservationService.matchRide;
            aTimer.Elapsed += reservationService.DeleteFinished;
            aTimer.Enabled = true;
            


            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("ride 1 matched, add new Ride !");
            reservationService.addToRideQueue(ride2);
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("stop ride 1, add ride 3!");
            reservationService.addToRideQueue(ride3);
            reservationService.stopRide(ride.RideID);
            Console.ReadKey();
        }
    }
    
}
