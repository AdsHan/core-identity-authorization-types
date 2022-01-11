using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IQT.Authorization.API.Requirements;

public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
        {
            return;
        }

        var birth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);

        int year = DateTime.Today.Year - birth.Year;

        if (year >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }
    }
}

