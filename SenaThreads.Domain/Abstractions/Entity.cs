namespace SenaThreads.Domain.Abstractions;

public abstract class Entity
{
    public DateTime CreatedOnUtc { get; set; }
    public string CreatedBy { get; set; }
    public  DateTime UpdatedOnUtc { get; set; }
    public string UpdatedBy { get; set; }

    public Entity()
    {
        // Se Inicializa CreatedOnUtc con la fecha y hora actuales 
        CreatedOnUtc = DateTime.UtcNow;
        UpdatedOnUtc = DateTime.UtcNow;


    }
}
