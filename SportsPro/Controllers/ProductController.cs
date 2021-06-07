// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Collections.Generic;
using System.Linq;

// < summary >
// Product Controller for Adding, Editing and Deleting product table
// This allows to Save new and updated and deleted rows
// </ summary >

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private IRepository<Product> products { get; set; }
        public ProductController(IRepository<Product> rep) => products = rep;

        [Route("products")]
        public ViewResult Index1()
        {
            var options = new QueryOptions<Product>
            {
                OrderBy = c => c.Name
            };
            var prodnames = products.List(options);
            return View(prodnames);
        }

        //Adds a new Product
        [HttpGet]
        public ViewResult Add()
        {
            var options = new QueryOptions<Product>
            {
                OrderBy = c => c.Name
            };
            ViewBag.Action = "Add";
            ViewBag.Products = products.List(options);
            return View("Edit", new Product());
        }

        //Retreive a product to be updated 
        [HttpGet]
        public ViewResult Edit(int id)
        {
            var options = new QueryOptions<Product>
            {
                OrderBy = c => c.Name
            };
            ViewBag.Action = "Edit";
            ViewBag.Products = products.List(options);
            var product = products.Get(id);
            return View(product);
        }

        //Saves the updated product in the database
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            string whataction;

            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)                  //ADD if no product 
                {
                    whataction = "is added to the";
                    products.Insert(product);
                }
                else                                        //Update current 
                {
                    whataction = "is edited in the";
                    products.Update(product);
                }

                TempData["message"] = $"{product.Name} {whataction} database.";
                products.Save();
                return RedirectToAction("Index1", "Product");
            }
            else
            {
                var options = new QueryOptions<Product>
                {
                    OrderBy = c => c.Name
                };
                ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
                ViewBag.Products = products.List(options);
                return View(product);
            }
        }

        //Retreive product to be deleted
        [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = products.Get(id);
            return View(product);
        }


        //Remove product select from the database
        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            products.Delete(product);
            products.Save();
            TempData["message"] = $"{product.Name} deleted from database.";
            return RedirectToAction("Index1", "Product");
        }

    }
}
