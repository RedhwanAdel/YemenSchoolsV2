using Microsoft.AspNetCore.Identity;

namespace YemenSchoolsV1.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? SchoolId { get; set; }


        public Guid EntityId { get; set; } // المفتاح الخارجي: يربط AppUser بـ Parent.Id أو Student.Id أو Teacher.Id
        public string UserType { get; set; } // "Parent", "Student", "Teacher", "Admin", "SuperAdmin"
        public Parent? ParentEntity { get; set; } // يربط هذا المستخدم بكيان ولي الأمر إذا كان UserType = 'Parent'
        public Student? StudentEntity { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; } = [];

    }
}
