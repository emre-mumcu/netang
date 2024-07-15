namespace Backend.App_Data;

public class EntityBase : IEntity
{
    public int Id { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.Now;
    public string State { get; set; } = null!;
    public string Status { get; set; } = null!;
}
