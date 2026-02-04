using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface ITermRepository : IGenericRepositoryAsync<Term>
    {
        Task<Term?> GetTermByIdIncludeAsync(Guid id);
        Task<List<Term>> GetTermByYearIdAsync(Guid id);


    }
}
