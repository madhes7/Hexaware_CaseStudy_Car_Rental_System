using CAR_RENTAL_SYSTEM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Model
{
    internal class Payment
    {
        public int PaymentID { get; set; }
        public int LeaseID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        // Parameterized Constructor
        public Payment(int paymentID, int leaseID, DateTime paymentDate, decimal amount)
        {
            PaymentID = paymentID;
            LeaseID = leaseID;
            PaymentDate = paymentDate;
            Amount = amount;
        }

        // Default Constructor
        public Payment() { }
    }
}

