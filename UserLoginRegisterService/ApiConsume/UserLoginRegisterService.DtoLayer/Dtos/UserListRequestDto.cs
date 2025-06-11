using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLoginRegisterService.DtoLayer.Dtos
{
	public class UserListRequestDto
	{
		public int UserID { get; set; }
		public string? UserEmail { get; set; }
		public int UserRoleID { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatedUserID { get; set; }
		public DateTime UpdatedDate { get; set; }
		public int UpdatedUserID { get; set; }
		public DateTime DeletedDate { get; set; }
		public int DeletedUserID { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
