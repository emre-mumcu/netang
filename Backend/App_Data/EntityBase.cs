namespace Backend.App_Data;

public class EntityBase : IEntityBase
{
    public int Id { get; set; }
    public string ClientIP { get; set; } = null!;
    public DateTime TimeStamp { get; set; } = DateTime.Now;
    public string State { get; set; } = null!;
    public string Status { get; set; } = null!;
}
