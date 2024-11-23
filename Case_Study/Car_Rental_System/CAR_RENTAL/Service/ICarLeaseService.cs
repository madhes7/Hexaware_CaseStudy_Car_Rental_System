using CAR_RENTAL_SYSTEM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Service
{
    internal interface ICarLeaseService
    {
        // Vehicle Management Methods
        void AddVehicle();
        void RemoveVehicle();
       void GetAllVehicles();
        void GetRentedVehicles();
        void GetVehicleById();

        // Customer Management Methods
        void AddCustomer();
        void RemoveCustomer();
        void GetAllCustomers();
        void GetCustomerById();

        // Lease Management Methods
        void CreateLease();
        void ReturnCar();
        void GetActiveLeases();
        void GetLeaseHistory();

        // Payment Management Method
        void RecordPayment();

        // Lease Amount Calculation Method
        void CalculateLeaseAmount();
    }
}
