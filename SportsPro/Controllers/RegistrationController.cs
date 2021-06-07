// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using SportsPro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
// <summary>
// Technician Registrations for selection Customer name and Product related to that csutomer
// This allows to Save and updated and deleted rows
// </summary>
namespace SportsPro.Controllers
{
 //Displays the Registration Page
    [Authorize(Roles = "Admin")]   //sets admin role.
    public class RegistrationController : Controller
    {
        private SportsProContext context { get; set; }

        public RegistrationController(SportsProContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }
       //Get Customer List
        [HttpGet]
        public IActionResult GetCustomer()
        {
            ViewBag.Action = "GetCustomer";
            ViewBag.Customers = context.Customers.OrderBy(g => g.FirstName).ToList();
            return View();
        }

        //Opens up the List of customers and products       
        [HttpGet]
        public IActionResult OpenCustomer(int CustomerID)
        {
            ViewBag.Action = "OpenCustomer";

            List<Registration> regists = context.Registrations.Include(c => c.Customer)
                                         .Include(p => p.Product)
                                         .Where(r => r.CustomerID == CustomerID).ToList();

            ViewBag.Name = context.Customers.Find(CustomerID).FullName;

            //CustomerViewModel is used to store the list of incidents            
            CustomerViewModel register = new CustomerViewModel()
            {
                Registrations = regists,
                Products = context.Products.ToList()
            };

            //Stores customer ID in session state
            if (CustomerID > 0)
            {
                HttpContext.Session.SetInt32("custid", CustomerID);
                ViewBag.http = HttpContext.Session.GetInt32("custid");
            }

            //A message that displays if a customer has an assigned product
            if (register.Registrations.Count == 0)
                ViewBag.Title = "No products registered";

            return View(register);

        }

        //Opens up the List of products related to customers        
        [HttpPost]
        public IActionResult OpenCustomer(CustomerViewModel regist1)
        {
            Registration newreg = new Registration { ProductID = regist1.ProductID,
                                                     CustomerID = regist1.CustomerID };

           
            context.Registrations.Add(newreg);
            context.SaveChanges();

            TempData["message"] = $"ProductId#: {regist1.ProductID} is added.";
            return RedirectToAction("OpenCustomer", new { CustomerID = regist1.CustomerID });

        }
        
       //Remove the product from the list and redirect to the page
        [HttpGet]
        public IActionResult Delete(int CustomerID, int ProductID)
        {
        
            var reg = context.Registrations.Single(r => r.CustomerID == CustomerID 
                                                        && r.ProductID == ProductID);
            
            context.Registrations.Remove(reg);
            context.SaveChanges();

            TempData["message"] = $"ProductId#: {ProductID} deleted from database.";
            return RedirectToAction("OpenCustomer", new { CustomerID });

        }

    }
}
