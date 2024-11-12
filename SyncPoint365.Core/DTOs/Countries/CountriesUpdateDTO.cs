using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPoint365.Core.DTOs.Countries
{
    public class CountriesUpdateDTO : BaseUpdateDTO
    {
        public string Name { get; set; } = default!;
        public string Capital { get; set; } = default!;
        public string Continent { get; set; } = default!;
    }
}
