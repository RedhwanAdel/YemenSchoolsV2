public class DashboardDto
{
    public DashboardSummaryDto Summary { get; set; }
    public List<TopSchoolDto> TopSchoolsByStudents { get; set; }
    public List<TopSchoolDto> TopSchoolsByTeachers { get; set; }
    public List<StudentGrowthDto> StudentGrowthLast6Months { get; set; }
    public List<RecentActivityDto> RecentActivities { get; set; }
}

public class DashboardSummaryDto
{
    public int TotalCities { get; set; }
    public int TotalRegions { get; set; }
    public int TotalSchools { get; set; }
    public int ActiveSchools { get; set; }
    public int TotalTeachers { get; set; }
    public int TotalStudents { get; set; }
    public int TotalUsers { get; set; }
    public string CurrentAcademicYear { get; set; }
}

public class TopSchoolDto
{
    public string SchoolName { get; set; }
    public int Count { get; set; } // يمكن تمثل عدد الطلاب أو المعلمين
}

public class StudentGrowthDto
{
    public string Month { get; set; }
    public int Students { get; set; }
}

public class RecentActivityDto
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string Action { get; set; }
    public DateTime Date { get; set; }
}
