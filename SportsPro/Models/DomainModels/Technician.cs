// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using System;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
	public class Technician
	{
		[Required(ErrorMessage = "Technician ID is not selected.")]
		public int TechnicianID { get; set; }

		[Required(ErrorMessage = "Please select a technician name.")]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Phone { get; set; }
	}
}
