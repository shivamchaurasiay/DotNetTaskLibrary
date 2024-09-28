using DotNetsTask.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTask.Dto.ViewModel
{
	public class IssuedBookDto
	{
		public int IssueId { get; set; }
		
			[Required(ErrorMessage = "Please Select Book")]
			[Range(1, int.MaxValue, ErrorMessage = "Please Select Book")]
			public int BookId { get; set; }

			[Required(ErrorMessage = "Please Select Student Name")]
			[Range(1, int.MaxValue, ErrorMessage = "Please Select Student")]
			public int UserId { get; set; }

			[Required(ErrorMessage = "Please Enter Mobile Number")]
			[StringLength(10, ErrorMessage = "Mobile number must be exactly 10 digits long.", MinimumLength = 10)]
			[RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be exactly 10 digits.")]
			public string MobileNo { get; set; }  // Changed from nullable to non-nullable if you want to enforce a value

			[Required(ErrorMessage = "Please Select Issue Date")]
			public DateOnly? IssueDate { get; set; }

			[Required(ErrorMessage = "Please Select Expected Return Date")]
		public string? ExpectedReturnDate { get; set; }


		public int? Status { get; set; }
		public DateOnly? ReturnDate { get; set; }

		public bool? IsActive { get; set; }

		public bool? IsDeleted { get; set; }

		public int? UpdatedBy { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }


		public List<SelectListItem> Books { get; set; }
		public List<SelectListItem> Students { get; set; }
		public IReadOnlyList<IssuedBookDto> IssuedBookList { get; set; } = new List<IssuedBookDto>();
		public IReadOnlyList<IssuedBookDto> ReturnBookList { get; set; } = new List<IssuedBookDto>();

		public virtual Book Book { get; set; } = null!;

		public virtual User User { get; set; } = null!;


	}
}
