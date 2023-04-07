using LMS.Model;
using LMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookTypeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookTypeRepository _bookTypeRepository;

        public BookTypeController(IBookTypeRepository bookTypeRepository, UserManager<ApplicationUser> userManager)
        {

            this._bookTypeRepository = bookTypeRepository;

            _userManager = userManager;



        }

        [HttpPost("AddNew")]
        [Authorize]
        public async Task<IActionResult> AddNew([FromBody] BookTypeVM bookTypeVM)
        {
            try
            {

                var rec = await _bookTypeRepository.AddBookTypeAsync(bookTypeVM);

                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var records = await _bookTypeRepository.GetAllBookTypeAsync();
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
        public async Task<IActionResult> GetPagedBookTypes(int page, int pageSize)
        {
            try
            {
                var records = await _bookTypeRepository.GetAllBookTypePagedAsync(page, pageSize);
                return Ok(records);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }



        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] BookTypeVM bookTypeVM)
        {
            try
            {
                var rec = await _bookTypeRepository.EditBookTypeAsync(bookTypeVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }



        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] BookTypeVM bookTypeVM)
        {
            try
            {
                var rec = await _bookTypeRepository.DeleteBookTypeAsync(bookTypeVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }
    }

}

