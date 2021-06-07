// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsPro.Models;

// <summary>
// Validation Controller for validating the customer's email address
// if already exist
// </summary>

namespace SportsPro.Controllers
{
    public class ValidationController : Controller
    {
        public JsonResult CheckEmail([FromServices] IRepository<Customer> data, string emailAddress
                                                    ,int customerid)
        {
            // skip validation for existing customer
            if (customerid != 0) { TempData["okEmail"] = true; return Json(true); }

            string msg = Check.EmailExists(data, emailAddress);  

            if (string.IsNullOrEmpty(msg))
            {
                TempData["okEmail"] = true;
                return Json(true);
            }
            else return Json(msg);
        }

    }
}

