using Microsoft.AspNetCore.Identity;

namespace HealthifyMeFinalProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] ProfilePicture { get; set; }
    }
}
