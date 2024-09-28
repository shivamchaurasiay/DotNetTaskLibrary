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
	public class ReturnDto
	{
		[Required(ErrorMessage = "Please Select Book")]
		[Range(1, int.MaxValue, ErrorMessage = "Please Select Book")]
		public int IssueId { get; set; }
		[Required(ErrorMessage = "Please Select Return Date")]
		public DateOnly? ReturnDate { get; set; }
		
		public int? Status { get; set; }
		[Required(ErrorMessage = "Please Select Student Name")]
		[Range(1, int.MaxValue, ErrorMessage = "Please Select Student")]
		public int UserId { get; set; }
		public int BookId { get; set; }
		public int MobileNo { get; set; }
		
		public DateOnly? IssueDate { get; set; }
	
		public DateOnly? ExpectedReturnDate { get; set; }
		

		public bool? IsActive { get; set; }

		public bool? IsDeleted { get; set; }

		public int? UpdatedBy { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public virtual Book Book { get; set; } = null!;

		public virtual User User { get; set; } = null!;

		public List<SelectListItem> Books { get; set; }
		public List<SelectListItem> Students { get; set; }
	}
}
