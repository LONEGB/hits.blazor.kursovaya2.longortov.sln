using Microsoft.AspNetCore.Identity;

namespace TovanyUchetV2.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? FullName { get; set; }
    }

}
