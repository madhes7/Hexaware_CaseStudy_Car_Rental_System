namespace CAR_RENTAL_SYSTEM.Exception
{
    public class CustomerNotFoundException : ApplicationException
    {
        public CustomerNotFoundException(string message) : base(message) { }
    }
}
