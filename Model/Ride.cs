using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiReservationProject.Model
{
	enum RideStatusValues { NEW, FINISHED, UNDERPROGRESS };

	class Ride
    {
		public int RideID { get; set; }
		public int TotalAmount { get; set; }
		public string RideSrc { get; set; }
		public string RideDst { get; set; }
		public RideStatusValues RideStatus { get; set; }
		public bool HasDriver { get; set; }
		public Driver RideDriver { get; set; }
		public Passenger RidePassenger { get; set; }
		public Ride(Passenger passenger, string src, string dst, RideStatusValues status =RideStatusValues.NEW)
        {
			RidePassenger = passenger;
			RideSrc = src;
			RideDst = dst;
			RideStatus = status;
        }


		static int generateRideId()
		{
			int id = 1;   //This resets the value to 1 every time you enter.
			return id += 1;
		}
	}
}
