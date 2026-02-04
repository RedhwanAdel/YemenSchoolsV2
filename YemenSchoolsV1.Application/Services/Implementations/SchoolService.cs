using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Services.Implementations
{
	public class SchoolService : ISchoolService
	{
		#region filed
		private readonly ISchoolRepository schoolRepository;

		#endregion

		#region constractor
		public SchoolService(ISchoolRepository schoolRepository)
		{
			this.schoolRepository = schoolRepository;
		}
		#endregion

		#region handel acrtions



		public async Task<List<School>> GetAllSchoolsAsync()
		{
			return await schoolRepository.GetAllAsync();
		}

		public async Task<School?> GetSchoolDetailsAsync(Guid id)
		{
			return await schoolRepository.GetByIdAsync(id);
		}
		public async Task<School?> GetSchoolDetailsIncludeAsync(Guid id)
		{
			return await schoolRepository.GetSchoolDetailsInculdeAsync(id);
		}
		public async Task<School?> CreateSchoolAsync(School school)
		{
			if (school == null)
			{
				throw new ArgumentNullException(nameof(school));
			}
			return await schoolRepository.AddAsync(school);
		}

		public async Task<School?> EditSchoolAsync(Guid id, School school)
		{
			if (school == null)
			{
				throw new ArgumentNullException(nameof(school));
			}
			var existingSchool = await schoolRepository.GetByIdAsync(id);
			if (existingSchool == null) { return null; }
			return await schoolRepository.UpdateAsync(id, school);
		}

		public async Task<bool> DeleteSchoolAsync(Guid id)
		{
			var school = await schoolRepository.GetByIdAsync(id);
			if (school == null)
				return false;
			return await schoolRepository.DeleteAsync(id);
		}

		//public IQueryable<School> FilterSchoolPaginatedQuerable(SchoolOrdering orderingEnum, string sortDirection, string search, Guid? cityId, Guid? regionId)
		//{
		//	var queryable = schoolRepository.GetSchoolsWithCityAndRegionQueryable();



		//	if (cityId != Guid.Empty && cityId != null)
		//	{
		//		queryable = queryable.Where(x => x.CityId == cityId);
		//	}
		//	if (regionId != Guid.Empty && regionId != null)
		//	{
		//		queryable = queryable.Where(x => x.RegionId == regionId);
		//	}
		//	if (!string.IsNullOrEmpty(search))
		//	{
		//		queryable = queryable.Where(x => x.NameAr.ToLower().Contains(search.ToLower()));
		//	}
		//	if (type.HasValue)
		//	{
		//		queryable = queryable.Where(x => x.Type == type.Value);
		//	}

		//	if (levels.HasValue)
		//	{
		//		queryable = queryable.Where(x => (x.Levels & levels.Value) != 0); // [Flags] filter
		//	}

		//	if (gender.HasValue)
		//	{
		//		queryable = queryable.Where(x => x.Gender == gender.Value);
		//	}

		//	switch (sortDirection?.Trim().ToLower())
		//	{
		//		case "asc":
		//			queryable = orderingEnum switch
		//			{
		//				SchoolOrdering.Name => queryable.OrderBy(x => x.NameEn),
		//				SchoolOrdering.city => queryable.OrderBy(x => x.City.NameEn),
		//				SchoolOrdering.region => queryable.OrderBy(x => x.Region.NameEn),
		//				_ => queryable.OrderBy(x => x.NameEn)
		//			};
		//			break;

		//		case "desc":
		//			queryable = orderingEnum switch
		//			{
		//				SchoolOrdering.Name => queryable.OrderByDescending(x => x.NameEn),
		//				SchoolOrdering.city => queryable.OrderByDescending(x => x.City.NameEn),
		//				SchoolOrdering.region => queryable.OrderByDescending(x => x.Region.NameEn),
		//				_ => queryable.OrderByDescending(x => x.NameEn)
		//			};
		//			break;

		//		default:
		//			queryable = queryable.OrderBy(x => x.NameEn);
		//			break;
		//	}


		//	// إرجاع الاستعلام
		//	return queryable;

		//}

		public async Task CreateSchoolPhonsRangAsync(List<SchoolPhone> schoolPhones)
		{
			await schoolRepository.CreateSchoolPhonesRangAsync(schoolPhones);
		}



		#endregion
	}
}
