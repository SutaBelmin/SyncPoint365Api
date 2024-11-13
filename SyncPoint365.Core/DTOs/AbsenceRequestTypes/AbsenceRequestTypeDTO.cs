using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPoint365.Core.DTOs.AbsenceRequestTypes
{
    public class AbsenceRequestTypeDTO : BaseDTO
    {
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
