using CAR_RENTAL_SYSTEM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Service
{
    internal interface IPaymentService
    {
        void RecordPayment(); // Record a payment for a lease
        void CalculateLeaseAmount(); // Calculate and display the total lease amount for a specific lease
    }
}
