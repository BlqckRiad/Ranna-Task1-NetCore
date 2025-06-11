using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserLoginRegisterService.BusinessLayer.Abstract;
using UserLoginRegisterService.DtoLayer.Dtos;
using UserLoginRegisterService.EntityLayer.Concrete;

namespace UserLoginRegisterService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public IActionResult GetAllRoles()
        {
           var roles = _roleService.TGetList().Where(x=> x.IsDeleted==false);
            return Ok(roles);
        }
        [HttpGet]
        public IActionResult GetRoleWithID(int id)
        {
            var role = _roleService.TGetByid(id);
            return Ok(role);
        }
        [HttpPost]
        public IActionResult AddRole(AddRoleDto model)
        {
            if (model == null)
            {
                return BadRequest("Model boştur");
            }
            var newrole = new Role
                {
                    RoleName = model.RoleName,
                    CreatedDate = DateTime.Now,
                    CreatedUserID = model.CreatedUserID,
                };
            _roleService.TAdd(newrole);
            return Ok(newrole);
        }
        [HttpPost]
        public IActionResult UpdateRole(UpdateRoleDto model)
        {
            var role = _roleService.TGetByid(model.RoleID);
            if(role == null)
            {
                return BadRequest("Rol bilgisi bulunanadı.");
            }
            role.UpdatedDate = DateTime.Now;
            role.UpdatedUserID = model.UpdatedUserID;
            role.RoleName = model.RoleName;
            _roleService.TUpdate(role);
            return Ok(role);
        }
        [HttpPost]
        public IActionResult DeleteRole (JustIDDto model)
        {
            var role = _roleService.TGetByid(model.Id);
            if (role == null)
            {
                return BadRequest("Rol bilgisi bulunanadı.");
            }
            role.DeletedDate = DateTime.Now;
            role.DeletedUserID = model.DeletedUserID;
            role.IsDeleted = true; 
            _roleService.TUpdate(role);
            return Ok(role);
        }
    }
}
