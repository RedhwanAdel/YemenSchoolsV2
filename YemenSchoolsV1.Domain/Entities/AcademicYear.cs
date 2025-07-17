namespace YemenSchoolsV1.Domain.Entities
{
    public class AcademicYear
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // تاريخ الإنشاء
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // تاريخ آخر تعديل
        public bool IsActive { get; set; } = true; // حالة التفعيل (فعال/معطل)

        public Guid StageId { get; set; }

        // Navigation Property
        public Stage Stage { get; set; } = null!; // المدرسة المرتبطة
        public ICollection<Term> Terms { get; set; } = [];
    }
}
