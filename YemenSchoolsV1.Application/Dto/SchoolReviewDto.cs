namespace YemenSchoolsV1.Application.Dto
{
    public class SchoolReviewDto
    {
        public Guid Id { get; set; }

        public Guid SchoolId { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public int Rating { get; set; } // 1 - 5 نجوم
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
