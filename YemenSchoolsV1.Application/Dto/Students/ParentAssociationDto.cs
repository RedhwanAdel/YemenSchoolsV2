namespace YemenSchoolsV1.Application.Dto.Students
{
    public class ParentAssociationDto
    {
        public required Guid ParentId { get; set; }

        /// <summary>
        /// نوع العلاقة بين ولي الأمر والطالب (مثل "Father", "Mother", "Guardian").
        /// </summary>
        public required string RelationType { get; set; }
    }
}
