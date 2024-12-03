namespace CAR_RENTAL_SYSTEM.Model
{
    public class Lease
    {

        //public int LeaseID { get; set; }
        //public string VehicleID { get; set; }
        //public int CustomerID { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public string LeaseType { get; set; }

        private int _leaseID;
        private string _vehicleID;
        private int _customerID;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _leaseType;

        // Properties with getters and setters
        public int LeaseID
        {
            get { return _leaseID; }
            set { _leaseID = value; }
        }

        public string VehicleID
        {
            get { return _vehicleID; }
            set { _vehicleID = value; }
        }

        public int CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public string LeaseType
        {
            get { return _leaseType; }
            set { _leaseType = value; }
        }


        // Parameterized Constructor
        public Lease(int leaseID, string vehicleID, int customerID, DateTime startDate, DateTime endDate, string leaseType)
        {
            LeaseID = leaseID;
            VehicleID = vehicleID;
            CustomerID = customerID;
            StartDate = startDate;
            EndDate = endDate;
            LeaseType = leaseType;
        }

        // Default Constructor
        public Lease() { }
    }
}
