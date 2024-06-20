using Microsoft.AspNetCore.Identity;
using SenaThreads.Domain.Abstractions;

namespace SenaThreads.Domain.Users;

public class User : IdentityUser
{
    public override string Id { get; set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    //Propiedades Opcionales
    public string ProfilePictureS3Key { get; set; }
    public override string PhoneNumber { get; set; }
    public string Biography { get; set; }
    public string City { get; set; }
    public DateOnly? DateOfBirth { get; set; }


    //Propiedades de navegacion 
    public virtual ICollection<UserBlock> BlockedUsers { get; set; } //Usuario bloqueados por este usuario
    public virtual ICollection<UserBlock> BlockeByUsers { get; set; } //Usuario que ha bloqueado este usuario
    public virtual ICollection<Follow> Followers { get; set; } // Usuarios que siguen a este usuario
    public virtual ICollection<Follow> Followees { get; set; } // Usuarios que sigue este usuario


    // Constructor de la clase User que inicializa las propiedades principales 
    public User(string firstName, string lastName, string email, string userName)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
    }
}
