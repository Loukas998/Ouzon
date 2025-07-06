using Template.Domain.Entities.Notifications;

namespace Template.Domain.Repositories;

public interface IDeviceRepository : IGenericRepository<Device>
{
    public Task<List<Device>?> SearchAsync(string? deviceToken, string? userId, bool? optIn, DateTime? loggedInInAfter, DateTime? loggedInBefore);
}
