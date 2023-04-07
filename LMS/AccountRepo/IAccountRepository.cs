using LMS.Model;
using Microsoft.AspNetCore.Identity;

namespace LMS.AccountRepo
{
    public interface IAccountRepository
    {

        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<UserModel> LoginAsync(SignInModel signInModel);
        Task<string> ALoginAsync(SignInModel signInModel);
        Task<UserModel> GetUserAsync(SignInModel signInModel);

    }

}