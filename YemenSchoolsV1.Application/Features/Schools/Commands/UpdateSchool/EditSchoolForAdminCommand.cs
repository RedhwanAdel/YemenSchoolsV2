using FinalProject.Application.Bases;
using MediatR;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Schools.Commands.UpdateSchool
{
    public class EditSchoolForAdminCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string AddressAr { get; set; }
        public string AddressEn { get; set; }
        public string? PostalCode { get; set; } // الرمز البريدي
        public string MainPhone { get; set; }
        public string Email { get; set; }
        public SchoolType SchoolType { get; set; } // نوع المدرسة (خاصة، حكومية، دولية...)

        public GenderType GenderType { get; set; } //  فئة الطلاب ,بنين, بنات بنين
        public CurriculumType CurriculumType { get; set; } // نوع المنهج
        public SchoolLevel SchoolLevel { get; set; } // مستوى المراحل الدراسية

        public Guid CityId { get; set; } // معرف المدينة
        public Guid RegionId { get; set; } // معرف المنطقة
    }
}
