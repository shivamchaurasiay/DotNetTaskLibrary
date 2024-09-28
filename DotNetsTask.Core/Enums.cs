
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;







public enum ModalSize
{
    Small,
    Large,
    Medium,
    XLarge
}



public enum GalleryMediaType
{
    Image = 1,
    Video = 2
}

public enum AuthenticationType
{
    Authenticated = 1,
    Anonymous = 2
}

public enum EmailFormat
{
    [Display(Name = "SMTP (This sends email directly and requires the information below)")]
    SMTP = 1,

    [Display(Name = "MAPI (This sends email through your existing email program)")]
    MAPI1 = 2,

    [Display(Name = "MAPI (Create but don't send)")]
    MAPI2 = 3
}

public enum Gender
{
    [Display(Name = "--Select--")]
    None = 0,
    Male = 1,
    Female = 2,
    Transgender=3,
}

public enum ObjectState
{
    Added,
    Modified,
    Deleted
}

public enum UserRoles
{
    SuperAdmin = 1, // data access not allowed or data accessible
    Admin = 2, // college/admin
    Faculty = 3,
    Student = 4,
    HOD = 5,

}

public enum MessageType
{
    Info,
    Warning,
    Danger,
    Success
}


public enum CredentialsRequest
{
    UserId = 1,
    UserPassword = 2
}


