using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Enums;

namespace YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolDetails
{
    public class GetSchoolDetailsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public string? Logo { get; set; }
        public string? CoverImage { get; set; }
        public string? PostalCode { get; set; } // الرمز البريدي
        public string MainPhone { get; set; }
        public string Email { get; set; }
        public string SchoolType { get; set; } // نوع المدرسة (خاصة، حكومية، دولية...)
        public bool IsActive { get; set; } = true; // حالة المدرسة
        public string GenderType { get; set; } //  فئة الطلاب ,بنين, بنات بنين
        public string CurriculumType { get; set; } // نوع المنهج
        public string SchoolLevel { get; set; } // مستوى المراحل الدراسية
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public List<PhoneResponse> PhoneNumberList { get; set; }

    }
    public class PhoneResponse
    {
        public string PhoneNumber { get; set; }
    }
}
