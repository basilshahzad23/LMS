using LMS.Data;
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
    public class BookController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository, UserManager<ApplicationUser> userManager)
        {

            this._bookRepository = bookRepository;

            _userManager = userManager;



        }

        [HttpPost("AddNew")]
        [Authorize]
        public async Task<IActionResult> AddNew([FromBody] BookVM bookVM)
        {
            try
            {

                var rec = await _bookRepository.AddBookAsync(bookVM);

                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<IActionResult> Get([FromBody] BookVM bookVM)
        {
            try
            {
                var records = await _bookRepository.GetAllBookAsync();
                return Ok(records);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }

        }
        [Route("[action]/{title}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetByBookTitle(string title)
        {
            try
            {//Task<BookVM> GetBookByTitleAsync(string Title);
                var records = await _bookRepository.GetBookByTitleAsync(title);
                return Ok(records);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }

        }



        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] BookVM bookVM)
        {
            try
            {
                var rec = await _bookRepository.EditBookAsync(bookVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }



        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] BookVM bookVM)
        {
            try
            {
                var rec = await _bookRepository.DeleteBookAsync(bookVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }

        }
    }
}