using CAR_RENTAL_SYSTEM.Exception;
using CAR_RENTAL_SYSTEM.Model;
using CAR_RENTAL_SYSTEM.Utility;
using System.Data.SqlClient;
using System.Transactions;

namespace CAR_RENTAL_SYSTEM.Repository
{
    public class CarLeaseRepository : ICarLeaseRepository
    {
        // Vehicle Management
       
        string _connectionstring;
        public CarLeaseRepository()
        {
            _connectionstring = DBConnection.GetConnection();
         
        }

       



        public int AddVehicle(Vehicle vehicle)
        {
            try
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
                    return 1;
                }
            }
            catch (CarNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
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

        public Vehicle GetVehicleById(string vehicleId)

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
                // Insert the customer and fetch the generated CustomerID using SCOPE_IDENTITY()
                string query = @"INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber) 
                         VALUES (@FirstName, @LastName, @Email, @PhoneNumber);
                         SELECT CAST(SCOPE_IDENTITY() AS INT);";  // Fetch the new identity value
                var command = new SqlCommand(query, connection);

                // Add the parameter values
                command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                command.Parameters.AddWithValue("@LastName", customer.LastName);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

                connection.Open();

                // Execute the query and get the newly inserted CustomerID
                var customerId = (int)command.ExecuteScalar();

                // Display the generated CustomerID
                Console.WriteLine("Your Customer ID: " + customerId);
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

        public Customer GetCustomerById(int customerID)
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

        //Lease Management
        public Lease createLease(int customerID, string carID, DateTime startDate, DateTime endDate, string leaseType)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Check if the vehicle is available
                    string checkVehicleQuery = @"SELECT [status] FROM Vehicle WHERE vehicleID = @VehicleID";
                    var checkVehicleCommand = new SqlCommand(checkVehicleQuery, connection, transaction);
                    checkVehicleCommand.Parameters.AddWithValue("@VehicleID", carID);

                    string vehicleStatus = (string)checkVehicleCommand.ExecuteScalar();

                    if (vehicleStatus != "available")
                    {
                        throw new CarNotFoundException("The vehicle is not available for lease.");
                    }

                    // Insert lease and get the lease ID
                    string leaseQuery = @"INSERT INTO Lease (customerID, vehicleID, startDate, endDate, [type]) 
                                  VALUES (@CustomerID, @VehicleID, @StartDate, @EndDate, @LeaseType); 
                                  SELECT SCOPE_IDENTITY();"
                    ;

                    var leaseCommand = new SqlCommand(leaseQuery, connection, transaction);
                    leaseCommand.Parameters.AddWithValue("@CustomerID", customerID);
                    leaseCommand.Parameters.AddWithValue("@VehicleID", carID);
                    leaseCommand.Parameters.AddWithValue("@StartDate", startDate);
                    leaseCommand.Parameters.AddWithValue("@EndDate", endDate);
                    leaseCommand.Parameters.AddWithValue("@LeaseType", leaseType);

                    int leaseID = Convert.ToInt32(leaseCommand.ExecuteScalar());

                    // Update the vehicle status to 'notAvailable'
                    string updateVehicleQuery = @"UPDATE Vehicle 
                                          SET [status] = 'notAvailable' 
                                          WHERE vehicleID = @VehicleID"
                    ;

                    var updateVehicleCommand = new SqlCommand(updateVehicleQuery, connection, transaction);
                    updateVehicleCommand.Parameters.AddWithValue("@VehicleID", carID);
                    updateVehicleCommand.ExecuteNonQuery();

                    //Commit the transaction
                    transaction.Commit();

                    // Calculate lease amount (assuming you have a method for this)
                    decimal leaseAmount = CalculateLeaseAmount(leaseID);
                    Console.WriteLine($"Lease Amount = {leaseAmount}");

                    // Return the created lease object
                    return new Lease
                    {
                        LeaseID = leaseID,
                        CustomerID = customerID,
                        VehicleID = carID,
                        StartDate = startDate,
                        EndDate = endDate,
                        LeaseType = leaseType
                    };

                }
                catch (CarNotFoundException ex)
                {
                    // Rollback the transaction in case of error
                    transaction.Rollback();
                    throw new CarNotFoundException("Error creating lease 65: " + ex.Message);
                }
            }
        }




        public int ReturnCar(int leaseID)
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
                        string updateVehicleQuery = "UPDATE Vehicle SET [status] = 'available' WHERE vehicleID = @VehicleID";
                        var vehicleCommand = new SqlCommand(updateVehicleQuery, connection);
                        vehicleCommand.Parameters.AddWithValue("@VehicleID", reader["vehicleID"]);

                        reader.Close();
                        vehicleCommand.ExecuteNonQuery();
                        return 1; // Success
                    }
                    else
                    {
                        throw new LeaseNotFoundException("Lease not found.");// Exception if no record found
                        return 0;
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
                            LeaseType = reader["type"].ToString()
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
                            LeaseType = reader["type"].ToString()
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


        
        public Lease ViewLeaseById(int leaseID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                string query = "SELECT * FROM Lease WHERE leaseID = @LeaseID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeaseID", leaseID);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Assuming you have a Lease class with appropriate properties
                            return new Lease
                            {
                                LeaseID = (int)reader["leaseID"],
                                CustomerID = (int)reader["customerID"],
                                VehicleID = (string)reader["vehicleID"].ToString(),
                                StartDate = (DateTime)reader["startDate"],
                                EndDate = (DateTime)reader["endDate"],
                                LeaseType = reader["type"].ToString()
                            };
                        }
                        else
                        {
                            // No lease found, return null
                            return null;
                        }
                    }
                }
            }
        }


        


    }
}
    

