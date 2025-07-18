using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Domain.Entities.Users
{
   public class Rating

    {
        public int Id { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public int ProcedureId { get; set; }
        public Procedure Procedure { get; set; }
    }
}
