using YemenSchoolsV1.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface ISchoolNewsRepository : IGenericRepositoryAsync<SchoolNews>
    {
        Task<List<SchoolNews>?> GetSchoolNewsBySchoolIdAsync(Guid schoolId);
    }
}
