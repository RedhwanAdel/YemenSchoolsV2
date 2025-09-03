namespace YemenSchoolsV1.Domain.Entities
{
    public class SchoolReview
    {
        public Guid Id { get; set; }

        // العلاقة مع المدرسة
        public Guid SchoolId { get; set; }
        public School School { get; set; }

        // العلاقة مع المستخدم
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        // التقييم الأساسي
        public int Rating { get; set; } // 1 - 5 نجوم
        public string? Comment { get; set; }

        // تواريخ
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

}
