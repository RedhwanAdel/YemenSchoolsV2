using FinalProject.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IRegionRepositry : IGenericRepositoryAsync<Region>
    {
        Task<List<Region>?> GetRegionByCityIdIncludeAsync(Guid cityId);
        Task<int?> GetSchoolCount(Guid regionId);

    }
}
