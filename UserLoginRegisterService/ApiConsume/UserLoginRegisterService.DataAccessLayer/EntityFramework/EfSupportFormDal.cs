﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLoginRegisterService.DataAccessLayer.Abstract;
using UserLoginRegisterService.DataAccessLayer.Concrete;
using UserLoginRegisterService.DataAccessLayer.Repository;
using UserLoginRegisterService.EntityLayer.Concrete;

namespace UserLoginRegisterService.DataAccessLayer.EntityFramework
{
    public class EfSupportFormDal : GenericRepository<SupportForm>, ISupportFormDal
    {
        public EfSupportFormDal(Context context) : base(context)
        {

        }
    }
}
