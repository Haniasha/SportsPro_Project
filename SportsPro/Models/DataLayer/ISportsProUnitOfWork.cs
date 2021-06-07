// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

namespace SportsPro.Models
{
    public interface ISportsProUnitOfWork
    {
        public Repository<Country> Countries { get; }
        public Repository<Customer> Customers { get; }
        public Repository<Incident> Incidents { get; }
        public Repository<Product> Products { get; }
        public Repository<Registration> Registrations { get; }
        public Repository<Technician> Technicians { get; }

        public void Save();
    }
}
