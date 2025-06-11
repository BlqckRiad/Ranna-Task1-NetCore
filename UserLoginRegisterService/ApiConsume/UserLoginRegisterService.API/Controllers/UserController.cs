using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserLoginRegisterService.BusinessLayer.Abstract;
using UserLoginRegisterService.DtoLayer.Dtos;
using UserLoginRegisterService.DtoLayer.Dtos.Update;
using UserLoginRegisterService.EntityLayer.Concrete;

namespace UserLoginRegisterService.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize(Roles ="Admin")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
        public UserController(IUserService userService , IMapper mapper)
        {
            _userService = userService;
			_mapper = mapper;
        }
		[HttpGet]
		public IActionResult GetAllUsers()
		{
			var users = _userService.TGetList().Where(x=> x.IsDeleted == false);
			var userDtos = _mapper.Map<List<UserListRequestDto>>(users);

			return Ok(userDtos);
		}
		[HttpGet]
		public IActionResult GetDeletedUsers()
		{
			var users = _userService.TGetList().Where(x => x.IsDeleted == true);
			var userDtos = _mapper.Map<List<UserListRequestDto>>(users);

			return Ok(userDtos);
		}
		[HttpGet]
		public IActionResult GetKisiWithID(int id)
		{
			var user = _userService.TGetByid(id);

			if (user == null)
			{
				return NotFound("Kullanıcı bulunamadı.");
			}

			var userDto = _mapper.Map<UserListRequestDto>(user);
			return Ok(userDto);
		}

		[HttpPost]
		public IActionResult AddUser([FromBody] UserRegisterDto model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Model Yapıya Uygun Değildir.");
			}

			var existingUsers = _userService.TGetList();
			if (existingUsers.Any(u => u.UserEmail == model.UserEmail))
			{
				return BadRequest("E-posta adresi zaten kullanılıyor.");
			}

			var user = new User
			{
				UserEmail = model.UserEmail,
				CreatedDate = DateTime.Now,
				CreatedUserID = 0,
				UserRoleID = 1
			};

			if (string.IsNullOrEmpty(model.Password))
			{
				return BadRequest("Password is null");
			}

			using (var sha = System.Security.Cryptography.SHA256.Create())
			{
				string hashedPassword = Convert.ToBase64String(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password)));
				user.Password = hashedPassword;
			}

			_userService.TAdd(user);
			return Ok(new { success = true });
		}

		[HttpPut]
		public IActionResult UpdateUser([FromBody] UserUpdateDto model)
		{
			var user = _userService.TGetByid(model.UserID);
			if (user == null)
			{
				return NotFound("Kullanıcı bulunamadı.");
			}

			// User entity'de Name, SurName, UserName, UserDate, UserSexsID yok, sadece güncellenebilir alanları güncelliyoruz
			user.UpdatedDate = DateTime.Now;
			user.UpdatedUserID = model.UpdatedUserID;
			// Eğer User entity'ye yeni alanlar eklenirse burada güncellenebilir

			_userService.TUpdate(user);
			return Ok(new { success = true });
		}

		[HttpDelete]
		public IActionResult DeleteUser(int id)
		{
			var user = _userService.TGetByid(id);
			if (user == null)
			{
				return NotFound("Kullanıcı bulunamadı.");
			}
			user.IsDeleted = true;
			user.DeletedDate = DateTime.Now;
			user.DeletedUserID = 0; // Gerekirse giriş yapan adminin ID'si alınabilir
			_userService.TUpdate(user);
			return Ok(new { success = true });
		}
	}
}
