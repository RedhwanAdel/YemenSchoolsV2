using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace YemenSchoolsV1.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? SchoolId { get; set; }


        public Guid EntityId { get; set; } // المفتاح الخارجي: يربط AppUser بـ Parent.Id أو Student.Id أو Teacher.Id
        public string UserType { get; set; } // "Parent", "Student", "Teacher", "Admin", "SuperAdmin"

        public ICollection<AppUserRole> UserRoles { get; set; } = [];
        [JsonIgnore]
        public List<Message> MessagesSent { get; set; } = [];

        [JsonIgnore]
        public List<Message> MessagesReceived { get; set; } = [];



    }
}
