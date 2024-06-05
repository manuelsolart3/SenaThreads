namespace SenaThreads.Domain.Abstractions;

public class Entity
{
    public DateTime CreatedOnUtc { get; set; }
    public string CreatedBy { get; set; }
    public  DateTime UpdateOnUtc { get; set; }
    public string UpdatedBy { get; set; }

    public Entity()
    {
        // Inicializa CreatedOnUtc con la fecha y hora actuales 
        CreatedOnUtc = DateTime.UtcNow;
    }
}
