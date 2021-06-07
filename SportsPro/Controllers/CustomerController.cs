// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsPro.Models;
using Microsoft.AspNetCore.Authorization;

// <summary>
// Customer Controller for Adding, Editing and Deleting customer table
// This allows to Save new and updated and deleted rows
// </summary>
namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private ISportsProUnitOfWork data { get; set; }
        public CustomerController(ISportsProUnitOfWork unit) => data = unit;

        //Displays the Customer Manager Page
        [Route("customers")]
        public ActionResult Index3()
        {
            var options = new QueryOptions<Customer>
            {
                OrderBy = c => c.FirstName
            };
            var customers = data.Customers.List(options);
            return View(customers);

        }

        //Allows for input of customer data
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            var options = new QueryOptions<Country>
            {
                OrderBy = c => c.Name
            };
            ViewBag.Countries = data.Countries.List(options);
            return View("Edit", new Customer());

        }

        //Displays selected customer data to be updated
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var options = new QueryOptions<Country>
            {
                OrderBy = c => c.Name
            };
            ViewBag.Countries = data.Countries.List(options);
            var customer = data.Customers.Get(id);
            return View(customer);
        }

        //Saves Added or Updated customer data
        //Using same Razor view file to Add and Edit customer data
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
          

            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)           // Checks if ADD or Edit action
                {
                    string message = Check.EmailExists(data.Customers, customer.Email);
                    if (!string.IsNullOrEmpty(message))
                    {
                        ModelState.AddModelError(nameof(customer.Email), message);
                    }

                    data.Customers.Insert(customer);
                }
                else
                {
                    data.Customers.Update(customer);
                }
                data.Save();
                return RedirectToAction("Index3", "Customer");
            }
            else
            {
                ViewBag.Action = (customer.CustomerID == 0) ? "Add" : "Edit";
                var options = new QueryOptions<Country>
                {
                    OrderBy = c => c.Name
                };
                ViewBag.Countries = data.Countries.List(options);      //all Country Names are to be selected
                return View(customer);
            }
        }
        //Display customer to delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = data.Customers.Get(id);
            return View(customer);
        }

        //Delete customer selected
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            data.Customers.Delete(customer);
            data.Save();
            return RedirectToAction("Index3", "Customer");
        }

    }
}
