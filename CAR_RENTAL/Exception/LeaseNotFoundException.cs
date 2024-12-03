namespace CAR_RENTAL_SYSTEM.Exception
{
    public class LeaseNotFoundException : ApplicationException
    {
        public LeaseNotFoundException() { }
        
        public LeaseNotFoundException(string message) : base(message) { }
    }
}
