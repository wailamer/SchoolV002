using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class AuthService
{
    private readonly UserRepository _users;
    private readonly IHttpContextAccessor _accessor;

    public AuthService(UserRepository users, IHttpContextAccessor accessor)
    {
        _users = users;
        _accessor = accessor;
    }

    public bool Login(string username, string password)
    {
        var user = _users.GetAll().FirstOrDefault(x => x.Username == username && x.Password == password);
        if (user is null) return false;
        _accessor.HttpContext?.Session.SetString("username", user.Username);
        _accessor.HttpContext?.Session.SetString("role", user.Role);
        return true;
    }

    public void Logout() => _accessor.HttpContext?.Session.Clear();
}
