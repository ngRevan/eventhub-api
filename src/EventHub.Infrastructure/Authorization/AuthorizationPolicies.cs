using Microsoft.AspNetCore.Authorization;

namespace EventHub.Infrastructure.Authorization
{
    public static class AuthorizationPolicies
    {
        public static void AddPolicies(AuthorizationOptions options)
        {
            options.AddPolicy(AuthorizationPolicyNames.ChatHubMember, policy =>
            {
                policy.Requirements.Add(new ChatHubMemberRequirement());
            });
        }
    }
}
