using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiReservationProject.Model;
using System.Web.Http;
using System.Net;

namespace TaxiReservationProject.Controller
{
    public class DriverController : ApiController
    {
        Driver[] drivers = new Driver[]
        {
            new Driver (  123,  "Tom Qurqman",  "Groceries", "TomSmith", "Mypassword" ),
             new Driver (  1234,  "Tom Qurqman2",  "Groceries2", "TomSmith2", "Mypassword2" ),
             new Driver (  1235,  "Tom Qurqman4",  "Groceries4", "TomSmith4", "Mypassword4" ),
        };

        public IEnumerable<Driver> GetAllDrivers()
        {
            return drivers;
        }
        [HttpGet]
        public Driver GetDriverById(int id)
        {
            var driver = drivers.FirstOrDefault((p) => p.LicencsID == id);
            if (driver == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return driver;
        }

        public IEnumerable<Driver> GetProductsByCategory(string LastName)
        {
            return drivers.Where(p => string.Equals(p.LastName, LastName,
                    StringComparison.OrdinalIgnoreCase));
        }
        [HttpPost]
        public Driver PostDriver(Driver driver)
        {
            if(driver != null)
            {
                drivers.Append(driver);
                return driver;
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}
