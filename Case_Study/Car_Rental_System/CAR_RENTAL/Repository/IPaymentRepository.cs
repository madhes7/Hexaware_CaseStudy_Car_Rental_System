using CAR_RENTAL_SYSTEM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Repository
{
    internal interface IPaymentRepository
    {
        void RecordPayment(Lease lease);
        decimal CalculateLeaseAmount(int leaseID);
    }
}
