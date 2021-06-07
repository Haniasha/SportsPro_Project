// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Authorization;

// <summary>
// Incident Controller for Adding, Editing and Deleting an incident
// This allows to Save new and updated and deleted rows
// Each row in the incident is related to one row in the Customer table
// One row in the Product table and one row in the Technician table
//Using Unitof to access more than 1 table
// </summary>
namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IncidentController : Controller
    {
        private ISportsProUnitOfWork data { get; set; }
        public IncidentController(ISportsProUnitOfWork unit) => data = unit;

        public IActionResult Index()
        {
            return RedirectToAction("Index4", "Incident");
        }

        //Create a list of incidents including the Customer and product table
        //Displays the Incident Manager Page
        //Option to display all or unassigned or open incidents       
        [Route("incidents")]
        public IActionResult Index4(string id = "All")
        {
            //Convert route id to Titlecase to be able to compare the id parameter against the List of filters
            CultureInfo culture_info = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culture_info.TextInfo;
            string selectid = txtinfo.ToTitleCase(id);

            IEnumerable<Incident> incidents;


            if (selectid == "Unassigned")           //Lists all incidents if TechnicianID is null
            {
                var options1 = new QueryOptions<Incident>
                {
                    Includes = "Customer, Product",
                    Where = t => t.TechnicianID == null
                };
                incidents = data.Incidents.List(options1);

            }
            else if (selectid == "Open")            //Lists all incidents if DateClosed is null
            {
                var options2 = new QueryOptions<Incident>
                {
                    Includes = "Customer, Product",
                    Where = t => t.DateClosed == null
                };
                incidents = data.Incidents.List(options2);

            }
            else                                     //List all incidents otherwise
            {
                var options3 = new QueryOptions<Incident>
                {
                    Includes = "Customer, Product"
                };
                incidents = data.Incidents.List(options3).ToList();
            }

            //IncidentMgrListViewModelInc is used to store the list of incidents
            //variables to be used for the filter
            var model = new IncidentMgrListViewModelInc
            {
                SelectedFilter = selectid,
                Incidents = incidents
            };

            return View(model);

        }


        //Products, Customers and Technicians table are used for dropdown selection in Add Page
        //IncidentAddEditViewModel is used to store the list of customers, products, technicians and incident
        [HttpGet]
        public IActionResult Add()
        {
            var custopt = new QueryOptions<Customer>
            {
                OrderBy = c => c.FirstName
            };
            var prodopt = new QueryOptions<Product>
            {
                OrderBy = g => g.Name
            };
            var techopt = new QueryOptions<Technician>
            {
                OrderBy = t => t.Name
            };
            var incident = new IncidentAddEditViewModel
            {
                AddorEditAction = "Add",
                Products = data.Products.List(prodopt),
                Customers = data.Customers.List(custopt),
                Technicians = data.Technicians.List(techopt),
                Incident = new Incident()

            };

            return View("Edit", incident);
        }

        //Displays the selected incident row to be updated
        //Products, Customers and Technicians table are used for dropdown selection in Edit Page
        //IncidentAddEditViewModel is used to store the list of customers, products, technicians and incident
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var custopt = new QueryOptions<Customer>
            {
                OrderBy = c => c.FirstName
            };
            var prodopt = new QueryOptions<Product>
            {
                OrderBy = g => g.Name
            };
            var techopt = new QueryOptions<Technician>
            {
                OrderBy = t => t.Name
            };
            var incident = new IncidentAddEditViewModel
            {
                AddorEditAction = "Edit",
                Products = data.Products.List(prodopt),
                Customers = data.Customers.List(custopt),
                Technicians = data.Technicians.List(techopt),
                Incident = data.Incidents.Get(id)

            };

            return View(incident);
        }

        // Displays the selected incident row to be updated
        //Products, Customers and Technicians table are used for dropdown selection in Edit Page
        //Saves Added or Updated incident data
        //Using same Razor view file to Add and Edit customer data
        //IncidentAddEditViewModel is used to store the list of customers, products, technicians and incident
        [HttpPost]
        public IActionResult Edit(IncidentAddEditViewModel incident1)
        {
            if (ModelState.IsValid)
            {
                if (incident1.Incident.IncidentID == 0)             //ADD if no incident created 
                    data.Incidents.Insert(incident1.Incident);      //or Edit if found incident
                else
                    data.Incidents.Update(incident1.Incident);
                data.Save();
                return RedirectToAction("Index4", "Incident");

            }
            else
            {
                var custopt = new QueryOptions<Customer>
                {
                    OrderBy = c => c.FirstName
                };
                var prodopt = new QueryOptions<Product>
                {
                    OrderBy = g => g.Name
                };
                var techopt = new QueryOptions<Technician>
                {
                    OrderBy = t => t.Name
                };
                var incidentnw = new IncidentAddEditViewModel
                {
                    AddorEditAction = (incident1.Incident.IncidentID == 0) ? "Add" : "Edit",
                    Products = data.Products.List(prodopt),
                    Customers = data.Customers.List(custopt),
                    Technicians = data.Technicians.List(techopt)

                };

                return View(incidentnw);
            }
        }

        //Display an incident
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var incident = data.Incidents.Get(id);
            return View(incident);
        }

        //Delete an incident
        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            data.Incidents.Delete(incident);
            data.Save();
            return RedirectToAction("Index4", "Incident");
        }

    }
}
