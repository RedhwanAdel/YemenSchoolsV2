using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface ISchoolNewsService
    {
        public Task<List<SchoolNews>> GetAllSchoolNewsAsync();
        public Task<SchoolNews?> GetSchoolNewsDetailsAsync(Guid id);
        public Task<List<SchoolNews>?> GetSchoolNewsDetailsBySchoolIdAsync(Guid id);
        public Task<SchoolNews?> CreateSchoolNewsAsync(SchoolNews news);
        public Task<SchoolNews?> EditSchoolNewsAsync(Guid id, SchoolNews news);
        public Task<bool> DeleteSchoolNewsAsync(Guid id);
    }
}
