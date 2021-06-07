// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Models
{
    public class IncidentMgrListViewModelInc
    {

        public string SelectedFilter { get; set; }

        public IEnumerable<Incident> Incidents { get; set; }

        public string CheckActiveFilter(string filtr) =>
            filtr == SelectedFilter ? "active" : "";

        public string[] Filterarray = { "All", "Unassigned", "Open" };

    }
}
