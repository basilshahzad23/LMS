using System.ComponentModel.DataAnnotations;

namespace LMS.Model
{
    public class SignUpModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]

        public string Email { get; set; }
        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string UserType { get; set; }
    }
}