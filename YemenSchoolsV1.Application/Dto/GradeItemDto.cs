namespace YemenSchoolsV1.Application.Dto
{
    public class GradeItemDto
    {
        public string Type { get; set; } = null!; // نوع التقييم، مثال: اختبار أول
        public double Score { get; set; }       // الدرجة الفعلية
        public double Total { get; set; }       // الحد الأقصى للدرجة
        public string Percentage { get; set; } = null!; // النسبة %
    }
}
