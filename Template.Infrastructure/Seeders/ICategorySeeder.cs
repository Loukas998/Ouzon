using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Infrastructure.Seeders
{
   public interface ICategorySeeder
    {
        public Task Seed();
    }
}
