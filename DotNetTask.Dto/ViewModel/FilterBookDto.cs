using DotNetsTask.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTask.Dto.ViewModel
{
    public class FilterBookDto
    {
        public int BookId { get; set; }
        public string? Subject { get; set; }
    
        public string? Title { get; set; }
      
        public string? Author { get; set; }
       
        public string? Genre { get; set; }
      
        public string? Isbn { get; set; }
        
        public string? PublishedYear { get; set; }
        
        public int? Quantity { get; set; }

        public int? AvailableQuantity { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public virtual ICollection<IssuedBook> IssuedBooks { get; set; } = new List<IssuedBook>();

        public IReadOnlyList<FilterBookDto> LstBookFilterData { get; set; } = new List<FilterBookDto>();
    }
}
