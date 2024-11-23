using CAR_RENTAL_SYSTEM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Repository
{
    internal interface IVehicleRepository
    {
        void AddVehicle(Vehicle vehicle);
        void RemoveVehicle(Vehicle vehicle);
        List<Vehicle> GetVehicles();
        List<Vehicle> ListRentedVehicles();
        Vehicle GetVehicle(string vehicleId);
    }
}
