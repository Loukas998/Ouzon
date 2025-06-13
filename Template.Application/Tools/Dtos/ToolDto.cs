using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Kits.Dtos;

namespace Template.Application.Tools.Dtos
{
   public class ToolDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Thickness { get; set; }
        public int Quantity { get; set; }
        public int? KitId { get; set; }
        public int? CategoryId { get; set; }

        public KitDto? Kit { get; set; }
    }
}
