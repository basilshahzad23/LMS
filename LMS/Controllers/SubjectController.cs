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
    public class SubjectController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository, UserManager<ApplicationUser> userManager)
        {

            this._subjectRepository = subjectRepository;

            _userManager = userManager;



        }

        [HttpPost("AddNew")]
        [Authorize]
        public async Task<IActionResult> AddNew([FromBody] SubjectVM subjectVM)
        {
            try
            {

                var rec = await _subjectRepository.AddSubjectAsync(subjectVM);

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
                var records = await _subjectRepository.GetAllSubjectAsync();
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
        public async Task<IActionResult> GetPagedSubjects(int page, int pageSize)
        {
            try
            {
                var records = await _subjectRepository.GetAllSubjectPagedAsync(page, pageSize);
                return Ok(records);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }






        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] SubjectVM publisherVM)
        {
            try
            {
                var rec = await _subjectRepository.EditSubjectAsync(publisherVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }



        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] SubjectVM publisherVM)
        {
            try
            {
                var rec = await _subjectRepository.DeleteSubjectAsync(publisherVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }
    }
}