﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Holidays.Dtos;
using Template.Application.Procedure.Dtos.MainProcedure;
using Template.Domain.Entities.Notifications;
using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Application.Users.Dtos
{
   public class UserDetailedDto
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = "User";
        public List<ProcedureSummaryDto>? InProcedure { get; set; }
        public List<ProcedureSummaryDto>? ProcedureFrom { get; set; }
        public List<HolidayDto>? Holidays { get; set; }
        public List<Device>? Devices { get; set; }
    }
}
