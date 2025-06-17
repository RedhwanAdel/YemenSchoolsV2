using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface ISectionService
    {
        public Task<List<Section>> GetAllSectionsAsync();
        public Task<Section?> GetSectionDetailsAsync(Guid id);
        public Task<Section?> CreateSectionAsync(Section section);
        public Task<Section?> EditSectionAsync(Guid id, Section section);
        public Task<bool> DeleteSectionAsync(Guid id);
    }
}
