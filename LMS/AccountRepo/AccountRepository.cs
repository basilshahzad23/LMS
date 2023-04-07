
using LMS.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS.AccountRepo
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            try
            {
                var user = new ApplicationUser()
                {

                    FirstName = signUpModel.FirstName,
                    LastName = signUpModel.LastName,
                    Email = signUpModel.Email,
                    UserName = signUpModel.Email,
                    MobileNumber = signUpModel.MobileNumber,
                    UserType = signUpModel.UserType

                };
                return await _userManager.CreateAsync(user, signUpModel.Password);
            }
            catch (Exception)
            {

                throw;
            }



        }

        public async Task<UserModel> LoginAsync(SignInModel signInModel)
        {

            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, true);
            if (!result.Succeeded)
            {


                return null;


            }
            var user = await _userManager.FindByEmailAsync(signInModel.Email);



            //if (user.UserType == "Admin")
            //{

            var authClaims = new List<Claim> {



                new Claim(ClaimTypes.Name,signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };
            var authySigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(

                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authySigninKey, SecurityAlgorithms.HmacSha256Signature)


                );



            var _token = new JwtSecurityTokenHandler().WriteToken(token);
            UserModel _user = new UserModel();
            _user.FirstName = user.FirstName;
            _user.LastName = user.LastName;
            _user.MobileNumber = user.MobileNumber;
            _user.UserType = user.UserType;
            _user.Token = _token;
            _user.Email = user.Email;
            _user.UserID = user.Id;

            return _user;

            //return new JwtSecurityTokenHandler().WriteToken(token);

            //}
            //else
            // {

            //   return null;

            //}


        }



        public async Task<string> ChangePassword(ChangePasswordModel changePasswordModel)
        {



            return null;
        }





        public async Task<UserModel> GetUserAsync(SignInModel signInModel)
        {

            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, true);
            if (!result.Succeeded)
            {


                return null;


            }
            var _applicationUser = await _userManager.FindByEmailAsync(signInModel.Email);

            UserModel user = new UserModel();
            user.FirstName = _applicationUser.FirstName;
            user.LastName = _applicationUser.LastName;
            user.MobileNumber = _applicationUser.MobileNumber;

            return user;


        }

        public async Task<string> ALoginAsync(SignInModel signInModel)
        {

            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, false, true);
            if (!result.Succeeded)
            {


                return null;


            }
            var user = await _userManager.FindByEmailAsync(signInModel.Email);



            if (user.UserType == "Admin")
            {

                var authClaims = new List<Claim> {



                new Claim(ClaimTypes.Name,signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };
                var authySigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(

                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authySigninKey, SecurityAlgorithms.HmacSha256Signature)


                    );

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            else
            {

                return null;

            }






        }

    }





}