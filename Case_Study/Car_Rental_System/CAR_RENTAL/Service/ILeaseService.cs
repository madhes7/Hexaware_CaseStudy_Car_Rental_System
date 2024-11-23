using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Service
{
    internal interface ILeaseService
    {
        void CreateLease( ); // Create a new lease
        void ReturnCar( ); // Process the return of a rented vehicle
        void DisplayActiveLeases(); // Display all active leases
        void DisplayLeaseHistory(); // Display the history of all leases
    }
}
