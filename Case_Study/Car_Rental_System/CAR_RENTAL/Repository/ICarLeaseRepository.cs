using CAR_RENTAL_SYSTEM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Repository
{
    internal interface ICarLeaseRepository
    {
        void AddVehicle(Vehicle vehicle);
        void RemoveVehicle(Vehicle vehicle);
        List<Vehicle> GetVehicles();

        List<Vehicle> listRentedVehicle();
        Vehicle GetVehicle(string vehicleId);


        void addCustomer(Customer customer);

        
        void removeCustomer(int customerID);

        
        List<Customer> listCustomers();

        
        Customer findCustomerById(int customerID);
        Lease createLease(int customerID, string carID, DateTime startDate, DateTime endDate);

        void returnCar(int leaseID);

        
        List<Lease> listActiveLeases();


        List<Lease> listLeaseHistory();
        void recordPayment(int leaseID);


        // claculate lease amount
        decimal CalculateLeaseAmount(int leaseID);
    }
}
