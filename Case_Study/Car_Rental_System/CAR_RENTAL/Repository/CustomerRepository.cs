using CAR_RENTAL_SYSTEM.Utility;
using CAR_RENTAL_SYSTEM.Model;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAR_RENTAL_SYSTEM.Exception;

namespace CAR_RENTAL_SYSTEM.Repository
{
    internal class CustomerRepository:ICustomerRepository
    {
        private string _connectionstring;
        public CustomerRepository()
        {
            _connectionstring = DBConnection.GetConnection();
        }
        
        // Customer Management
        public void AddCustomer(Customer customer)
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

        public void RemoveCustomer(int customerID)
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

        public List<Customer> ListCustomers()
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

        public Customer FindCustomerById(int customerID)
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
                        throw new CustomerNotFoundException("Customer not found.");
                    }
                }
            }
        }
    }
}
