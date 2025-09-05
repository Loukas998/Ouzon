using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities;
using Template.Domain.Enums;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class StatisticsRepository(TemplateDbContext dbContext) : IStatisticsRepository
{
    public async Task<int> GetNumberOfProcedures(int? month, int? year, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = dbContext.Procedures.AsQueryable();

        if (month.HasValue)
            query = query.Where(p => p.Date.Month == month.Value);

        if (year.HasValue)
            query = query.Where(p => p.Date.Year == year.Value);

        if (startDate.HasValue && endDate.HasValue)
            query = query.Where(p => p.Date >= startDate.Value && p.Date <= endDate.Value);

        return await query.CountAsync();
    }

    public async Task<Dictionary<string, int>> GetNumberOfUsersInEachRoleAsync()
    {
        var result = await (from role in dbContext.Roles
                            join userRole in dbContext.UserRoles on role.Id equals userRole.RoleId into ur
                            select new
                            {
                                RoleName = role.Name!,
                                Count = ur.Count()
                            })
                           .ToDictionaryAsync(x => x.RoleName, x => x.Count);

        return result;
    }

    public async Task<List<(User user, int procCount)>> GetTopFiveAssistantsByAssignments()
    {
        var query = await dbContext.Users
            .Where(u => u.InProcedure.Where(ip => ip.Procedure.Status.Equals(EnumProcedureStatus.DONE)).Any())
           .Select(u => new
           {
               User = u,
               ProcCount = u.InProcedure.Count(ip => ip.Procedure.Status == EnumProcedureStatus.DONE)
           })
            .OrderByDescending(u => u.ProcCount)
            .Take(5)
            .ToListAsync()
            ;
        return query.Select(u => (u.User, u.ProcCount)).ToList();
    }

    public async Task<List<User>> GetTopFiveAssistantsByRatings()
    {
        return await dbContext.Users
           .Where(u => u.RatingsReceived.Any())
           .OrderByDescending(u => u.RatingsReceived.Average(r => r.Rate))
           .Take(5)
           .ToListAsync();
    }

    public async Task<List<(User user, int procCount)>> GetTopFiveDoctors()
    {
        var query = await dbContext.Users
            .Where(u => u.ProcedureFrom.Where(fp => fp.Status.Equals(EnumProcedureStatus.DONE)).Any())
            .Select(x => new
            {
                User = x,
                ProcCount = x.ProcedureFrom.Where(fp => fp.Status.Equals(EnumProcedureStatus.DONE)).Count(),
            })
            .OrderByDescending(u => u.ProcCount)
            .Take(5)
            .ToListAsync();
        return query.Select(x => (x.User, x.ProcCount)).ToList();
    }
}
