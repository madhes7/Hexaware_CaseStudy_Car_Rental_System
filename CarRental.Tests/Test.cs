using NUnit.Framework;
using CAR_RENTAL_SYSTEM.Repository;
using CAR_RENTAL_SYSTEM.Model;
using System.Reflection;
using System.Data.SqlClient;
namespace CarRental.Tests
{
    public class Test
    {
        ICarLeaseRepository carLeaseRepository = new CarLeaseRepository();

        string _connectionstring = "Server=ACER_SWIFT;Database=CRSDB;Trusted_Connection=True";
        #region  test for car created successfully or not
        [Test]
        public void TestToAddVehicle() // Add car
        {
            // Arrange
            Vehicle vehicle = new Vehicle
            {
                VehicleID = "V004", // Use a unique ID to test with
                Make = "HindustanMotors",
                Model = "Nova",
                Year = 1995,
                DailyRate = 300,
                Status = "available",
                PassengerCapacity = 5,
                EngineCapacity = 1760
            };

            // Act: Add vehicle to the repository
            int addVehicle = carLeaseRepository.AddVehicle(vehicle);

            // Assert: Check if the vehicle is added successfully
            Assert.That(addVehicle, Is.EqualTo(1));

            Console.WriteLine($"Vehicle {vehicle.VehicleID} added successfully.");
        }

        // Cleanup method: This runs after each test to remove any added data
        [TearDown]
        public void CleanUp()
        {
            string vehicleIDToDelete = "V004"; // Make sure this matches the test vehicle ID

            Console.WriteLine($"Attempting to delete vehicle with ID: {vehicleIDToDelete}");

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                try
                {
                    string deleteQuery = "DELETE FROM Vehicle WHERE vehicleID = @VehicleID";
                    var deleteCommand = new SqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@VehicleID", vehicleIDToDelete);

                    connection.Open();
                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Vehicle deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Vehicle deletion failed. No rows affected.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error during cleanup: " + ex.Message);
                }
            }
        }
        #endregion
        #region test for lease is created successfully or not
        private int createdLeaseID; // Class-level variable to store LeaseID

        [Test]
        public void TestToCreateLease() // Create lease
        {
            // Arrange: Setting up data to create a lease
            int customerID = 2;
            string vehicleID = "V002";  // Assuming this vehicle is available in the database
            DateTime startDate = new DateTime(2025, 12, 1);
            DateTime endDate = new DateTime(2025, 12, 5);
            string leaseType = "DailyLease";

            // Act: Calling the method to create the lease
            Lease createdLease = carLeaseRepository.createLease(customerID, vehicleID, startDate, endDate, leaseType);

            // Assert: Verifying that the lease was created and LeaseID is not 0
            Assert.That(createdLease, Is.Not.Null);
            Assert.That(createdLease.LeaseID, Is.GreaterThan(0));
            Assert.That(createdLease.CustomerID, Is.EqualTo(customerID));
            Assert.That(createdLease.VehicleID, Is.EqualTo(vehicleID));
            Assert.That(createdLease.StartDate, Is.EqualTo(startDate));
            Assert.That(createdLease.EndDate, Is.EqualTo(endDate));
            Assert.That(createdLease.LeaseType, Is.EqualTo(leaseType));

            // Storing LeaseID 
            createdLeaseID = createdLease.LeaseID;

            Console.WriteLine($"Lease {createdLease.LeaseID} created successfully.");
        }

        // Cleanup method: This runs after each test to return the car
        [TearDown]
        public void Return()
        {
            if (createdLeaseID > 0)
            {
                int returnCarResult = carLeaseRepository.ReturnCar(2);
                // Verifying that the car was returned successfully
                Assert.That(returnCarResult, Is.EqualTo(1));
            }
            else
            {
                Console.WriteLine("Car not returned");
            }
        }
        #endregion


        #region test for lease is retrived successfully or not
        [Test]
        public void TestToRetrieveLease()
        {
            // Arrange: Assuming lease ID 2 exists in the database
            int leaseID = 1;

            // Act: Retrieve the lease from the repository
            Lease retrievedLease = carLeaseRepository.ViewLeaseById(leaseID);

            // Assert: Check that the lease is not null, indicating it was retrieved successfully
            Assert.That(retrievedLease, Is.Not.Null);

            // You can further validate the properties of the retrieved lease
            Assert.That(retrievedLease.LeaseID, Is.EqualTo(leaseID));
            Assert.That(retrievedLease.CustomerID, Is.EqualTo(1)); // Example customer ID
            Assert.That(retrievedLease.VehicleID, Is.EqualTo("V001")); // Example vehicle ID
            Assert.That(retrievedLease.LeaseType, Is.EqualTo("DailyLease")); // Example lease type
        }

        [Test]
        public void TestToReturnNullWhenLeaseDoesNotExist()
        {
            // Arrange: Assuming lease ID 999 does not exist
            int leaseID = 999;

            // Act: Try to retrieve the lease from the repository
            Lease retrievedLease = carLeaseRepository.ViewLeaseById(leaseID);

            // Assert: Check that the lease is null, indicating it was not found
            Assert.That(retrievedLease, Is.Null);
        }

        #endregion

        #region Test for returning null when customer ID is not found
        [Test]
        public void TestToReturnNullWhenCustomerIdNotFound()
        {
            // Arrange: Set a customer ID that does not exist in the database
            int customerID = 999; // Assuming this customer does not exist

            // Act: Try to retrieve the customer from the repository
            Customer retrievedCustomer = carLeaseRepository.GetCustomerById(customerID);

            // Assert: Check that the customer is null, indicating it was not found
            Assert.That(retrievedCustomer, Is.Null);
        }
        #endregion

        #region Test for returning null when car ID is not found
        [Test]
        public void TestToReturnNullWhenCarIdNotFound()
        {
            // Arrange: Set a vehicle ID that does not exist in the database
            string vehicleID = "V999"; // Assuming this vehicle does not exist

            // Act: Try to retrieve the vehicle from the repository
            Vehicle retrievedVehicle = carLeaseRepository.GetVehicleById(vehicleID);

            // Assert: Check that the vehicle is null, indicating it was not found
            Assert.That(retrievedVehicle, Is.Null);
        }
        #endregion

        #region Test for returning null when lease ID is not found
        [Test]
        public void TestToReturnNullWhenLeaseIdNotFound()
        {
            // Arrange: Set a lease ID that does not exist in the database
            int leaseID = 999; // Assuming this lease does not exist

            // Act: Try to retrieve the lease from the repository
            Lease retrievedLease = carLeaseRepository.ViewLeaseById(leaseID);

            // Assert: Check that the lease is null, indicating it was not found
            Assert.That(retrievedLease, Is.Null);
        }
        #endregion
    }
}

