using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Identity.Entities
{
    public class AppUser : IdentityUser
    {
        // extra Property added

        public string? FileName { get; set;}
        public string? FileType { get; set; }
    }
}
