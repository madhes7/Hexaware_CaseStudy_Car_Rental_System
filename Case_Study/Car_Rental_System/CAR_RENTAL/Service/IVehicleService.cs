using CAR_RENTAL_SYSTEM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Service
{
    internal interface IVehicleService
    {
        void AddVehicle( ); // Add a new vehicle
        void RemoveVehicle( ); // Remove an existing vehicle
        void DisplayVehicles(); // Display available vehicles
        void DisplayRentedVehicles(); // Display vehicles that are currently rented
        void FindVehicle( ); // Find and display details of a specific vehicle
    }
}
