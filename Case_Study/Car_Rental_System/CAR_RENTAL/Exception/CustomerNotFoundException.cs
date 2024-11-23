using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAR_RENTAL_SYSTEM.Exception
{
    public class CustomerNotFoundException : ApplicationException
    {
        public CustomerNotFoundException(string message) : base(message) { }
    }
}
