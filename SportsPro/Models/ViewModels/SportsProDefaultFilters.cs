// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

namespace SportsPro.Models
{
    public class SportsProDefaultFilters
    {

        public const string DefaultFilter = "all";

        public string Customer { get; set; } = DefaultFilter;
        public string Product { get; set; } = DefaultFilter;
        public string Technician { get; set; } = DefaultFilter;

        public string Country { get; set; } = DefaultFilter;

    }
}
