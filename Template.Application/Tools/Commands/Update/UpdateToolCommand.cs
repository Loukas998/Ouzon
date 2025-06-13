using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Tools.Dtos;

namespace Template.Application.Tools.Commands.Update;

public class UpdateToolCommand :IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Thickness { get; set; }
    public int Quantity { get; set; }
    public int? KitId { get; set; }
    public int? CategoryId { get; set; }
}
