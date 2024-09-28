using DotNetsTask.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTask.Dto
{
    public class UserDto
    {
		public int UserId { get; set; }
		public int RoleId { get; set; }
		[Required(ErrorMessage = "Please Enter Your Name")]
		public string? Name { get; set; }
		[Required(ErrorMessage = "Please Enter Location")]
		public string? Location { get; set; }
		[Required(ErrorMessage = "Please Enter Adhar Card Number")]
		public string? AdharCard { get; set; }
		
		public string? Password { get; set; }



		[Required(ErrorMessage = "Please Enter Email")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Please Enter Mobile Number")]
		[StringLength(10, ErrorMessage = "Mobile number must be 10 digits long.")]
		[RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits.")]
		public string? Phone { get; set; }

		public DateOnly? RegistrationDate { get; set; }

		public bool? IsActive { get; set; }

		public bool? IsDeleted { get; set; }

		public int? UpdatedBy { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public virtual ICollection<IssuedBook> IssuedBooks { get; set; } = new List<IssuedBook>();

	}
}
