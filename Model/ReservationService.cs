using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace TaxiReservationProject.Model
{
    enum infoData { STATUS, AMOUNT, DRIVER, PASSENGER, SRC, DST };


    class ReservationService
    {
        public List<Ride> RideQueue;
        public List<Driver> DriverQueue;// 
        public  List<Passenger> PassengerQueue; // waiting queue
        private static object LOCK = new object();
        public ReservationService() {
            RideQueue = new List<Ride>();
            DriverQueue = new List<Driver>();
            PassengerQueue = new List<Passenger>();
        
        }
        /* notifactaion Service (observer design pattern using Delagate and Events)
         * 1- Define a delegate (signeture of a method in the subsrciber )
         * 2- Event based on the delegate 
         * 3- publish the event
         */
        public delegate void NewRideEventHandler(object source, EventArgs args);
        public event NewRideEventHandler NewRide;

        protected virtual void NotifyDrivers()
        {
            lock (LOCK)
            {
                if (NewRide != null)
                {   
                 NewRide(this, EventArgs.Empty);   
                }
            }
        }

        public bool addToRideQueue(Ride ride)
        {
            lock (LOCK)
            {
                try
                {
                    RideQueue.Add(ride);
                    NotifyDrivers();
                    return true;
                }
                catch (Exception _e)
                {
                    Console.WriteLine("Error While adding to Ride Queue");
                }
                return false;
            }
        }

      

        public bool addToPassengerQueue(Passenger passenger)
        {
            lock (LOCK)
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
        }
        public bool addToDriverQueue(Driver  driver)
        {
            lock (LOCK)
            {
                try
                {
                    driver.Avilabe = true;
                    DriverQueue.Add(driver);
                    return true;
                }
                catch (Exception _e)
                {
                    Console.WriteLine("Error While adding to Driver Queue");
                }
                return false;
            }
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
            lock (LOCK)
            {
                foreach (Ride item in RideQueue)
                {
                    if (item.RideID == id)
                    {
                        item.RideStatus = RideStatusValues.UNDERPROGRESS;
                        return true;
                    }
                }
                return false;
            }
        }
        public async Task stopRide(int id)
        {
            
                await Task.Delay(5 * 1000);
                foreach (Ride item in RideQueue)
                {
                    if (item.RideID == id && item.RideStatus == RideStatusValues.UNDERPROGRESS)
                    {
                        item.RideStatus = RideStatusValues.FINISHED;
                        item.RideDriver.Avilabe = true;
                        this.NewRide += item.RideDriver.Notifyme;
                        return;
                    }
                }
            
        }
        public void DeleteFinished(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Deleteing finished Rides");
            lock (LOCK)
            {
                foreach (Ride item in this.RideQueue)
                {
                    if (item.RideStatus == RideStatusValues.FINISHED)
                    {
                        RideQueue.Remove(item);
                        return;
                    }
                }
            }
        }

        public bool deleteFromDriverQueue(int id)
        {
            lock (LOCK)
            {
                foreach (Driver driver in DriverQueue)
                {
                    if (driver.UserID == id)
                    {
                        DriverQueue.Remove(driver);
                        return true;
                    }
                }
                return false;
            }
        }

        public bool deleteFromPassengerQueue(int id)
        {
            lock (LOCK)
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
        }

        public bool deleteFromRideQueue(int id)
        {
            lock (LOCK)
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
            Console.WriteLine("Matching Rides!!");
            printRideQueue();
            lock (LOCK)
            {
                if (emptyDriverQueue() || emptyRideQueue()) return;
                Ride ride = null;
                foreach (Ride item in RideQueue)
                {
                    if (!item.HasDriver)
                    {
                        ride = item;
                        break;
                    }
                }
                if (ride == null) return;

                Driver driver = null;
                foreach (Driver item in DriverQueue)
                {
                    if (item.Avilabe)
                    {
                        driver = item;
                        break;
                    }
                }
                if (driver == null) return;

                ride.RideDriver = driver;
                driver.Avilabe = false;
                ride.RideStatus = RideStatusValues.UNDERPROGRESS;
                ride.HasDriver = true;
                this.NewRide -= driver.Notifyme;
               
                return;

            }
           
            
        }



        //matchRide Overloading for the timer 
        public void matchRide(object sender, ElapsedEventArgs e)
        {
            this.matchRide();
            Console.WriteLine("inside periodic one");
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
    class StatusChecker
    {
        private int invokeCount;
        private int maxCount;

        public StatusChecker(int count)
        {
            invokeCount = 0;
            maxCount = count;
        }

        // This method is called by the timer delegate.
        public void CheckStatus(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            invokeCount++;

            if (invokeCount == maxCount)
            {
                // Reset the counter and signal the waiting thread.
                invokeCount = 0;
                autoEvent.Set();
            }
        }
    }

}
