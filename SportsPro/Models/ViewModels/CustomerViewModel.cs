// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Models
{
    public class CustomerViewModel
    {
        public List<Registration> Registrations { get; set; }
        public List<Product> Products { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }

        public int CustomerID { get; set; }
        public int ProductID { get; set; }

    }
}
