using Microsoft.AspNetCore.Identity;
using Template.Domain.Entities;
using Template.Domain.Entities.AuthEntities;

namespace Template.Domain.Repositories;

public interface IAccountRepository
{
    Task<bool> CheckPassword(string userId, string password);
    Task<User> GetUserAsync(string id, bool isAssistant);
    Task<User> GetUserDetails(string? id);
    Task<User> GetUserWithDevicesAsync(string id);
    //Task<List<User>> GetUsersWithFilters(string? role, string? email, string? phoneNumber, string? clinicAddress, string? clinicName);
    Task<AuthResponse>? LoginUser(string email, string password, string deviceToken);
    Task<int> NewUsersAfterMonth(int month, string roleId, int year);
    Task<int> NumberOfUsersInRole(string roleId);
    Task<IEnumerable<IdentityError>> Register(User owner, string password, string role);
    Task<IEnumerable<IdentityError>> RegisterAdmin(User user, string password);
    Task UpdateUser(User user);
    Task<bool> UserInRoleAsync(string id, string roleName);
    Task<List<User>> GetAssistants(string? sortByRating);
    public Task DeleteAccount(string userId);
    Task<User> UpdateUserAsync(User user);
    Task<List<(User user, string? roleName)>> GetUsersWithFilters(string? role, string? email, string? phoneNumber, string? clinicAddress, string? clinicName);
}
