﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLoginRegisterService.DtoLayer.Dtos
{
	public class UserLoginDto
	{
		public string? Email { get; set; }
		public string? Password { get; set; }
	}
}
