using CAR_RENTAL_SYSTEM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Repository
{
    internal interface ILeaseRepository
    {
        Lease CreateLease(int customerID, string carID, DateTime startDate, DateTime endDate);
        void ReturnCar(int leaseID);
        List<Lease> ListActiveLeases();
        List<Lease> ListLeaseHistory();
    }
}
