using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLoginRegisterService.DtoLayer.Dtos
{
	public class UserRegisterDto
	{
		[Required(ErrorMessage = "UserEmail alanı zorunludur.")]
		[EmailAddress(ErrorMessage = "Geçersiz e-posta formatı.")]
		public string? UserEmail { get; set; }

		[Required(ErrorMessage = "Password alanı zorunludur.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "KisiPassword en az 6 ve en fazla 100 karakter olmalıdır.")]
		public string? Password { get; set; }
	}
}
