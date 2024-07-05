using SenaThreads.Domain.Users;

namespace SenaThreads.Application.Dtos.Users;
public class UserBlockStatusDto
{
    public bool IsBlocked { get; set; }
    public string UsuarioQueHaSidoBloqueado { get; set; }
    public string UsuarioQueLoBloqueo { get; set; }
}
