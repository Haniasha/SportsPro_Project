// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsPro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

// <summary>
// Technician Controller for Adding, Editing and Deleting technician table
// This allows to Save new and updated and deleted rows
// Using Unitofwork here to access more than 1 table 
// </summary>
namespace SportsPro.Controllers
{
    
    public class TechnicianController : Controller
    {
        private ISession session { get; set; }

        private ISportsProUnitOfWork data { get; set; }

        public TechnicianController(ISportsProUnitOfWork unit, IHttpContextAccessor http)
        {
            data = unit;
            session = http.HttpContext.Session;
        }

        [Route("technicians")]
        [Authorize(Roles = "Admin")]    //Sets admin role.
        public ActionResult Index2()
        {
            var options = new QueryOptions<Technician>
            {
                OrderBy = c => c.Name
            };
            var technicians = data.Technicians.List(options);
            return View(technicians);
        }


        //Adds a new Technician
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            var options = new QueryOptions<Technician>
            {
                OrderBy = c => c.Name
            };
            ViewBag.Technicians = data.Technicians.List(options);
            return View("Edit", new Technician());
        }


        //Get Technician List
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var options = new QueryOptions<Technician>
            {
                OrderBy = c => c.Name
            };
            ViewBag.Technicians = data.Technicians.List(options);
            var technician = data.Technicians.Get(id);
            return View(technician);
        }


        //Save Technician list to the database
        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianID == 0)           //ADD if no incident created
                    data.Technicians.Insert(technician);    //or Edit if found incident
                else
                    data.Technicians.Update(technician);
                data.Save();
                return RedirectToAction("Index2", "Technician");
            }
            else
            {
                var options = new QueryOptions<Technician>
                {
                    OrderBy = c => c.Name
                };
                ViewBag.Action = (technician.TechnicianID == 0) ? "Add" : "Edit";
                ViewBag.Technicians = data.Technicians.List(options);
                return View(technician);
            }
        }

        //Retreive Technician to be deleted
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var technician = data.Technicians.Get(id);
            return View(technician);
        }


        //Remove Technician from the Database
        [HttpPost]
        public IActionResult Delete(Technician technician)
        {
            data.Technicians.Delete(technician);
            data.Save();
            return RedirectToAction("Index2", "Technician");
        }




        //TechIncident
        //Gets Technician Page that allows user to select technician through a dropdown list
        [Route("techincident/get")]
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var options = new QueryOptions<Technician>
            {
                OrderBy = c => c.Name
            };
            ViewBag.Action = "Get";
            ViewBag.Technicians = data.Technicians.List(options);
            return View();
        }

        //Opens up the List of incidents by technician        
        [HttpGet]
        public IActionResult Open(int TechnicianID)
        {
            ViewBag.Action = "Open";
            var options = new QueryOptions<Incident>
            {
                Includes = "Customer, Product, Technician",
                Where = t => t.TechnicianID == TechnicianID
            };

            var incidents = data.Incidents.List(options);

            ViewBag.Name = data.Technicians.Get(TechnicianID).Name;

            //IncidentMgrListViewModelInc is used to store the list of incidents            
            IncidentMgrListViewModelInc incident = new IncidentMgrListViewModelInc()
            {
                Incidents = incidents
            };

            //Stores technician ID in session state
            if (TechnicianID > 0)
            {
                session.SetInt32("techid", TechnicianID);
                ViewBag.http = session.GetInt32("techid");
            }

            //A message that displays if a technician has an assigned or no incidents
            if (incident.Incidents.Count() == 0)
                ViewBag.Title = "No Open Incidents";
            else
                ViewBag.Title = "Assigned/Open Incidents";

            return View(incident);

        }

        //Retrieve the incident for current technician to be updated
        [HttpGet]
        public IActionResult EditCurrInc(int id)
        {
            var incidentedc = new IncidentMgrListViewModel();
            incidentedc.AddorEditAction = "EditCurrInc";
            var options = new QueryOptions<Incident>
            {
                Includes = "Customer, Product, Technician",
                Where = i => i.IncidentID == id
            };

            incidentedc.Incident = data.Incidents.Get(options);

            return View(incidentedc);
        }


        //Save the updated incident by the current technician
        [HttpPost]
        public IActionResult EditCurrInc(IncidentMgrListViewModel incident1)
        {

            var newinc = data.Incidents.Get(incident1.Incident.IncidentID);
            newinc.Description = incident1.Incident.Description;
            newinc.DateClosed = incident1.Incident.DateClosed;
            data.Save();

            return RedirectToAction("Open", new { TechnicianID = newinc.TechnicianID });

        }

    }
}
