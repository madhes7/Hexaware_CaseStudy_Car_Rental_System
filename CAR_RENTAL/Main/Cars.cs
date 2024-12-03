using CAR_RENTAL_SYSTEM.Model;
using CAR_RENTAL_SYSTEM.Repository;
using CAR_RENTAL_SYSTEM.Service;

namespace CAR_RENTAL_SYSTEM.Main
{
    internal class Cars
    {
        public void run()
        {
            // Instantiate the repository and service layer
            CarLeaseRepository carLeaseRepository = new CarLeaseRepository();
            ICarLeaseService carLeaseService = new CarLeaseService(carLeaseRepository);

            bool continueRunning = true;

            while (continueRunning)
            {
                Console.WriteLine("");
                Console.WriteLine("        ______");
                Console.WriteLine("       /|_||_\\`.__");
                Console.WriteLine("      (   _    _ _\\");
                Console.WriteLine("      =`-(_)--(_)-' ");
                Console.WriteLine("     CAR RENTAL SYSTEM");
                Console.WriteLine();
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
                Console.WriteLine("12. Make & Record Payment");
                Console.WriteLine("13. Display Lease Amount");
                Console.WriteLine("14. View Lease by ID");
                Console.WriteLine("15. View Customer by ID");
                Console.WriteLine("16. View Vehicle by ID");
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
                        carLeaseService.ViewLeaseById();
                        break;
                    case "15":
                        carLeaseService.GetCustomerById();
                        break;
                    case "16":
                        carLeaseService.GetVehicleById();  
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                // Ask the user if they want to continue or exit
                Console.WriteLine("\nDo you want to continue?");
                Console.WriteLine("Press 1 to continue, or 0 to exit.");
                string continueChoice = Console.ReadLine();

                if (continueChoice == "0")
                {
                    Console.WriteLine("Exiting system. Goodbye!");
                    continueRunning = false;
                }
                else if (continueChoice != "1")
                {
                    Console.WriteLine("Invalid input. Exiting system by default.");
                    continueRunning = false;
                }
            }
        }
    }
}
