using LMS.AccountRepo;
using LMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            var result = await _accountRepository.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {

                return Ok(result.Succeeded);


            }
            return Unauthorized();

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInModel signInModel)
        {
            var result = await _accountRepository.LoginAsync(signInModel);
            // if (String.IsNullOrEmpty(result))

            if (result == null)


            {

                return Unauthorized();


            }

            return Ok(result);

        }


        [HttpPost("GetUser")]
        public async Task<IActionResult> GetUser([FromBody] SignInModel signInModel)
        {
            var result = await _accountRepository.GetUserAsync(signInModel);
            if (result == null)
            {

                return Unauthorized();

            }
            return Ok(result);

        }



        [HttpPost("Alogin")]
        public async Task<IActionResult> ALogin([FromBody] SignInModel signInModel)
        {
            var result = await _accountRepository.ALoginAsync(signInModel);
            if (String.IsNullOrEmpty(result))
            {

                return Unauthorized();


            }



            return Ok(result);

        }


    }
}