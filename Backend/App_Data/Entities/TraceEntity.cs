namespace Backend.App_Data.Entities;

public class TraceEntity: EntityBase
{
    public string TraceID { get; set; } = Guid.NewGuid().ToString();
 
}