using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using TaxiReservationProject.Model;

namespace TaxiReservationProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:8080");

            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
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
            
            reservationService.NewRide += driver.Notify;
            reservationService.NewRide += driver2.Notify;
            Console.WriteLine("First Ride to Add");
            reservationService.addToRideQueue(ride);
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("matching ride !");

            reservationService.matchRide();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("ride 1 matched, add new Ride !");
            reservationService.addToRideQueue(ride2);
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("stop ride 1, add ride 3!");
            reservationService.stopRide(ride.RideID);
            reservationService.addToRideQueue(ride3);
            // reservationService.addToRideQueue(ride2);
            // reservationService.matchRide();

            /*       // Create an AutoResetEvent to signal the timeout threshold in the
                // timer callback has been reached.
            var autoEvent = new AutoResetEvent(false);

                var statusChecker = new StatusChecker(10);

                // Create a timer that invokes CheckStatus after one second, 
                // and every 1/4 second thereafter.
                Console.WriteLine("{0:h:mm:ss.fff} Creating timer.\n",
                                  DateTime.Now);
                var stateTimer = new Timer(statusChecker.CheckStatus,
                                           autoEvent, 1000, 250);

                // When autoEvent signals, change the period to every half second.
                autoEvent.WaitOne();
                stateTimer.Change(0, 500);
                Console.WriteLine("\nChanging period to .5 seconds.\n");

                // When autoEvent signals the second time, dispose of the timer.
                autoEvent.WaitOne();
                stateTimer.Dispose();
                Console.WriteLine("\nDestroying timer.");*/
            /*Thread thread = new Thread(new ThreadStart(reservationService.matchRide));
            thread.IsBackground = true;

            thread.Start();*/

            // reservationService.stopRide(ride.RideID);


            /* reservationService.printDriverQueue();
             reservationService.printPassengerQueue();
             reservationService.printRideQueue();

            

             reservationService.printDriverQueue();
             reservationService.printRideQueue();

             reservationService.StartRide(ride.RideID);
             reservationService.printRideQueue();

             reservationService.stopRide(ride.RideID);
             reservationService.printRideQueue();
             reservationService.printDriverQueue();*/


            Console.ReadKey();
        }
    }
    
}
