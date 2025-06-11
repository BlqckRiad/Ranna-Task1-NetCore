using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserLoginRegisterService.BusinessLayer.Abstract;
using UserLoginRegisterService.EntityLayer.Concrete;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace UserLoginRegisterService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SupportFormController : ControllerBase
    {
        private readonly ISupportFormService _supportFormService;
        private readonly IUserService _userService;

        public SupportFormController(ISupportFormService supportFormService, IUserService userService)
        {
            _supportFormService = supportFormService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllSupportForms()
        {
            var forms = _supportFormService.TGetList().Where(x => x.IsDeleted == false);
            var result = forms.Select(form => new
            {
                form.SupportFormID,
                form.Subject,
                form.Message,
                form.UserId,
                UserEmail = _userService.TGetByid(form.CreatedUserID)?.UserEmail,
                form.CreatedDate,
                form.Status
            }).ToList();

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetSupportFormsByStatus(int statusCode)
        {
            if (!Enum.IsDefined(typeof(SupportFormStatus), statusCode))
            {
                return BadRequest($"Invalid status code. Valid values are: {string.Join(", ", Enum.GetValues(typeof(SupportFormStatus)))}");
            }

            var status = (SupportFormStatus)statusCode;
            var forms = _supportFormService.TGetList()
                .Where(x => x.IsDeleted == false && x.Status == status);
            
            var result = forms.Select(form => new
            {
                form.SupportFormID,
                form.Subject,
                form.Message,
                form.UserId,
                UserEmail = _userService.TGetByid(form.CreatedUserID)?.UserEmail,
                form.CreatedDate,
                form.Status
            }).ToList();

            if (!result.Any())
            {
                return NotFound($"No support forms found with status: {status}");
            }

            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetSupportFormWithID(int id)
        {
            var form = _supportFormService.TGetByid(id);
            if (form == null || form.IsDeleted)
            {
                return NotFound($"Support form with ID {id} not found.");
            }

            var result = new
            {
                form.SupportFormID,
                form.Subject,
                form.Message,
                form.UserId,
                UserEmail = _userService.TGetByid(form.CreatedUserID)?.UserEmail,
                form.CreatedDate,
                form.Status
            };

            return Ok(result);
        }

    }
}
