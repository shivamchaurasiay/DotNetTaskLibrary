using DotNetsTask.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTask.Dto.ViewModel
{
	public class BookDto
	{
		public int BookId { get; set; }

		[Required(ErrorMessage = "Please Enter Subject")]
		public string? Subject { get; set; }
		[Required(ErrorMessage = "Please Enter Tittle")]
		public string? Title { get; set; }
		[Required(ErrorMessage = "Please Enter Author")]
		public string? Author { get; set; }
		[Required(ErrorMessage = "Please Enter Generation")]
		public string? Genre { get; set; }
		[Required(ErrorMessage = "Please Enter Isbn")]
		public string? Isbn { get; set; }
		[Required(ErrorMessage = "Please Enter Published Year")]
		public string? PublishedYear { get; set; }
		[Required(ErrorMessage = "Please Enter Quantity")]
		public int? Quantity { get; set; }

		public int? AvailableQuantity { get; set; }

		public bool? IsActive { get; set; }

		public bool? IsDeleted { get; set; }

		public int? UpdatedBy { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public int? CreatedBy { get; set; }

		public DateTime? CreatedOn { get; set; }

		public virtual ICollection<IssuedBook> IssuedBooks { get; set; } = new List<IssuedBook>();



		public IReadOnlyList<BookDto> BookList { get; set; } = new List<BookDto>();
        public IReadOnlyList<BookDto> LstBookFilterData { get; set; } = new List<BookDto>();
    }

}
