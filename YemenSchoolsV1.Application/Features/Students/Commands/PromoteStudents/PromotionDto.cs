namespace YemenSchoolsV1.Application.Features.Students.Commands.PromoteStudents
{
    public class PromotionDto
    {

        public List<Guid> StudentIds { get; set; } = [];
        public Guid NewSectionId { get; set; }

    }
}
