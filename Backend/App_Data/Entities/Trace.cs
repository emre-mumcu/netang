namespace Backend.App_Data.Entities;

public class Trace: EntityBase
{
    public string TraceIdentifier { get; set; } = Guid.NewGuid().ToString(); 
    public string ClientIP { get; set; } = null!;
}