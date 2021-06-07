// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

namespace SportsPro.Models
{
    public class SportsProUnitOfWork : ISportsProUnitOfWork
    {
        private SportsProContext context { get; set; }
        public SportsProUnitOfWork(SportsProContext ctx) => context = ctx;

        private Repository<Country> countryData;
        public Repository<Country> Countries
        {
            get
            {
                if (countryData == null)
                    countryData = new Repository<Country>(context);
                return countryData;
            }
        }

        private Repository<Customer> customerData;
        public Repository<Customer> Customers
        {
            get
            {
                if (customerData == null)
                    customerData = new Repository<Customer>(context);
                return customerData;
            }
        }

        private Repository<Product> productData;
        public Repository<Product> Products
        {
            get
            {
                if (productData == null)
                    productData = new Repository<Product>(context);
                return productData;
            }
        }

        private Repository<Registration> registrationData;
        public Repository<Registration> Registrations
        {
            get
            {
                if (registrationData == null)
                    registrationData = new Repository<Registration>(context);
                return registrationData;
            }
        }

        private Repository<Incident> incidentsData;
        public Repository<Incident> Incidents
        {
            get
            {
                if (incidentsData == null)
                    incidentsData = new Repository<Incident>(context);
                return incidentsData;
            }
        }

        private Repository<Technician> technicianData;
        public Repository<Technician> Technicians
        {
            get
            {
                if (technicianData == null)
                    technicianData = new Repository<Technician>(context);
                return technicianData;
            }
        }

        public void Save() => context.SaveChanges();
    }
}
