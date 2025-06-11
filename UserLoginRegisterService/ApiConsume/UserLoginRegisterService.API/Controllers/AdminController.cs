using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserLoginRegisterService.BusinessLayer.Abstract;
using UserLoginRegisterService.DtoLayer.Dtos;

namespace UserLoginRegisterService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdminController(IUserService userService , IMapper mapper)
        {
            _userService = userService;    
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllAdmins()
        {
            var users = _userService.TGetList().Where(x => x.IsDeleted == false && x.UserRoleID==2);
            var userDtos = _mapper.Map<List<UserListRequestDto>>(users);

            return Ok(userDtos);
        }
        [HttpGet]
        public IActionResult GetDeletedUsers()
        {
            var users = _userService.TGetList().Where(x => x.IsDeleted == false && x.UserRoleID == 2);
            var userDtos = _mapper.Map<List<UserListRequestDto>>(users);

            return Ok(userDtos);
        }
        [HttpGet]
        public IActionResult GetAdminWithID(int id)
        {
            var user = _userService.TGetByid(id);

            if (user == null || user.UserRoleID == 2)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var userDto = _mapper.Map<UserListRequestDto>(user);
            return Ok(userDto);
        }
    }
}
