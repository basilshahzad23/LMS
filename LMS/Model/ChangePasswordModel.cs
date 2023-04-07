using System.ComponentModel.DataAnnotations;

namespace LMS.Model
{
    public class ChangePasswordModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string ReEnterCurrentPassword { get; set; }

    }
}