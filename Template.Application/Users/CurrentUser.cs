

namespace Template.Application.Users;

public record CurrentUser(string Id, string Email, string UserName, IEnumerable<string> Roles)
{
    public bool isInRole(string role) => Roles.Contains(role);
}
