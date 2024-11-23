using CAR_RENTAL_SYSTEM.Model;
using CAR_RENTAL_SYSTEM.Repository;
using CAR_RENTAL_SYSTEM.Service;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CAR_RENTAL_SYSTEM
{
    public class Main
    {

        public Main()
        {
            // Instantiate the repository and service layer
            ICarLeaseRepository carLeaseRepository = new CarLeaseRepository();
            ICarLeaseService carLeaseService = new CarLeaseService(carLeaseRepository);

            while (true)
            {
                Console.WriteLine("\nCAR RENTAL SYSTEM MENU");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Remove Vehicle");
                Console.WriteLine("3. View All Vehicles");
                Console.WriteLine("4. View Rented Vehicles");
                Console.WriteLine("5. Add Customer");
                Console.WriteLine("6. Remove Customer");
                Console.WriteLine("7. View All Customers");
                Console.WriteLine("8. View Active Leases");
                Console.WriteLine("9. View Lease History");
                Console.WriteLine("10. Create Lease");
                Console.WriteLine("11. Return Car");
                Console.WriteLine("12. Record Payment");
                Console.WriteLine("13. Calculate Lease Amount");
                Console.WriteLine("14. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        carLeaseService.AddVehicle();
                        break;
                    case "2":
                        carLeaseService.RemoveVehicle();
                        break;
                    case "3":
                        carLeaseService.GetAllVehicles();
                        break;
                    case "4":
                        carLeaseService.GetRentedVehicles();
                        break;
                    case "5":
                        carLeaseService.AddCustomer();
                        break;
                    case "6":
                        carLeaseService.RemoveCustomer();
                        break;
                    case "7":
                        carLeaseService.GetAllCustomers();
                        break;
                    case "8":
                        carLeaseService.GetActiveLeases();
                        break;
                    case "9":
                        carLeaseService.GetLeaseHistory();
                        break;
                    case "10":
                        carLeaseService.CreateLease();
                        break;
                    case "11":
                        carLeaseService.ReturnCar();
                        break;
                    case "12":
                        carLeaseService.RecordPayment();
                        break;
                    case "13":
                        carLeaseService.CalculateLeaseAmount();
                        break;
                    case "14":
                        Console.WriteLine("Exiting system. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
    }

