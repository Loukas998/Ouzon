﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Tools.Commands.Create;

public class CreateToolCommand :ICommand<int>
{
    public string Name { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Thickness { get; set; }
    public int Quantity { get; set; }
    public int? KitId { get; set; }
    public int? CategoryId { get; set; }
}
