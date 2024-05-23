using Microsoft.AspNet.Identity.EntityFramework;

namespace SenaThreads.Domain.Users;

public class User : IdentityUser
{
    public override string Id { get; set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    //Propiedades Opcionales
    public override string PhoneNumber { get; set; }
    public string Biography { get; set; }
    public string City { get; set; }
    public DateTime? DateOfBirth { get; set; }


    //Propiedades de navegacion 
    public ICollection<UserBlock> BlockedUsers { get; set; } //Usuario bloqueados por este usuario
    public ICollection<UserBlock> BlockeByUsers { get; set; } //Usuario que han bloqueado este usuario
    public ICollection<Follow> Followers { get; set; } // Usuarios que siguen a este usuario
    public ICollection<Follow> Followees { get; set; } // Usuarios que sigue este usuario


    // Constructor de la clase User que inicializa las propiedades principales 
    public User(string firstName, string lastName, string email, string userName)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
    }
}
