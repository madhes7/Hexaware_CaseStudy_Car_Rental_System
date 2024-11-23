using CAR_RENTAL_SYSTEM.Exception;
using CAR_RENTAL_SYSTEM.Model;
using CAR_RENTAL_SYSTEM.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Repository
{
    internal class LeaseRepository:ILeaseRepository
    {
        private string _connectionstring;
        public LeaseRepository()
        {
            _connectionstring = DBConnection.GetConnection();
        }

        // Lease Management
        public Lease CreateLease(int customerID, string carID, DateTime startDate, DateTime endDate)
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
        public void ReturnCar(int leaseID)
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

        public List<Lease> ListActiveLeases()
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
        public List<Lease> ListLeaseHistory()
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
    }
}
