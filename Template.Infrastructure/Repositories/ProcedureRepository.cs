using Microsoft.EntityFrameworkCore;
using Template.Domain.Entities.ProcedureRelatedEntities;
using Template.Domain.Enums;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class ProcedureRepository : GenericRepository<Procedure>, IProcedureRepository
{
    public TemplateDbContext dbContext;
    public IToolRepository toolRepository;
    public IKitRepository kitRepository;
    public ProcedureRepository(TemplateDbContext dbContext, IToolRepository toolRepository, IKitRepository kitRepository) : base(dbContext)
    {
        this.dbContext = dbContext;
        this.toolRepository = toolRepository;
        this.kitRepository = kitRepository;
    }
    public async Task<Procedure> GetDetailedWithId(int id)
    {
        var procedure = await dbContext.Procedures
            .IgnoreQueryFilters()
            .Include(pro => pro.ToolsInProcedure)
                .ThenInclude(tp => tp.Tool)
            .Include(pro => pro.KitsInProcedure)
                .ThenInclude(tp => tp.Kit)
                    .ThenInclude(kit => kit.Implants)
             .Include(pro => pro.KitsInProcedure)
                 .ThenInclude(tp => tp.Kit)
                 .ThenInclude(kit => kit.Tools)
             .Include(pro => pro.AssistantsInProcedure)
                 .ThenInclude(ap => ap.Asisstant)
             .Include(pro => pro.Doctor)
                 .ThenInclude(doc => doc.Clinic)
             .Include(pro => pro.ProcedureImplants)
                 .ThenInclude(pi => pi.Implant)
             .Include(pro => pro.ProcedureImplantTools)
                 .ThenInclude(pro => pro.Implant)
              .Include(pro => pro.ProcedureImplantTools)
                 .ThenInclude(pro => pro.Tool)
                 .AsSplitQuery()
            .FirstOrDefaultAsync(pro => pro.Id == id);
        return procedure;
    }
    public async Task<List<Procedure>> GetFilteredProcedures(int pageSize, int pageNum, string? DoctorId, string? AssistantId)
    {
        var query = dbContext.Procedures.AsQueryable();
        if (!string.IsNullOrEmpty(DoctorId))
        {
            query = query.Where(p => p.DoctorId == DoctorId);
        }
        if (!string.IsNullOrEmpty(AssistantId))
        {
            query = query.Where(p => p.AssistantsInProcedure.Any(x => x.AsisstantId == AssistantId));
        }
        var procedures = await query.Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
           .ToListAsync();
        return procedures;
    }
    public async Task<List<Procedure>> GetAllFilteredProcedures(string? DoctorId, string? AssistantId, DateTime? from, DateTime? to,
        int? minNumberOfAssistants, int? maxNumberOfAssistants, string? doctorName, List<string>? assistantNames, string? clinicName,
        string? clinicAddress, EnumProcedureStatus? status, string? isDoctorAuthenticated, string? isAssistantAuthenticated)
    {
        var query = dbContext.Procedures
            .IgnoreQueryFilters()
            .AsQueryable();
        if (!string.IsNullOrEmpty(DoctorId))
        {
            query = query.Where(p => p.DoctorId == DoctorId).Include(p => p.Doctor).ThenInclude(d => d.Clinic);
        }
        if (!string.IsNullOrEmpty(AssistantId))
        {
            query = query.Where(p => p.AssistantsInProcedure.Any(x => x.AsisstantId == AssistantId));
        }
        if (from != null)
        {
            query = query.Where(pro => pro.Date >= from);
        }
        if (to != null)
        {
            query = query.Where(pro => pro.Date <= to);
        }
        if (minNumberOfAssistants != null)
        {
            query = query.Where(pro => pro.NumberOfAsisstants >= minNumberOfAssistants);
        }
        if (maxNumberOfAssistants != null)
        {
            query = query.Where(pro => pro.NumberOfAsisstants <= maxNumberOfAssistants);
        }
        if (!string.IsNullOrWhiteSpace(doctorName))
        {
            query = query.Where(pro => pro.Doctor.UserName!.Contains(doctorName));
        }
        if (assistantNames?.Count > 0)
        {
            var validNames = assistantNames.Where(name => !string.IsNullOrWhiteSpace(name)).ToList();

            if (validNames.Any())
            {
                query = query
                    .Where(pro => pro.AssistantsInProcedure != null && pro.AssistantsInProcedure.Any())
                    .Where(pro => pro.AssistantsInProcedure!
                        .Select(asp => asp.Asisstant.UserName) // Fixed typo (if applicable)
                        .Any(userName => validNames.Contains(userName)));
            }
        }
        if (!string.IsNullOrWhiteSpace(clinicName))
        {
            query = query.Where(pro => pro.Doctor.Clinic!.Name!.Contains(clinicName));
        }
        if (!string.IsNullOrWhiteSpace(clinicAddress))
        {
            query = query.Where(pro => pro.Doctor.Clinic!.Address!.Contains(clinicAddress));
        }
        if (status != null)
        {
            query = query.Where(pro => pro.Status == status);
        }
        if (!string.IsNullOrWhiteSpace(isDoctorAuthenticated))
        {
            query = query.Where(pro => pro.DoctorId.Equals(isDoctorAuthenticated));
        }
        if (!string.IsNullOrWhiteSpace(isAssistantAuthenticated))
        {
            query = query
                    .Where(pro => pro.AssistantsInProcedure != null && pro.AssistantsInProcedure.Any())
                    .Where(pro => pro.AssistantsInProcedure!
                        .Select(asp => asp.Asisstant.Id)
                        .Any(assistantId => assistantId.Equals(isAssistantAuthenticated)));
        }
        query = query.Include(pro => pro.Doctor)
            .ThenInclude(doc => doc.Clinic)
            .Include(pro => pro.AssistantsInProcedure!)
                .ThenInclude(asp => asp.Asisstant)
                .AsSplitQuery();
        var procedures = await query.OrderByDescending(p => p.Date).ToListAsync();
        return procedures;
    }
    public async Task<int> AddProcedureAssistant(ProcedureAssistant entity)
    {
        await dbContext.ProcedureAssistants.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
    public async Task<Procedure> GetProcedureWithKits(int Id)
    {
        return await dbContext.Procedures
            .Include(x => x.KitsInProcedure)
            .ThenInclude(kp => kp.Kit)
            .ThenInclude(kp => kp.Tools)
            .Include(kp => kp.KitsInProcedure)
            .ThenInclude(kp => kp.Kit)
            .ThenInclude(k => k.Implants)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == Id);
    }
    public async Task<Procedure> GetProcedureWithAssistants(int Id)
    {
        return await dbContext.Procedures
            .Include(x => x.AssistantsInProcedure)
            .ThenInclude(kp => kp.Asisstant)
            .FirstOrDefaultAsync(x => x.Id == Id);
    }
    public async Task<Procedure> GetProcedureWithToolsNotInKit(int Id)
    {
        return await dbContext.Procedures
            .Include(x => x.ToolsInProcedure)
            .ThenInclude(kp => kp.Tool)
            .FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<List<Procedure>> GetPagedFilteredProcedures(int pageSize, int pageNum, string? DoctorId, string? AssistantId)
    {
        var query = dbContext.Procedures
            .Include(pro => pro.Doctor)
            .ThenInclude(doc => doc.Clinic)
            .AsQueryable();
        if (!string.IsNullOrEmpty(DoctorId))
        {
            query = query.Where(p => p.DoctorId == DoctorId).Include(p => p.Doctor).ThenInclude(d => d.Clinic);
        }
        if (!string.IsNullOrEmpty(AssistantId))
        {
            query = query.Where(p => p.AssistantsInProcedure.Any(x => x.AsisstantId == AssistantId));
        }

        var procedures = await query
           .Skip(pageSize * (pageNum - 1))
           .Take(pageSize)
           .ToListAsync();
        return procedures;
    }

    public async Task<List<Procedure>> GetAllFilteredProcedures(string? DoctorId, string? AssistantId, DateTime? from, DateTime? to,
        int? minNumberOfAssistants, int? maxNumberOfAssistants, string? doctorName, List<string>? assistantNames,
        string? clinicName, string? clinicAddress)
    {
        var query = dbContext.Procedures
            .AsQueryable();
        if (!string.IsNullOrEmpty(DoctorId))
        {
            query = query.Where(p => p.DoctorId == DoctorId).Include(p => p.Doctor).ThenInclude(d => d.Clinic);
        }
        if (!string.IsNullOrEmpty(AssistantId))
        {
            query = query
                .Where(p => p.Status == EnumProcedureStatus.CANCELLED)
                .Where(p => p.Status == EnumProcedureStatus.DECLINED)
                .Where(p => p.Status == EnumProcedureStatus.IN_PROGRESS)
                .Where(p => p.Status == EnumProcedureStatus.DONE)
                .Where(p => p.AssistantsInProcedure.Any(x => x.AsisstantId == AssistantId));
        }
        if (from != null)
        {
            query = query.Where(pro => pro.Date >= from);
        }
        if (to != null)
        {
            query = query.Where(pro => pro.Date <= to);
        }
        if (minNumberOfAssistants != null)
        {
            query = query.Where(pro => pro.NumberOfAsisstants >= minNumberOfAssistants);
        }
        if (maxNumberOfAssistants != null)
        {
            query = query.Where(pro => pro.NumberOfAsisstants <= maxNumberOfAssistants);
        }
        if (!string.IsNullOrWhiteSpace(doctorName))
        {
            query = query.Where(pro => pro.Doctor.UserName!.Contains(doctorName));
        }
        if (assistantNames?.Count > 0)
        {
            var validNames = assistantNames.Where(name => !string.IsNullOrWhiteSpace(name)).ToList();

            if (validNames.Any())
            {
                query = query
                    .Where(pro => pro.AssistantsInProcedure != null && pro.AssistantsInProcedure.Any())
                    .Where(pro => pro.AssistantsInProcedure!
                        .Select(asp => asp.Asisstant.UserName) // Fixed typo (if applicable)
                        .Any(userName => validNames.Contains(userName)));
            }
        }
        if (!string.IsNullOrWhiteSpace(clinicName))
        {
            query = query.Where(pro => pro.Doctor.Clinic!.Name!.Contains(clinicName));
        }
        if (!string.IsNullOrWhiteSpace(clinicAddress))
        {
            query = query.Where(pro => pro.Doctor.Clinic!.Address!.Contains(clinicAddress));
        }
        query = query.Include(pro => pro.Doctor)
            .ThenInclude(doc => doc.Clinic)
            .Include(pro => pro.AssistantsInProcedure!)
                .ThenInclude(asp => asp.Asisstant)
                .AsSplitQuery();

        var procedures = await query.OrderByDescending(p => p.Date).ToListAsync();
        return procedures;
    }

}
