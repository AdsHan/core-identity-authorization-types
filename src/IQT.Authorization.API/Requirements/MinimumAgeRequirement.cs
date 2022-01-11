using Microsoft.AspNetCore.Authorization;

namespace IQT.Authorization.API.Requirements;

public class MinimumAgeRequirement : IAuthorizationRequirement
{
    public MinimumAgeRequirement(int age)
    {
        MinimumAge = age;
    }

    public int MinimumAge { get; set; }
}

