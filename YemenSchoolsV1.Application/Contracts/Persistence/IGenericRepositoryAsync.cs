using System.Linq.Expressions;
using YemenSchoolsV1.Application.Helpers;
using YemenSchoolsV1.Application.Wrappers;

namespace FinalProject.Application.Contracts.Persistence
{
	public interface IGenericRepositoryAsync<T> where T : class
	{
		Task<bool> IsExist(Guid id);
		Task<PaginatedResult<T>> GetPagedAsync(PaginationQuery paginationQuery,
					Expression<Func<T, bool>>? predicate,
					Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
					List<Expression<Func<T, object>>>? includes = null);
		Task<List<T>> GetAllAsync();

		Task<T?> GetByIdAsync(Guid id);

		Task SaveChangesAsync();

		void Commit();

		void RollBack();

		IQueryable<T> GetTableNoTracking();

		IQueryable<T> GetTableAsTracking();

		Task<T> AddAsync(T entity);

		Task<T?> UpdateAsync(Guid id, T entity);

		Task<bool> DeleteAsync(Guid id);

		Task<List<T>> GetAllBySchoolIdAsync(Guid schoolId);

		//      Task UpdateRangeAsync(ICollection<T> entities);
		//     Task AddRangeAsync(ICollection<T> entities);
		//    Task DeleteRangeAsync(ICollection<T> entities);
		// IDbContextTransaction BeginTransaction();
	}
}