using Microsoft.AspNetCore.Identity;
namespace LMS.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string UserType { get; set; }

    }
}
