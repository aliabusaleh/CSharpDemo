using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiReservationProject.Model
{
    enum infoData { STATUS, AMOUNT, DRIVER, PASSENGER, SRC, DST };

    class ReservationService
    {
        public List<Ride> RideQueue;
        public List<Driver> DriverQueue;// 
        public  List<Passenger> PassengerQueue; // waiting queue
        public ReservationService() {
            RideQueue = new List<Ride>();
            DriverQueue = new List<Driver>();
            PassengerQueue = new List<Passenger>();
        
        }

        public bool addToRideQueue(Ride ride)
        {
            try
            {
                RideQueue.Add(ride);
                Notify();
                return true;
            }
            catch (Exception _e)
            {
                Console.WriteLine("Error While adding to Ride Queue");
            }
            return false;
        }

        private void Notify()
        {
            Console.WriteLine("Notifying My Clients!");
            foreach(Driver driver in DriverQueue)
            {
                if(driver.Avilabe) driver.Update();
            }
        }

        public bool addToPassengerQueue(Passenger passenger)
        {
            try
            {
                PassengerQueue.Add(passenger);
                return true;
            }
            catch (Exception _e)
            {
                Console.WriteLine("Error While adding to Passenger Queue");
            }
            return false;
        }
        public bool addToDriverQueue(Driver  driver)
        {
            try
            {
                DriverQueue.Add(driver);
                return true;
            }
            catch (Exception _e)
            {
                Console.WriteLine("Error While adding to Driver Queue");
            }
            return false;
        }
        public string getRideInfo(int id, infoData type)
        {
            foreach(Ride item in RideQueue)
            {
                if(item.RideID == id)
                {
                    switch (type)
                    {
                        case infoData.STATUS:
                            return item.RideStatus.ToString();
                        case infoData.AMOUNT:
                            return item.TotalAmount.ToString();
                        case infoData.DRIVER:
                            return item.RideDriver.FirstName;
                        case infoData.PASSENGER:
                            return item.RidePassenger.FirstName;
                        case infoData.SRC:
                            return item.RideSrc;
                        case infoData.DST:
                            return item.RideDst;
                        default:
                            return "Unknow Option!";
                    }
                }
            }
            return "Unknow Ride ID";
        }

        public bool StartRide(int id)
        {
            foreach(Ride item in RideQueue)
            {
                if(item.RideID == id)
                {
                    item.RideStatus = RideStatusValues.UNDERPROGRESS;
                    return true;
                }
            }
            return false;
        }
        public void stopRide(int id)
        {
            foreach(Ride item in RideQueue)
            {
                if(item.RideID == id)
                {
                    item.RideStatus = RideStatusValues.FINISHED;
                    item.RideDriver.Avilabe = true;
                    return;
                }
            }
        }

        public bool deleteFromDriverQueue(int id)
        {
            foreach(Driver driver in DriverQueue)
            {
                if(driver.UserID == id)
                {
                    DriverQueue.Remove(driver);
                    return true;
                }
            }
            return false;
        }

        public bool deleteFromPassengerQueue(int id)
        {
            foreach (Passenger passenger in PassengerQueue)
            {
                if (passenger.UserID == id)
                {
                    PassengerQueue.Remove(passenger);
                    return true;
                }
            }
            return false;
        }

        public bool deleteFromRideQueue(int id)
        {
            foreach (Ride ride in RideQueue)
            {
                if (ride.RideID == id)
                {
                    RideQueue.Remove(ride);
                    return true;
                }
            }
            return false;
        }

        public bool emptyDriverQueue()
        {
            return (DriverQueue.Count() == 0);
        }

        public bool emptyPassengerQueue()
        {
            return (PassengerQueue.Count() == 0);
        }

        public bool emptyRideQueue()
        {
            return (RideQueue.Count() == 0);
        }

        public void matchRide()
        {
            if (emptyDriverQueue() || emptyRideQueue()) return;
            Ride ride = null ;
            foreach(Ride item in RideQueue)
            {
                if (!item.HasDriver)
                {
                    ride = item;
                    break;
                }
            }
            if (ride == null) return;

            Driver driver = null;
            foreach(Driver item in DriverQueue)
            {
                if (item.Avilabe)
                {
                    driver = item;
                    break;
                }
            }
            if (driver == null) return;

            ride.RideDriver = driver;
            driver.Avilabe= false;
            ride.RideStatus = RideStatusValues.UNDERPROGRESS;
            ride.HasDriver = true;
            return;

        }

        public void printPassengerQueue()
        {
            Console.WriteLine("Passenger List: ");
            foreach(Passenger passenger in PassengerQueue)
            {
                Console.WriteLine("ID: " + passenger.UserID.ToString() + " , FirstName: " + passenger.FirstName);
            }
            Console.WriteLine("-----------------------------------------------");
        }

        public void printDriverQueue()
        {
            Console.WriteLine("Driver List: ");
            foreach (Driver driver in DriverQueue)
            {
                Console.WriteLine("ID: " + driver.UserID.ToString() + " , FirstName: " + driver.FirstName +" Avilable :" + (driver.Avilabe ? "Avilable" : "Working"));
            }
            Console.WriteLine("-----------------------------------------------");
        }

        public void printRideQueue()
        {
            Console.WriteLine("Ride List: ");
            foreach (Ride ride in RideQueue)
            {
                if(ride.RideDriver != null)
                Console.WriteLine("ID: " + ride.RideID.ToString() + " , Driver: " + ride.RideDriver.FirstName + " , Passenger: "+ ride.RidePassenger.FirstName +" ,Status: "+ ride.RideStatus);
                else
                    Console.WriteLine("ID: " + ride.RideID.ToString() + " , Driver: No Driver yet , Passenger: " + ride.RidePassenger.FirstName+" ,Status: "+ ride.RideStatus);
            }
            Console.WriteLine("-----------------------------------------------");
        }


    }

}
