using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.Users;
using Template.Domain.Repositories;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Repositories;

public class RatingRepository(TemplateDbContext dbContext) : GenericRepository<Rating>(dbContext), IRatingsRepository
{
}
