using System.ComponentModel.DataAnnotations;

namespace UserLoginRegisterService.EntityLayer.Concrete
{

	public class User : BaseEntity
	{
		[Key]
		public int UserID { get; set; }

		[Required(ErrorMessage = "UserEmail alanı zorunludur.")]
		[EmailAddress(ErrorMessage = "Geçersiz e-posta formatı.")]
		public string? UserEmail { get; set; }

		[Required(ErrorMessage = "Password alanı zorunludur.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "KisiPassword en az 6 ve en fazla 100 karakter olmalıdır.")]
		public string? Password { get; set; }
		public int UserRoleID { get; set; }
		

	}
}

