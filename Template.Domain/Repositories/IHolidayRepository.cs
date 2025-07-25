﻿using Template.Domain.Entities.Schedule;

namespace Template.Domain.Repositories;

public interface IHolidayRepository : IGenericRepository<Holiday>
{
    Task<List<Holiday>> GetAllHolidaysWithFilter(DateTime? FromDate, DateTime? ToDate, string? AssistantId);
    Task<List<Holiday>> GetHolidaysWithFilter(int pageNum, int pageSize, DateTime? FromDate, DateTime? ToDate, string? AssistantId);
}
