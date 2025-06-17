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
    public class SchoolNewsService : ISchoolNewsService
    {

        #region filed
        private readonly ISchoolNewsRepositry schoolNewsRepositry;

        #endregion

        #region constractor
     
        public SchoolNewsService(ISchoolNewsRepositry schoolNewsRepositry)
        {
            this.schoolNewsRepositry = schoolNewsRepositry;
        }
        #endregion
        public async Task<SchoolNews?> CreateSchoolNewsAsync(SchoolNews news)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news));
            }
            return await schoolNewsRepositry.AddAsync(news);
        }

        public async Task<bool> DeleteSchoolNewsAsync(Guid id)
        {
            var news = await schoolNewsRepositry.GetByIdAsync(id);
            if (news == null)
                return false;
            return await schoolNewsRepositry.DeleteAsync(id);
        }

        public async Task<SchoolNews?> EditSchoolNewsAsync(Guid id, SchoolNews news)
        {
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news));
            }
            var existingNews = await schoolNewsRepositry.GetByIdAsync(id);
            if (existingNews == null) { return null; }
            return await schoolNewsRepositry.UpdateAsync(id, news);
        }

        public async Task<List<SchoolNews>> GetAllSchoolNewsAsync()
        {
            return await schoolNewsRepositry.GetAllAsync();
        }

        public async Task<SchoolNews?> GetSchoolNewsDetailsAsync(Guid id)
        {
            return await schoolNewsRepositry.GetByIdAsync(id);
        }

        public async Task<List<SchoolNews>?> GetSchoolNewsDetailsBySchoolIdAsync(Guid id)
        {
            return await schoolNewsRepositry.GetSchoolNewsBySchoolIdAsync(id);
        }
    }
}
