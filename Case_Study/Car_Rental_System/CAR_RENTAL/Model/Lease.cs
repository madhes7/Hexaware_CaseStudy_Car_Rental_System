using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Model
{
    internal class Lease
    {
        
        public int LeaseID { get; set; }
        public string VehicleID { get; set; }
        public int CustomerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }

        // Parameterized Constructor
        public Lease(int leaseID, string vehicleID, int customerID, DateTime startDate, DateTime endDate, string type)
        {
            LeaseID = leaseID;
            VehicleID = vehicleID;
            CustomerID = customerID;
            StartDate = startDate;
            EndDate = endDate;
            Type = type;
        }

        // Default Constructor
        public Lease() { }
    }
}
