using CAR_RENTAL_SYSTEM.Exception;
using CAR_RENTAL_SYSTEM.Model;
using CAR_RENTAL_SYSTEM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Service
{
    internal class CarLeaseService:ICarLeaseService
    {
            private readonly ICarLeaseRepository _carLeaseRepository;
        
        public CarLeaseService(ICarLeaseRepository carLeaseRepository)
            {
                
            _carLeaseRepository = carLeaseRepository;
           
            }

        // Vehicle Management Methods
        public void AddVehicle()
        {
            Console.Write("Enter Vehicle ID: ");
            string vehicleId = Console.ReadLine();
            Console.Write("Enter Make: ");
            string make = Console.ReadLine();
            Console.Write("Enter Model: ");
            string model = Console.ReadLine();
            Console.Write("Enter Year: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Enter Daily Rate: ");
            decimal dailyRate = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Status (available/notAvailable): ");
            string status = Console.ReadLine();
            Console.Write("Enter Passenger Capacity: ");
            int passengerCapacity = int.Parse(Console.ReadLine());
            Console.Write("Enter Engine Capacity: ");
            int engineCapacity = int.Parse(Console.ReadLine());

            Vehicle vehicle = new Vehicle
            {
                VehicleID = vehicleId,
                Make = make,
                Model = model,
                Year = year,
                DailyRate = dailyRate,
                Status = status,
                PassengerCapacity = passengerCapacity,
                EngineCapacity = engineCapacity
            };

            _carLeaseRepository.AddVehicle(vehicle);
            Console.WriteLine("Vehicle added successfully.");
        }

        public void RemoveVehicle()
        {
            try
            {
                Console.Write("Enter Vehicle ID to Remove: ");
                string vehicleId = Console.ReadLine();
                Vehicle vehicle = _carLeaseRepository.GetVehicle(vehicleId);

                if (vehicle != null)
                {
                    _carLeaseRepository.RemoveVehicle(vehicle);
                    Console.WriteLine("Vehicle removed successfully.");
                }
                else
                {
                  throw new CarNotFoundException ("Vehicle not found.");
                }
            }
            catch(CarNotFoundException e)
            {
                
                Console.WriteLine( e.Message);
            }
        }

        public void GetAllVehicles()
        {
            List<Vehicle> vehicles = _carLeaseRepository.GetVehicles();
            Console.WriteLine("All Vehicles:");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"ID: {vehicle.VehicleID}, Make: {vehicle.Make}, Model: {vehicle.Model}, Year: {vehicle.Year}, Status: {vehicle.Status}");
            }
        }

        public void GetRentedVehicles()
        {
            List<Vehicle> rentedVehicles = _carLeaseRepository.listRentedVehicle();
            Console.WriteLine("Rented Vehicles:");
            foreach (var vehicle in rentedVehicles)
            {
                Console.WriteLine($"ID: {vehicle.VehicleID}, Make: {vehicle.Make}, Model: {vehicle.Model}, Status: {vehicle.Status}");
            }
        }

        // Customer Management Methods
        public void AddCustomer()
        {
            Console.Write("Enter Customer First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Customer Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Customer Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Customer Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Customer customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };

            _carLeaseRepository.addCustomer(customer);
            Console.WriteLine("Customer added successfully.");
        }

        public void RemoveCustomer()
        {
            try
            {
                Console.Write("Enter Customer ID to Remove: ");
                int customerID = int.Parse(Console.ReadLine());

                Customer customer = _carLeaseRepository.findCustomerById(customerID);
                if (customer != null)
                {
                    _carLeaseRepository.removeCustomer(customerID);
                    Console.WriteLine("Customer removed successfully.");
                }
                else
                {
                    throw new CustomerNotFoundException("CustomerNotFound      Exception");
                }
            }
            catch(CustomerNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetAllCustomers()
        {
            List<Customer> customers = _carLeaseRepository.listCustomers();
            Console.WriteLine("All Customers:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.CustomerID}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}, Phone: {customer.PhoneNumber}");
            }
        }

        public void GetActiveLeases()
        {
            List<Lease> activeLeases = _carLeaseRepository.listActiveLeases();
            Console.WriteLine("Active Leases:");
            foreach (var lease in activeLeases)
            {
                Console.WriteLine($"Lease ID: {lease.LeaseID}, Vehicle ID: {lease.VehicleID}, Customer ID: {lease.CustomerID}, Start Date: {lease.StartDate}, End Date: {lease.EndDate}");
            }
        }

        public void GetLeaseHistory()
        {
            List<Lease> leaseHistory = _carLeaseRepository.listLeaseHistory();
            Console.WriteLine("Lease History:");
            foreach (var lease in leaseHistory)
            {
                Console.WriteLine($"Lease ID: {lease.LeaseID}, Vehicle ID: {lease.VehicleID}, Customer ID: {lease.CustomerID}, Start Date: {lease.StartDate}, End Date: {lease.EndDate}");
            }
        }

        public void CreateLease()
        {
            Console.Write("Enter Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            Console.Write("Enter Vehicle ID: ");
            string vehicleId = Console.ReadLine();
            Console.Write("Enter Lease Start Date (yyyy-mm-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter Lease End Date (yyyy-mm-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            Lease lease = _carLeaseRepository.createLease(customerId, vehicleId, startDate, endDate);
            Console.WriteLine($"Lease created successfully with Lease ID: {lease.LeaseID}");
        }

        public void ReturnCar()
        {
            try
            {
                Console.Write("Enter Lease ID to return the car: ");
                int leaseId = int.Parse(Console.ReadLine());
                _carLeaseRepository.returnCar(leaseId);
                Console.WriteLine("Car returned successfully.");
            }
            catch(LeaseNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RecordPayment()
        {
            Console.Write("Enter Lease ID: ");
            int leaseId = int.Parse(Console.ReadLine());

            _carLeaseRepository.recordPayment(leaseId);
            Console.WriteLine("Payment recorded successfully.");
        }

        // Lease Amount Calculation Method
        public void CalculateLeaseAmount()
        {
            try
            {
                Console.Write("Enter Lease ID: ");
                int leaseId = int.Parse(Console.ReadLine());


                decimal amount = _carLeaseRepository.CalculateLeaseAmount(leaseId);
                Console.WriteLine($"Lease Amount: {amount}");
            }
            catch (LeaseNotFoundException e) { 
                Console.WriteLine(e.Message);
            }

        }
        public void GetCustomerById()
        {
            try
            {
                Console.Write("Enter Customer ID: ");
                int customerId = int.Parse(Console.ReadLine());

                Customer customer = _carLeaseRepository.findCustomerById(customerId);

                if (customer != null)
                {
                    Console.WriteLine($"Customer Details:");
                    Console.WriteLine($"ID: {customer.CustomerID}");
                    Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                    Console.WriteLine($"Email: {customer.Email}");
                    Console.WriteLine($"Phone: {customer.PhoneNumber}");
                }
                else
                {
                    throw new CustomerNotFoundException("Customer not found.");
                }
            }
            catch(CustomerNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void GetVehicleById()
        {
            try
            {
                Console.Write("Enter Vehicle ID: ");
                string vehicleId = Console.ReadLine();

                Vehicle vehicle = _carLeaseRepository.GetVehicle(vehicleId);

                if (vehicle != null)
                {
                    Console.WriteLine($"Vehicle Details:");
                    Console.WriteLine($"ID: {vehicle.VehicleID}");
                    Console.WriteLine($"Make: {vehicle.Make}");
                    Console.WriteLine($"Model: {vehicle.Model}");
                    Console.WriteLine($"Year: {vehicle.Year}");
                    Console.WriteLine($"Daily Rate: {vehicle.DailyRate}");
                    Console.WriteLine($"Status: {vehicle.Status}");
                    Console.WriteLine($"Passenger Capacity: {vehicle.PassengerCapacity}");
                    Console.WriteLine($"Engine Capacity: {vehicle.EngineCapacity}");
                }
                else
                {
                    throw new CarNotFoundException("Vehicle not found.");
                }
            }
            catch(CarNotFoundException e)
            {
                
                Console.WriteLine(e.Message);
            }
        }

    }
}

