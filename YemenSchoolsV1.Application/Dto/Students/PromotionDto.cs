namespace YemenSchoolsV1.Application.Dto.Students
{
    public class PromotionDto
    {

        public List<Guid> StudentIds { get; set; } = [];
        public Guid NewSectionId { get; set; }

    }
}
