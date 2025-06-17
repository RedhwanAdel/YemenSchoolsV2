using YemenSchoolsV1.Domain.Commons;

namespace YemenSchoolsV1.Domain.Entities
{
	public class City : GeneralLocalizableEntity
	{
		public Guid Id { get; set; }
		public string NameAr { get; set; }
		public string NameEn { get; set; }
		public string? ImageUrl { get; set; }
		public ICollection<Region> Regions { get; set; } = [];
		public ICollection<School> Schools { get; set; } = [];
	}
}
