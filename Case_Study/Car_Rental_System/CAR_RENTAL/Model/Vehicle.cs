using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Model
{
    internal class Vehicle
    {
        public string VehicleID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal DailyRate { get; set; }
        public string Status { get; set; }
        public int PassengerCapacity { get; set; }
        public int EngineCapacity { get; set; }

        // Parameterized Constructor
        public Vehicle(string vehicleID, string make, string model, int year, decimal dailyRate, string status, int passengerCapacity, int engineCapacity)
        {
            VehicleID = vehicleID;
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
            Status = status;
            PassengerCapacity = passengerCapacity;
            EngineCapacity = engineCapacity;
        }

        // Default Constructor
        public Vehicle() { }
    }
}
