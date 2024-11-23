using CAR_RENTAL_SYSTEM.Exception;
using CAR_RENTAL_SYSTEM.Model;
using CAR_RENTAL_SYSTEM.Utility;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Repository
{
    internal class CarLeaseRepository : ICarLeaseRepository
    {
        // Vehicle Management

        private string _connectionstring;
        public CarLeaseRepository()
        {
            _connectionstring = DBConnection.GetConnection();
        }
        public void AddVehicle(Vehicle vehicle)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                string query = @"INSERT INTO Vehicle (vehicleID, make, model, [year], dailyRate, [status], passengerCapacity, engineCapacity) 
                                 VALUES (@VehicleID, @Make, @Model, @Year, @DailyRate, @Status, @PassengerCapacity, @EngineCapacity)";
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@VehicleID", vehicle.VehicleID);
                command.Parameters.AddWithValue("@Make", vehicle.Make);
                command.Parameters.AddWithValue("@Model", vehicle.Model);
                command.Parameters.AddWithValue("@Year", vehicle.Year);
                command.Parameters.AddWithValue("@DailyRate", vehicle.DailyRate);
                command.Parameters.AddWithValue("@Status", vehicle.Status);
                command.Parameters.AddWithValue("@PassengerCapacity", vehicle.PassengerCapacity);
                command.Parameters.AddWithValue("@EngineCapacity", vehicle.EngineCapacity);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
           
                using (SqlConnection connection = new SqlConnection(_connectionstring))
                {

                    string query = "DELETE FROM Vehicle WHERE vehicleID = @VehicleID";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VehicleID", vehicle.VehicleID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
           
        

        public List<Vehicle> GetVehicles()
        {
            var vehicles = new List<Vehicle>();
            using (SqlConnection connection = new SqlConnection(_connectionstring)) 
            {
                string query = "SELECT * FROM Vehicle";
                var command = new SqlCommand(query, connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vehicles.Add(new Vehicle
                        {
                            VehicleID = reader["vehicleID"].ToString(),
                            Make = reader["make"].ToString(),
                            Model = reader["model"].ToString(),
                            Year = (int)reader["year"],
                            DailyRate = (decimal)reader["dailyRate"],
                            Status = reader["status"].ToString(),
                            PassengerCapacity = (int)reader["passengerCapacity"],
                            EngineCapacity = (int)reader["engineCapacity"]
                        });
                    }
                }
            }
            return vehicles;
        }

        public List<Vehicle> listRentedVehicle()
        {
            var vehicles = new List<Vehicle>();
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                string query = "SELECT * FROM Vehicle WHERE [status] = 'notAvailable'";
                var command = new SqlCommand(query, connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vehicles.Add(new Vehicle
                        {
                            VehicleID = reader["vehicleID"].ToString(),
                            Make = reader["make"].ToString(),
                            Model = reader["model"].ToString(),
                            Year = (int)reader["year"],
                            DailyRate = (decimal)reader["dailyRate"],
                            Status = reader["status"].ToString(),
                            PassengerCapacity = (int)reader["passengerCapacity"],
                            EngineCapacity = (int)reader["engineCapacity"]
                        });
                    }
                }
            }
            return vehicles;
        }

        public Vehicle GetVehicle(string vehicleId)

        {
            
                using (SqlConnection connection = new SqlConnection(_connectionstring))
                {
                    string query = "SELECT * FROM Vehicle WHERE vehicleID = @VehicleID";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@VehicleID", vehicleId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Vehicle
                            {
                                VehicleID = reader["vehicleID"].ToString(),
                                Make = reader["make"].ToString(),
                                Model = reader["model"].ToString(),
                                Year = (int)reader["year"],
                                DailyRate = (decimal)reader["dailyRate"],
                                Status = reader["status"].ToString(),
                                PassengerCapacity = (int)reader["passengerCapacity"],
                                EngineCapacity = (int)reader["engineCapacity"]
                            };
                        }
                        else
                        {
                        return null;
                        }

                    }
                }
            
           
        }
                    
                
            
        

        // Customer Management
        public void addCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                string query = @"INSERT INTO Customer (firstName, lastName, email, phoneNumber) 
                                 VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";
                var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void removeCustomer(int customerID)
        {
           
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                string query = "DELETE FROM Customer WHERE customerID = @CustomerID";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customerID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Customer> listCustomers()
        {
            var customers = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                string query = "SELECT * FROM Customer";
                var command = new SqlCommand(query, connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerID = (int)reader["customerID"],
                            FirstName = reader["firstName"].ToString(),
                            LastName = reader["lastName"].ToString(),
                            Email = reader["email"].ToString(),
                            PhoneNumber = reader["phoneNumber"].ToString()
                        });
                    }
                }
            }
            return customers;
        }

        public Customer findCustomerById(int customerID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                string query = "SELECT * FROM Customer WHERE customerID = @CustomerID";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customerID);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Customer
                        {
                            CustomerID = (int)reader["customerID"],
                            FirstName = reader["firstName"].ToString(),
                            LastName = reader["lastName"].ToString(),
                            Email = reader["email"].ToString(),
                            PhoneNumber = reader["phoneNumber"].ToString()
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        // Lease Management
        public Lease createLease(int customerID, string carID, DateTime startDate, DateTime endDate)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                string query = @"INSERT INTO Lease (customerID, vehicleID, startDate, endDate, type) 
                                 VALUES (@CustomerID, @VehicleID, @StartDate, @EndDate, 'DailyLease'); 
                                 SELECT SCOPE_IDENTITY();";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customerID);
                command.Parameters.AddWithValue("@VehicleID", carID);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                connection.Open();
                int leaseID = Convert.ToInt32(command.ExecuteScalar());

                return new Lease
                {
                    LeaseID = leaseID,
                    CustomerID = customerID,
                    VehicleID = carID.ToString(),
                    StartDate = startDate,
                    EndDate = endDate,
                    Type = "DailyLease"
                };
            }
        }
        public void returnCar(int leaseID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                string updateLeaseQuery = @"UPDATE Lease SET endDate = GETDATE() WHERE leaseID = @LeaseID;
                                    SELECT l.*, v.* FROM Lease l
                                    JOIN Vehicle v ON l.vehicleID = v.vehicleID
                                    WHERE l.leaseID = @LeaseID;";

                var command = new SqlCommand(updateLeaseQuery, connection);
                command.Parameters.AddWithValue("@LeaseID", leaseID);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        // First query has been read and processed, now execute the second command
                        string updateVehicleQuery = "UPDATE Vehicle SET [status] = 'available' WHERE vehicleID = @VehicleID";
                        var vehicleCommand = new SqlCommand(updateVehicleQuery, connection);
                        vehicleCommand.Parameters.AddWithValue("@VehicleID", reader["vehicleID"]);

                        reader.Close();
                        // Execute the vehicle update
                        vehicleCommand.ExecuteNonQuery();

                       
                    }
                    else
                    {
                        throw new LeaseNotFoundException("Lease not found.");
                    }
                    
                    
                }
            }
        }

        public List<Lease> listActiveLeases()
        {
            var activeLeases = new List<Lease>();
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                string query = "SELECT * FROM Lease WHERE endDate >= GETDATE()";
                var command = new SqlCommand(query, connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        activeLeases.Add(new Lease
                        {
                            LeaseID = (int)reader["leaseID"],
                            VehicleID = reader["vehicleID"].ToString(),
                            CustomerID = (int)reader["customerID"],
                            StartDate = (DateTime)reader["startDate"],
                            EndDate = (DateTime)reader["endDate"],
                            Type = reader["type"].ToString()
                        });
                    }
                }
            }
            return activeLeases;
        }
        public List<Lease> listLeaseHistory()
        {
            var leaseHistory = new List<Lease>();
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                string query = "SELECT * FROM Lease WHERE endDate < GETDATE()";
                var command = new SqlCommand(query, connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        leaseHistory.Add(new Lease
                        {
                            LeaseID = (int)reader["leaseID"],
                            VehicleID = reader["vehicleID"].ToString(),
                            CustomerID = (int)reader["customerID"],
                            StartDate = (DateTime)reader["startDate"],
                            EndDate = (DateTime)reader["endDate"],
                            Type = reader["type"].ToString()
                        });
                    }
                }
            }
            return leaseHistory;
        }
        public void recordPayment(int leaseID)
        {
            decimal dec = CalculateLeaseAmount(leaseID);
            Console.WriteLine($"The payment amount is ::{dec} \n type 1 to make payment");
                if (int.Parse(Console.ReadLine()) != 1)
            {
                Console.WriteLine( "Payment cancled");
            }
            else {
                using (SqlConnection connection = new SqlConnection(_connectionstring))
                {
                    string query = @"INSERT INTO Payment (leaseID, paymentDate, amount) 
                         VALUES (@LeaseID, GETDATE(), @Amount)";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LeaseID", leaseID);



                    command.Parameters.AddWithValue("@Amount", dec);

                    connection.Open();
                    command.ExecuteNonQuery();
                } }
        }
        public decimal CalculateLeaseAmount(int leaseID)
        {
            
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                string query = @"SELECT l.startDate, l.endDate,l.type, v.dailyRate
                         FROM Lease l
                         JOIN Vehicle v ON l.vehicleID = v.vehicleID
                         WHERE l.leaseID = @LeaseID";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LeaseID", leaseID);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime startDate = (DateTime)reader["startDate"];
                        DateTime endDate = (DateTime)reader["endDate"];
                        decimal dailyRate = (decimal)reader["dailyRate"];
                        string type = reader["type"].ToString();
                        decimal amount = 0;

                        if (type.Equals("dailylease", StringComparison.OrdinalIgnoreCase))
                        {

                            int days = (endDate - startDate).Days;
                            
                            amount = days * dailyRate;
                        }
                        else if (type.Equals("monthlylease", StringComparison.OrdinalIgnoreCase))
                        {
                            int months = ((endDate.Year - startDate.Year) * 12) + endDate.Month - startDate.Month;
                            
                            amount = months * (dailyRate * 30); // Approximation for monthly rate
                        }

                       
                        return amount;
                    }
                    else
                    {
                        throw new LeaseNotFoundException("Lease not found.");
                    }
                }
            }
        }


    }
}
