using FinalProject.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Helpers;
using YemenSchoolsV1.Application.Wrappers;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
	public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
	{
		#region Vars / Props

		private readonly YemenShoolsDbContext _dbContext;

		public GenericRepositoryAsync(YemenShoolsDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		#endregion Vars / Props

		#region Methods

		public async Task<bool> IsExist(Guid id)
		{
			var rsult = await _dbContext.Set<T>().FindAsync(id);
			return rsult != null;
		}
		public async Task<T?> GetByIdAsync(Guid id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync(); ;
		}

		public IQueryable<T> GetTableAsTracking()
		{
			return _dbContext.Set<T>().AsQueryable();
		}

		public IQueryable<T> GetTableNoTracking()
		{
			return _dbContext.Set<T>().AsNoTracking().AsQueryable();
		}

		public async Task<T> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();

			return entity;
		}

		public async Task<T?> UpdateAsync(Guid id, T entity)
		{
			var entityDb = await _dbContext.Set<T>().FindAsync(id);

			if (entityDb == null)
			{
				return null;
			}

			// إذا كان الكائن موجود، نقوم بتحديث القيم
			_dbContext.Entry(entityDb).CurrentValues.SetValues(entity);

			await _dbContext.SaveChangesAsync();

			return entityDb;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var entity = await _dbContext.Set<T>().FindAsync(id);

			if (entity == null)
			{
				return false;
			}

			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
			return true;
		}

		public void Commit()
		{
			_dbContext.Database.CommitTransaction();
		}

		public void RollBack()
		{
			_dbContext.Database.RollbackTransaction();
		}

		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}

		public async Task<List<T>> GetAllBySchoolIdAsync(Guid schoolId)
		{
			if (typeof(ISchoolEntity).IsAssignableFrom(typeof(T)))
			{
				return await _dbContext.Set<T>()
					.Where(e => ((ISchoolEntity)e).SchoolId == schoolId)
					.ToListAsync();
			}
			throw new InvalidOperationException($"Entity {typeof(T).Name} does not implement ISchoolEntity.");
		}

		public async Task<PaginatedResult<T>> GetPagedAsync(PaginationQuery paginationQuery,
					Expression<Func<T, bool>>? predicate = null,
					Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
					List<Expression<Func<T, object>>>? includes = null)
		{
			IQueryable<T> query = _dbContext.Set<T>();

			if (includes != null)
			{
				query = includes.Aggregate(query, (current, include) => current.Include(include));
			}

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			int totalRecords = await query.CountAsync();

			if (orderBy != null)
			{
				query = orderBy(query);
			}

			var data = await query
				.Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
				.Take(paginationQuery.PageSize)
				.ToListAsync();



			return PaginatedResult<T>.Success(
				data,
				totalRecords,
				paginationQuery.PageNumber,
				paginationQuery.PageSize);
		}



		#endregion Methods
	}
}