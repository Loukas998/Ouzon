using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Kits.Dtos;
using Template.Domain.Enums;

namespace Template.Application.Procedure.Dtos;

public class ProcedureKitDetailsDto
{
    public IEnumerable<KitDto>? MainKits { get; set; } = [];
    public IEnumerable<KitDto>? KitsWithImplants { get; set; } = [];
    public IEnumerable<KitDto>? KitsWithoutImplants { get; set; } = [];
}
