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
    public class PublisherController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPublisherRepository _publisherRepository;

        public PublisherController(IPublisherRepository publisherRepository, UserManager<ApplicationUser> userManager)
        {

            this._publisherRepository = publisherRepository;

            _userManager = userManager;



        }

        [HttpPost("AddNew")]
        [Authorize]
        public async Task<IActionResult> AddNew([FromBody] PublisherVM publisherVM)
        {
            try
            {

                var rec = await _publisherRepository.AddPublisherAsync(publisherVM);

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
                var records = await _publisherRepository.GetAllPublisherAsync();
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
        public async Task<IActionResult> GetPagedPublishers(int page, int pageSize)
        {
            try
            {
                var records = await _publisherRepository.GetAllPublisherPagedAsync(page, pageSize);
                return Ok(records);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }






        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] PublisherVM publisherVM)
        {
            try
            {
                var rec = await _publisherRepository.EditPublisherAsync(publisherVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }



        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] PublisherVM publisherVM)
        {
            try
            {
                var rec = await _publisherRepository.DeletePublisherAsync(publisherVM);
                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }
    }
}