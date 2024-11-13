using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPoint365.Core.DTOs.Cities
{
    public class CityUpdateDTO : BaseUpdateDTO
    {
        public string Name { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public int? PostalCode { get; set; }
        public int CountryId { get; set; }
    }
}
