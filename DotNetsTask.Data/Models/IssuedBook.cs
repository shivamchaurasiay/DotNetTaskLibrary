using System;
using System.Collections.Generic;

namespace DotNetsTask.Data.Models;

public partial class IssuedBook
{
    public int IssueId { get; set; }

    public int BookId { get; set; }
    public int? Status { get; set; }

    public int UserId { get; set; }

    public DateOnly? IssueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? MobileNo { get; set; }

    public string? ExpectedReturnDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
