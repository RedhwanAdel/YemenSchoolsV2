namespace YemenSchoolsV1.Application.Contracts.Persistence
{
	//this just for mark the tabels that use or need getbyschoolid
	public interface ISchoolEntity
	{
		Guid SchoolId { get; set; }
	}
}