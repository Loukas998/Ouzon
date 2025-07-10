using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities;
using Template.Domain.Entities.AuthEntities;

namespace Template.Domain.Repositories;

public interface IAccountRepository
{
    Task<bool> CheckPassword(string userId, string password);
    Task<User> GetUserAsync(string id);
    Task<AuthResponse>? LoginUser(string email, string password);
    Task<int> NewUsersAfterMonth(int month, string roleId, int year);
    Task<int> NumberOfUsersInRole(string roleId);
    Task<IEnumerable<IdentityError>> Register(User owner, string password, string role);
    Task<IEnumerable<IdentityError>> RegisterAdmin(User user, string password);
    Task UpdateUser(User user);
}
