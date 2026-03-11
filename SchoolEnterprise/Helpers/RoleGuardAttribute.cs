using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolEnterprise.Models.Domain;

namespace SchoolEnterprise.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RoleGuardAttribute(params UserRole[] allowedRoles) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var roleText = context.HttpContext.Session.GetString("Role");
        if (string.IsNullOrWhiteSpace(roleText) || !Enum.TryParse<UserRole>(roleText, out var role))
        {
            context.Result = new RedirectToActionResult("Login", "Account", null);
            return;
        }

        if (allowedRoles.Length > 0 && !allowedRoles.Contains(role))
        {
            context.Result = new RedirectToActionResult("Index", "Dashboard", null);
        }
    }
}
