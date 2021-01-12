using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiReservationProject.Model;

namespace TaxiReservationProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Passenger passenger = new Passenger("ali", "abuasleh", "ali98", "132");
            Driver driver = new Driver(123, "ali", "abuasleh", "ali98", "132", 10, true);
            Ride ride = new Ride(passenger, "here", "moon");
            ReservationService reservationService = new ReservationService();
            reservationService.addToDriverQueue(driver);
            reservationService.addToPassengerQueue(passenger);
            reservationService.addToRideQueue(ride);
           /* reservationService.printDriverQueue();
            reservationService.printPassengerQueue();
            reservationService.printRideQueue();

            reservationService.matchRide();

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
