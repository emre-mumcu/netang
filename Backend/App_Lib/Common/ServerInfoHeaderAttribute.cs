namespace Backend.App_Lib.Common;

public class ServerInfoHeaderAttribute: AddHeaderAttribute
{
    public ServerInfoHeaderAttribute() : base("X-Server-Name", "Backend Server")
    {
    }
}
