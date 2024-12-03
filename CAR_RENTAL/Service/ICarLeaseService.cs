using CAR_RENTAL_SYSTEM.Model;

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
        void ReturnCar();

        // Customer Management Methods
        void AddCustomer();
        void RemoveCustomer();
        void GetAllCustomers();
        void GetCustomerById();

        // Lease Management Methods
        void CreateLease();

        void GetActiveLeases();
        void GetLeaseHistory();
        void ViewLeaseById();

        // Payment Management Method
        void RecordPayment();

        // Lease Amount Calculation Method
        void CalculateLeaseAmount();

    }
}
