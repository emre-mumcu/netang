namespace Backend.App_Data.Entities;

public class User : EntityBase
{
    public required string Email { get; set; }    
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public required string UserName { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? City { get; set; }
    public List<Photo>? Photos { get; set; } = [];
}