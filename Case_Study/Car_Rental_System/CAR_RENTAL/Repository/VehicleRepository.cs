using CAR_RENTAL_SYSTEM.Model;
using CAR_RENTAL_SYSTEM.Utility;
using CAR_RENTAL_SYSTEM.Exception;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Repository
{
    internal class VehicleRepository:IVehicleRepository
    {
        private string _connectionstring;
        public VehicleRepository()
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

        public List<Vehicle> ListRentedVehicles()
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
                        throw new LeaseNotFoundException("Vehicle not found.");
                    }
                }
            }
        }

    }
}

