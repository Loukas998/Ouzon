using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Procedure.Dtos.MainProcedure;
using Template.Application.Users.Dtos;
using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Application.Ratings.Dtos
{
    public class RatingsDto
    {
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public int ProcedureId { get; set; }
        public ProcedureSummaryDto Procedure { get; set; }
        public UserDto Doctor { get; set; }
    }
}
