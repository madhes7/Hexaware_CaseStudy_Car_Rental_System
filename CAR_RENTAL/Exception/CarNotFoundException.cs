namespace CAR_RENTAL_SYSTEM.Exception
{
    public class CarNotFoundException : ApplicationException
    {
        public CarNotFoundException(string message) : base(message) { }

    }
}
