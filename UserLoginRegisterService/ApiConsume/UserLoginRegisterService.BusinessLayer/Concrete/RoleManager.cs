using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLoginRegisterService.BusinessLayer.Abstract;
using UserLoginRegisterService.DataAccessLayer.Abstract;
using UserLoginRegisterService.EntityLayer.Concrete;

namespace UserLoginRegisterService.BusinessLayer.Concrete
{
    
    public class RoleManager : IRoleService
    {
        private readonly IRoleDal _dal;

        public RoleManager(IRoleDal dal)
        {
            _dal = dal;
        }
        public void TAdd(Role entity)
        {
            _dal.Add(entity);
        }

        public void TDelete(Role id)
        {
            _dal.Delete(id);
        }

        public Role TGetByid(int id)
        {
            return _dal.GetByid(id);
        }

        public List<Role> TGetList()
        {
            return _dal.GetList();
        }

        public void TUpdate(Role entity)
        {
            _dal.Update(entity);
        }
    }
}
