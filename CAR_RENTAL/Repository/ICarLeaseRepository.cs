using CAR_RENTAL_SYSTEM.Model;

namespace CAR_RENTAL_SYSTEM.Repository
{
    public interface ICarLeaseRepository
    {
        //Vehicle
        int AddVehicle(Vehicle vehicle);
        void RemoveVehicle(Vehicle vehicle);
        List<Vehicle> GetVehicles();
        List<Vehicle> listRentedVehicle();
        Vehicle GetVehicleById(string vehicleId);
        int ReturnCar(int leaseID);


        //Customer
        void addCustomer(Customer customer);
        void removeCustomer(int customerID);
        List<Customer> listCustomers(); 
        Customer GetCustomerById(int customerID);


        //Lease
        Lease createLease(int customerID, string carID, DateTime startDate, DateTime endDate, string leaseType);
        List<Lease> listActiveLeases();
        List<Lease> listLeaseHistory();
        decimal CalculateLeaseAmount(int leaseID);
        Lease ViewLeaseById(int leaseID);


        //Payment
        void recordPayment(int leaseID);


    }
}
