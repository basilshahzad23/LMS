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
    public class LanguageController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILanguageRepository _languageRepository;

        public LanguageController(ILanguageRepository languageRepository, UserManager<ApplicationUser> userManager)
        {

            this._languageRepository = languageRepository;

            _userManager = userManager;



        }

        [HttpPost("AddNew")]
        [Authorize]
        public async Task<IActionResult> AddNew([FromBody] LanguageVM languageVM)
        {
            try
            {

                var rec = await _languageRepository.AddLanguageAsync(languageVM);

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
                var records = await _languageRepository.GetAllLanguageAsync();
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
        public async Task<IActionResult> GetPagedLanguages(int page, int pageSize)
        {
            try
            {
                var records = await _languageRepository.GetAllLanguagePagedAsync(page, pageSize);
                return Ok(records);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }






        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] LanguageVM languageVM)
        {
            try
            {
                var rec = await _languageRepository.EditLanguageAsync(languageVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }



        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] LanguageVM languageVM)
        {
            try
            {
                var rec = await _languageRepository.DeleteLanguageAsync(languageVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }
    }
}