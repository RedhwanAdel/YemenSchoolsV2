namespace YemenSchoolsV1.Application.Contracts.Persistence
{
    public interface IDashboardRepository
    {
        Task<DashboardDto> GetDashboardAsync();
    }
}
