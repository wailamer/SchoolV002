using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SchoolEnterprise.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RoleGuardAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles;

    public RoleGuardAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var role = context.HttpContext.Session.GetString("role");
        if (string.IsNullOrWhiteSpace(role) || !_roles.Contains(role))
        {
            context.Result = new RedirectToActionResult("Login", "Account", null);
        }
    }
}
