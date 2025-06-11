using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserLoginRegisterService.BusinessLayer.Abstract;
using UserLoginRegisterService.EntityLayer.Concrete;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace UserLoginRegisterService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SupportFormUserController : ControllerBase
    {
        private readonly ISupportFormService _supportFormService;
        private readonly IUserService _userService;

        public SupportFormUserController(ISupportFormService supportFormService, IUserService userService)
        {
            _supportFormService = supportFormService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetMySupportForms()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var forms = _supportFormService.TGetList()
                .Where(x => x.IsDeleted == false && x.UserId == userId)
                .Select(form => new
                {
                    form.SupportFormID,
                    form.Subject,
                    form.Message,
                    form.CreatedDate,
                    form.Status
                }).ToList();

            return Ok(forms);
        }

        [HttpPost]
        public IActionResult CreateSupportForm([FromBody] SupportFormCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = _userService.TGetByid(userId);
            
            if (user == null)
            {
                return NotFound("User not found");
            }

            var supportForm = new SupportForm
            {
                UserId = userId,
                Subject = model.Subject,
                Message = model.Message,
                Status = SupportFormStatus.Processing,
                CreatedUserID = userId,
                CreatedDate = DateTime.Now
            };

            _supportFormService.TAdd(supportForm);

            return CreatedAtAction(nameof(GetMySupportForms), new { id = supportForm.SupportFormID }, supportForm);
        }
    }

    public class SupportFormCreateDto
    {
        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
    }
}
