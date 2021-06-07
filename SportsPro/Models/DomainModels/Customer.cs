// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Models
{
	public class Customer
	{
		public int CustomerID { get; set; }

		[Required(ErrorMessage = "FirstName cannot be empty")]
		[StringLength(50, ErrorMessage = "FirstName must be less than 51 characters.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "LastName cannot be empty")]
		[StringLength(50, ErrorMessage = "LastName must be less than 51 characters.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Address cannot be empty")]
		[StringLength(50, ErrorMessage = "Address must be less than 51 characters.")]
		public string Address { get; set; }

		[Required(ErrorMessage = "City cannot be empty")]
		[StringLength(50, ErrorMessage = "City must be less than 51 characters.")]
		public string City { get; set; }

		[Required(ErrorMessage = "State cannot be empty")]
		[StringLength(50, ErrorMessage = "State must be less than 51 characters.")]
		public string State { get; set; }

		[Required]
		[StringLength(20)]
		public string PostalCode { get; set; }

		//[Required(ErrorMessage = "Required")]
		public string CountryID { get; set; }

		//[Required(ErrorMessage = "Required")]
		public Country Country { get; set; }


		[RegularExpression(@"^\(\d{3}\)\d{3}-\d{4}$", ErrorMessage = "Please follow pattern (999)999-9999")]
		public string Phone { get; set; }

		//[BindProperty, TempData]
		[Required(ErrorMessage = "Email cannot be empty")]
		[Display(Name = "Email Address")]
		[DataType(DataType.EmailAddress, ErrorMessage = "Please enter email in this format xxxxx@xxxx.com")]
		[StringLength(50, ErrorMessage = "Email must be less than 51 characters.")]
		[Remote("CheckEmail", "Validation", AdditionalFields = "CustomerID")]
		public string Email { get; set; }

		public string FullName => FirstName + " " + LastName;   // read-only property

		// navigation property to linking entity
		public ICollection<Registration> Registrations { get; set; }
	}
}