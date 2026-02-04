using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;


namespace YemenSchoolsV1.Application.Services.Implementations
{
    public class SchoolNewsService : ISchoolNewsService
    {

        #region filed
        private readonly ISchoolNewsRepository schoolNewsRepository;

        #endregion

        #region constractor
     
        public SchoolNewsService(ISchoolNewsRepository schoolNewsRepository)
        {
            this.schoolNewsRepository = schoolNewsRepository;
        }
        #endregion
        public async Task<SchoolNews?> CreateSchoolNewsAsync(SchoolNews news)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news));
            }
            return await schoolNewsRepository.AddAsync(news);
        }

        public async Task<bool> DeleteSchoolNewsAsync(Guid id)
        {
            var news = await schoolNewsRepository.GetByIdAsync(id);
            if (news == null)
                return false;
            return await schoolNewsRepository.DeleteAsync(id);
        }

        public async Task<SchoolNews?> EditSchoolNewsAsync(Guid id, SchoolNews news)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news));
            }
            var existingNews = await schoolNewsRepository.GetByIdAsync(id);
            if (existingNews == null) { return null; }
            return await schoolNewsRepository.UpdateAsync(id, news);
        }

        public async Task<List<SchoolNews>> GetAllSchoolNewsAsync()
        {
            return await schoolNewsRepository.GetAllAsync();
        }

        public async Task<SchoolNews?> GetSchoolNewsDetailsAsync(Guid id)
        {
            return await schoolNewsRepository.GetByIdAsync(id);
        }

        public async Task<List<SchoolNews>?> GetSchoolNewsDetailsBySchoolIdAsync(Guid id)
        {
            return await schoolNewsRepository.GetSchoolNewsBySchoolIdAsync(id);
        }
    }
}
