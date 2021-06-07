// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Models
{
    public class Registration
    {

        // composite primary key
        public int CustomerID { get; set; }     // foreign key for Customer 
        public int ProductID { get; set; }   // foreign key for Product

        // navigation properties
        public Customer Customer { get; set; }
        public Product Product { get; set; }

    }
}
