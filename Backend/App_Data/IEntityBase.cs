namespace Backend.App_Data;

public interface IEntityBase
{
    public int Id { get; set; }
    public string State { get; set; }
    public string Status { get; set; }
    public string ClientIP { get; set; }
    public DateTime TimeStamp { get; set; }
}
