using Microsoft.AspNetCore.Authorization;

namespace CRUD.API.Extensions;

public class PermissaoNecessaria : IAuthorizationRequirement
{
    public PermissaoNecessaria(string permissao)
    {
        Permissao = permissao;
    }

    public string Permissao { get; }
}

public class PermissaoNecessariaHandler : AuthorizationHandler<PermissaoNecessaria>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissaoNecessaria requirement)
    {
        if(context.User.HasClaim(c => c.Type == "Admin" && c.Value.Contains(requirement.Permissao)))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}