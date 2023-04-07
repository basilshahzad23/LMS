using LMS.Model;
using LMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookLedgerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookLedgerRepository _bookLedgerRepository;

        public BookLedgerController(IBookLedgerRepository bookLedgerRepository, UserManager<ApplicationUser> userManager)
        {

            this._bookLedgerRepository = bookLedgerRepository;

            _userManager = userManager;



        }

        [HttpPost("BookIssue")]
        [Authorize]
        public async Task<IActionResult> Issue([FromBody] BookLedgerVM bookLedgerVM)
        {
            try
            {

                var rec = await _bookLedgerRepository.IssueBookAsync(bookLedgerVM);

                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }



        [HttpPost("BookBankIssue")]
        [Authorize]
        public async Task<IActionResult> Issue([FromBody] BankBookIssueVM bookBookIssueVM)
        {
            try
            {

                var rec = await _bookLedgerRepository.IssueBookBankIssueAsync(bookBookIssueVM);

                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }



        [HttpGet("Get")]
        [Authorize]
        public async Task<IActionResult> Get([FromBody] BookLedgerVM bookLedgerVM)
        {
            try
            {
                var records = await _bookLedgerRepository.GetAllBookLedgerAsync();
                return Ok(records);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }

        [Route("[action]/{StudentFacultyID},{Book_ID}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Gets([FromBody] BookLedgerVM bookLedgerVM)
        {
            try
            {
                var records = await _bookLedgerRepository.GetBookLedgerIssuedAsync(bookLedgerVM.Student_FacultyID, bookLedgerVM.BookID);
                return Ok(records);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }


        [HttpPost("BookReturn")]
        [Authorize]
        public async Task<IActionResult> Return([FromBody] BookLedgerVM bookLedgerVM)
        {
            try
            {

                var rec = await _bookLedgerRepository.ReturnBookAsync(bookLedgerVM);

                return Ok(rec);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }




        [Route("[action]/{page}, {pageSize}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPagedBookLedgers(int page, int pageSize)
        {
            try
            {
                var records = await _bookLedgerRepository.GetAllBookLedgerPagedAsync(page, pageSize);
                return Ok(records);
            }
            catch (Exception ex)
            {

                return Unauthorized(ex);
            }
        }        
    }

}

