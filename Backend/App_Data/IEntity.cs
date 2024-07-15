namespace Backend.App_Data;

public interface IEntity
{
    public int Id { get; set; }
    public string State { get; set; }
    public string Status { get; set; }
    public DateTime TimeStamp { get; set; }
}
