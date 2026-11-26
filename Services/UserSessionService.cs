namespace Libero.Services;

public interface IUserSessionService
{
    string CurrentRole { get; set; }
    string UserName { get; set; }
    string UserInitials { get; set; }
}

public class UserSessionService : IUserSessionService
{
    public string CurrentRole { get; set; } = "Customer";
    public string UserName { get; set; } = "Lê Minh Anh";
    public string UserInitials { get; set; } = "AL";
}