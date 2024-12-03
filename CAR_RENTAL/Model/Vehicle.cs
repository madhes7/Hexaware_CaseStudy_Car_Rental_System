namespace CAR_RENTAL_SYSTEM.Model
{
    public class Vehicle
    {
        private string _vehicleID;
        private string _make;
        private string _model;
        private int _year;
        private decimal _dailyRate;
        private string _status;
        private int _passengerCapacity;
        private int _engineCapacity;
        // Properties with getters and setters
        public string VehicleID
        {
            get { return _vehicleID; }
            set { _vehicleID = value; }
        }

        public string Make
        {
            get { return _make; }
            set { _make = value; }
        }

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public decimal DailyRate
        {
            get { return _dailyRate; }
            set { _dailyRate = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int PassengerCapacity
        {
            get { return _passengerCapacity; }
            set { _passengerCapacity = value; }
        }

        public int EngineCapacity
        {
            get { return _engineCapacity; }
            set { _engineCapacity = value; }
        }


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
