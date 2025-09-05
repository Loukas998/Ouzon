using Template.Domain.Entities;

namespace Template.Domain.Repositories;

public interface IStatisticsRepository
{
    Task<Dictionary<string, int>> GetNumberOfUsersInEachRoleAsync();
    Task<int> GetNumberOfProcedures(int? month, int? year, DateTime? startDate = null, DateTime? endDate = null);
    Task<List<User>> GetTopFiveAssistantsByRatings();
    Task<List<(User user, int procCount)>> GetTopFiveAssistantsByAssignments();
    Task<List<(User user, int procCount)>> GetTopFiveDoctors();
}
