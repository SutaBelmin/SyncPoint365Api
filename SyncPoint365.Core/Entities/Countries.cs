using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPoint365.Core.Entities
{
    public class Countries : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Capital { get; set; } = default!;
        public string Continent { get; set; } = default!;
    }
}
