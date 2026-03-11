using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SchoolEnterprise.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RoleGuardAttribute(params string[] roles) : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var role = context.HttpContext.Session.GetString("Role");
        if (string.IsNullOrWhiteSpace(role) || !roles.Contains(role))
        {
            context.Result = new RedirectToActionResult("Login", "Account", null);
        }
    }
}
