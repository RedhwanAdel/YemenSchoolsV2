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
    public class TermService : ITermService
    {
        private readonly ITermRepositry termRepositry;

        public TermService(ITermRepositry termRepositry)
        {
            this.termRepositry = termRepositry;
        }
        public async Task<Term?> CreateTermAsync(Term term)
        {
            if (term == null)
            {
                throw new ArgumentNullException(nameof(term));
            }
            return await termRepositry.AddAsync(term);
        }

        public async Task<bool> DeleteTermAsync(Guid id)
        {
            var term = await termRepositry.GetByIdAsync(id);
            if (term == null)
                return false;
            return await termRepositry.DeleteAsync(id);
        }

        public async Task<Term?> EditTermAsync(Guid id, Term term)
        {
            if (term == null)
            {
                throw new ArgumentNullException(nameof(term));
            }
            var existingTerm = await termRepositry.GetByIdAsync(id);
            if (existingTerm == null) { return null; }
            return await termRepositry.UpdateAsync(id, term);
        }

        public async Task<List<Term>> GetAllTermsAsync()
        {
            return await termRepositry.GetAllAsync();
        }

        public async Task<Term?> GetTermDetailsAsync(Guid id)
        {
            return await termRepositry.GetByIdAsync(id);
        }
    }
}
