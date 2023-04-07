using LMS.Model;
using LMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository, UserManager<ApplicationUser> userManager)
        {

            this._authorRepository = authorRepository;

            _userManager = userManager;



        }

        [HttpPost("AddNew")]
        [Authorize]
        public async Task<IActionResult> AddNew([FromBody] AuthorVM authorVM)
        {
            try
            {

                var rec = await _authorRepository.AddAuthorAsync(authorVM);

                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<IActionResult> Get([FromBody] AuthorVM authorVM)
        {
            try
            {
                var records = await _authorRepository.GetAllAuthorsAsync();
                return Ok(records);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }

        }



        [Route("[action]/{page}, {pageSize}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPagedAuthors(int page, int pageSize)
        {
            try
            {
                var records = await _authorRepository.GetAllAuthorsPagedAsync(page, pageSize);
                return Ok(records);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }



        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] AuthorVM authorVM)
        {
            try
            {
                var rec = await _authorRepository.EditAuthorAsync(authorVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }



        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] AuthorVM authorVM)
        {
            try
            {
                var rec = await _authorRepository.DeleteAuthorAsync(authorVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }
    }
}