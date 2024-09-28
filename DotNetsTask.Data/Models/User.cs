using System;
using System.Collections.Generic;

namespace DotNetsTask.Data.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public string? Location { get; set; }

    public string? AdharCard { get; set; }

    public virtual ICollection<IssuedBook> IssuedBooks { get; set; } = new List<IssuedBook>();
}
