using SchoolEnterprise.Models.Domain;
using SchoolEnterprise.Repositories;

namespace SchoolEnterprise.Services;

public class AuthService(UserRepository users)
{
    public AppUser? Login(string username, string password)
        => users.GetAll().FirstOrDefault(x => x.UserName == username && x.Password == password);
}
