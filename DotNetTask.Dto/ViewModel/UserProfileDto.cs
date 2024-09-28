using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTask.Dto.ViewModel
{
    public class UserProfileDto
    {

        public int PersonId { get; set; }
        public long UserId { get; set; }

        public string? FirstName { get; set; }
        public string? UserName { get; set; }

        public string? LastName { get; set; }
        public string? MobileNo { get; set; }

        public string? Email { get; set; }

        public int? Gender { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public string? Address { get; set; }

        public string? Block { get; set; }

        public string? PinCode { get; set; }

        public string? District { get; set; }

        public string? ProfilePicPath { get; set; }

        public string? AboutUs { get; set; }

        public string? TwitterProfileLink { get; set; }

        public string? FacebookProfilelink { get; set; }

        public string? InstagramProfilelink { get; set; }

        public string? LinkedinProfile { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [Required(ErrorMessage = "Current Password is required.")]
        public string? CurrentPassword { get; set; }
        [Required(ErrorMessage = "New Password is required.")]
       
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Please re-enter the new password.")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]

        public string? ReEnterPassword { get; set; }
    }
}
