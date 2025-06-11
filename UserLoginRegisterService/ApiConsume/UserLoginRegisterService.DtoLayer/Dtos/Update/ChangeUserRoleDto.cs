using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLoginRegisterService.DtoLayer.Dtos.Update
{
    public class ChangeUserRoleDto
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int UpdatedUserID { get; set; }
    }
}
