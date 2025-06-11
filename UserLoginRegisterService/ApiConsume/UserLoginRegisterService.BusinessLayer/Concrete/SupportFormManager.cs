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
   
    public class SupportFormManager : ISupportFormService
    {
        private readonly ISupportFormDal _dal;

        public SupportFormManager(ISupportFormDal dal)
        {
            _dal = dal;
        }
        public void TAdd(SupportForm entity)
        {
            _dal.Add(entity);
        }

        public void TDelete(SupportForm id)
        {
            _dal.Delete(id);
        }

        public SupportForm TGetByid(int id)
        {
            return _dal.GetByid(id);
        }

        public List<SupportForm> TGetList()
        {
            return _dal.GetList();
        }

        public void TUpdate(SupportForm entity)
        {
            _dal.Update(entity);
        }
    }
}
