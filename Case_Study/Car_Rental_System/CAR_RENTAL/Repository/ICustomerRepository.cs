using CAR_RENTAL_SYSTEM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Repository
{
    internal interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        void RemoveCustomer(int customerID);
        List<Customer> ListCustomers();
        Customer FindCustomerById(int customerID);
    }
}
