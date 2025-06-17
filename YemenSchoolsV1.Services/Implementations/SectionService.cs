using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Repositories;

namespace YemenSchoolsV1.Services.Implementations
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepositry sectionRepositry;

        public SectionService(ISectionRepositry sectionRepositry)
        {
            this.sectionRepositry = sectionRepositry;
        }
        public async Task<Section?> CreateSectionAsync(Section section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }
            return await sectionRepositry.AddAsync(section);
        }

        public async Task<bool> DeleteSectionAsync(Guid id)
        {
            var section = await sectionRepositry.GetByIdAsync(id);
            if (section == null)
                return false;
            return await sectionRepositry.DeleteAsync(id);
        }

        public async Task<Section?> EditSectionAsync(Guid id, Section section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }
            var existingSection = await sectionRepositry.GetByIdAsync(id);
            if (existingSection == null) { return null; }
            return await sectionRepositry.UpdateAsync(id, section);
        }

        public async Task<List<Section>> GetAllSectionsAsync()
        {
            return await sectionRepositry.GetAllAsync();
        }

        public async Task<Section?> GetSectionDetailsAsync(Guid id)
        {
            return await sectionRepositry.GetByIdAsync(id);
        }
    }
}
