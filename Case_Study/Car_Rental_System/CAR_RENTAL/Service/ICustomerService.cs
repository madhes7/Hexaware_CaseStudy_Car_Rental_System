using CAR_RENTAL_SYSTEM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Service
{
    internal interface ICustomerService
    {
        void AddCustomer( ); // Add a new customer
        void RemoveCustomer( ); // Remove an existing customer
        void DisplayCustomers(); // Display a list of all customers
        void FindCustomerById( ); // Find and display details of a specific customer
    }
}
