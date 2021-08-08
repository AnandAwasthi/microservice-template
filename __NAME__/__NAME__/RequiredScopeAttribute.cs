
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Identity.Web;
using System;
using System.Linq;
using System.Security.Claims;

namespace __NAME__
{
    public class RequiredScopeAttribute : TypeFilterAttribute
    {
        public RequiredScopeAttribute(string[] scopes) : base(typeof(RequiredScopeFilter))
        {
            Arguments = new object[] { scopes };
        }
    }

    public class RequiredScopeFilter : IAuthorizationFilter
    {
        readonly string[] _scopes;

        public RequiredScopeFilter(string[] scopes)
        {
            _scopes = scopes;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_scopes == null || context == null)
            {
                throw new ArgumentNullException(nameof(_scopes));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            else if (context.HttpContext.User == null || context.HttpContext.User.Claims == null || !context.HttpContext.User.Claims.Any())
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                // Attempt with Scp claim
                Claim? scopeClaim = context.HttpContext.User.FindFirst(ClaimConstants.Scp);

                // Fallback to Scope claim name
                if (scopeClaim == null)
                {
                    scopeClaim = context.HttpContext.User.FindFirst(ClaimConstants.Scope);
                }

                if (scopeClaim == null || !scopeClaim.Value.Split(' ').Intersect(_scopes).Any())
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
