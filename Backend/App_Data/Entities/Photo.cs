namespace Backend.App_Data.Entities;

public class Photo : EntityBase
{
    public required string Url { get; set; }
    public bool IsMain { get; set; }

    // Navigation properties
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}