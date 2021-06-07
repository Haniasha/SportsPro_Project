// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using System.Linq;
using Microsoft.AspNetCore.Mvc; 
namespace SportsPro.Models
{
    public class CustomerDropDown : ViewComponent
    {
        private IRepository<Customer> data { get; set; }
        public CustomerDropDown(IRepository<Customer> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var customers = data.List(new QueryOptions<Customer> {
                OrderBy = a => a.FirstName
            });
          
            var vm = new DropDownViewModel {                
                SelectedValue = selectedValue,
                DefaultValue = SportsProDefaultFilters.DefaultFilter,
                DefaultText = "Select a customer ...",
                Items = customers.ToDictionary(
                    a => a.CustomerID.ToString(), a => a.FullName)
            };

            return View(SharedPath.Select, vm);
        }
    }
    public class ProductDropDown : ViewComponent
    {
        private IRepository<Product> data { get; set; }
        public ProductDropDown(IRepository<Product> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var products = data.List(new QueryOptions<Product>
            {
                OrderBy = a => a.Name
            });

            var vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = SportsProDefaultFilters.DefaultFilter,
                DefaultText = "Select a Product ...",
                Items = products.ToDictionary(
                    a => a.ProductID.ToString(), a => a.Name)
            };

            return View(SharedPath.Select, vm);
        }
    }

    public class TechnicianDropDown : ViewComponent
    {
        private IRepository<Technician> data { get; set; }
        public TechnicianDropDown(IRepository<Technician> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var technicians = data.List(new QueryOptions<Technician>
            {
                OrderBy = a => a.Name
            });

            var vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = SportsProDefaultFilters.DefaultFilter,
                DefaultText = "Select a Technician ...",
                Items = technicians.ToDictionary(
                    a => a.TechnicianID.ToString(), a => a.Name)
            };

            return View(SharedPath.Select, vm);
        }
    }
    public class CountryDropDown : ViewComponent
    {
        private IRepository<Country> data { get; set; }
        public CountryDropDown(IRepository<Country> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var country = data.List(new QueryOptions<Country>
            {
                OrderBy = a => a.Name
            });

            var vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = SportsProDefaultFilters.DefaultFilter,
                DefaultText = "Select a Country ...",
                Items = country.ToDictionary(
                    a => a.CountryID.ToString(), a => a.Name)
            };

            return View(SharedPath.Select, vm);
        }
    }
}
