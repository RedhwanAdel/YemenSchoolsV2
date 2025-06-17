using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface ITermService
    {
        public Task<List<Term>> GetAllTermsAsync();
        public Task<Term?> GetTermDetailsAsync(Guid id);
        public Task<Term?> CreateTermAsync(Term term);
        public Task<Term?> EditTermAsync(Guid id, Term term);
        public Task<bool> DeleteTermAsync(Guid id);
    }
}
