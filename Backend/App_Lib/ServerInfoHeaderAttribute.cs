namespace Backend;

public class ServerInfoHeaderAttribute: AddHeaderAttribute
{
    public ServerInfoHeaderAttribute() : base("X-Server-Name", "Backend Server")
    {
    }
}
