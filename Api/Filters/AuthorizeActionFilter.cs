using TestApp.Application.Services.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace TestApp.Api.Filters
{
    public class AuthorizeActionFilter:IAsyncActionFilter
    {
        private readonly ICurrentUser _currentUser;

        public AuthorizeActionFilter(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor == null) { return; }

            var hasallowAnonymous = descriptor.MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>() != null;
            if (hasallowAnonymous)
            {
                await next();
            }
            else
            {
                var actionPermissions = GetActionPermissions(context, descriptor);
                if (!actionPermissions.Any()) { await next.Invoke(); }
                var permissions = _currentUser.ActiveRoleAndFlatPermissions;
                bool hasAtLeastOnerPermission = permissions.Select(x => x.ToString()).Intersect(actionPermissions).Any();
                if (hasAtLeastOnerPermission)
                    await next.Invoke();

                context.Result = new UnauthorizedResult();
            }
        }


        private IEnumerable<string> GetActionPermissions(ActionExecutingContext context, ControllerActionDescriptor descriptor)
        {
            var allowPermissionAttribute = descriptor.MethodInfo.GetCustomAttribute<AllowPermissionsAttribute>();
            if (allowPermissionAttribute != null)
                return allowPermissionAttribute.Permissions;

            return GetConditionalPermissions(context, descriptor);
        }

        private IEnumerable<string> GetConditionalPermissions(ActionExecutingContext context, ControllerActionDescriptor descriptor)
        {
            var conditionalAttributes = descriptor.MethodInfo.GetCustomAttributes<ConditionalAllowPermissionsAttribute>();
            if (!conditionalAttributes.Any())
                return Enumerable.Empty<string>();

            var permissions = new List<string>();
            foreach (var attribute in conditionalAttributes)
            {
                if (EvaluateCondition(context, attribute.ArgumentName, attribute.Value))
                    permissions.AddRange(attribute.Permissions);
            }
            return permissions;
        }

        private bool EvaluateCondition(ActionExecutingContext context,string argumentName,object coditionalValue)
        {
            var argumentValue = context.ActionArguments[argumentName];
            if ((argumentValue !=null && coditionalValue==null)|| (argumentValue == null && coditionalValue != null))
            {
                return false;
            }
            return Equals(argumentValue, Convert.ChangeType(coditionalValue, argumentValue.GetType()));
        }
    }
}
