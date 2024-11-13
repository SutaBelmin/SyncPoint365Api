using SyncPoint365.Core.DTOs.Cities;
using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface ICitiesService : IBaseService<CityDTO, CityAddDTO, CityUpdateDTO>
    {
        Task<IEnumerable<CityDTO>> GetCitiesListAsync(CancellationToken cancellationToken = default);
    }
}
