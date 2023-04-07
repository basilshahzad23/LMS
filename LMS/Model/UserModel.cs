namespace LMS.Model
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string UserType { get; set; }
        public string Email { get; set; }
        public string UserID { get; set; }
        public string Token { get; set; } = "";

    }
}