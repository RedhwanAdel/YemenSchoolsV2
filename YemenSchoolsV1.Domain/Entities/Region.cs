using YemenSchoolsV1.Domain.Commons;

namespace YemenSchoolsV1.Domain.Entities
{
	public class Region : GeneralLocalizableEntity
	{
		public Guid Id { get; set; }
		public string NameAr { get; set; }
		public string NameEn { get; set; }
		public string? ImageUrl { get; set; }

		public Guid CityId { get; set; }
		public City City { get; set; } = null!;
		public ICollection<School> Schools { get; set; } = [];

	}
}
