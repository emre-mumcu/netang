using Backend.App_Data.Enums;

namespace Backend.App_Data;

public class EntityBase : IEntity
{
    public int Id { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.Now;
    public string State { get; set; } = StateEnum.Created.ToString();
    public string Status { get; set; } = StatusEnum.Active.ToString();
}
