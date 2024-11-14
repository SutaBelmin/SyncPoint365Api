using SyncPoint365.Core.DTOs.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPoint365.Core.DTOs.Cities
{
    public class CityDTO : BaseDTO
    { 
        public string Name { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public int? PostalCode { get; set; }

        public CountryDTO Country { get; set; } = default!;
        public int CountryId { get; set; }
    }
}
