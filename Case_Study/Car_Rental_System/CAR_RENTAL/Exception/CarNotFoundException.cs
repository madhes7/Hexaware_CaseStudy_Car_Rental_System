using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Exception
{
    public class CarNotFoundException : ApplicationException
    {
        public CarNotFoundException(string message) : base(message) { }

    }
}
