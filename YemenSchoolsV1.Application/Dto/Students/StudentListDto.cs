namespace YemenSchoolsV1.Application.Dto.Students
{
    public class StudentListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RegisterNo { get; set; }
        public string? GradeName { get; set; }
        public string? SectionName { get; set; }

        public Guid SectionId { get; set; }


    }
}
