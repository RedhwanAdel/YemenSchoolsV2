using FinalProject.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface ITermRepositry : IGenericRepositoryAsync<Term>
    {
        Task<Term?> GetTermByIdIncludeAsync(Guid id);
        Task<List<Term>> GetTermByYearIdAsync(Guid id);


    }
}
