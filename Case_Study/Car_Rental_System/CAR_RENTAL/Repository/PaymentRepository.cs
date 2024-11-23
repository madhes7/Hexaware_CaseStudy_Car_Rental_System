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
    internal class PaymentRepository:IPaymentRepository
    {
        private string _connectionstring;
        public PaymentRepository()
        {
            _connectionstring = DBConnection.GetConnection();
        }
        public void RecordPayment(Lease lease)
        {
            decimal dec = CalculateLeaseAmount(lease.LeaseID);
            Console.WriteLine($"The payment amount is ::{dec} \n type 1 to make payment");
            if (int.Parse(Console.ReadLine()) != 1)
            {
                Console.WriteLine("Payment cancled");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(_connectionstring))
                {
                    string query = @"INSERT INTO Payment (leaseID, paymentDate, amount) 
                         VALUES (@LeaseID, GETDATE(), @Amount)";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@LeaseID", lease.LeaseID);



                    command.Parameters.AddWithValue("@Amount", dec);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
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
