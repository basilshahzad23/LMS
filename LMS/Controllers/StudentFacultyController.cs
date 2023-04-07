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
    public class StudentFacultyController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStudentFacultyRepository _studentFacultyRepository;

        public StudentFacultyController(IStudentFacultyRepository studentFacultyRepository, UserManager<ApplicationUser> userManager)
        {

            this._studentFacultyRepository = studentFacultyRepository;

            _userManager = userManager;



        }

        [HttpPost("AddNew")]
        [Authorize]
        public async Task<IActionResult> AddNew([FromBody] StudentFacultyVM StudentVM)
        {
            try
            {

                var rec = await _studentFacultyRepository.AddStudent_FacultyAsync(StudentVM);

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
                var records = await _studentFacultyRepository.GetAllStudent_FacultyAsync();
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
        public async Task<IActionResult> GetPagedStudent_Facultys(int page, int pageSize)
        {
            try
            {
                var records = await _studentFacultyRepository.GetAllStudent_FacultyPagedAsync(page, pageSize);
                return Ok(records);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }






        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] StudentFacultyVM StudentVM)
        {
            try
            {
                var rec = await _studentFacultyRepository.EditStudent_FacultyAsync(StudentVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }



        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] StudentFacultyVM StudentVM)
        {
            try
            {
                var rec = await _studentFacultyRepository.DeleteStudent_FacultyAsync(StudentVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }
    }
}