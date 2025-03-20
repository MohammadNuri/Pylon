using Pylon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pylon.Application.Interfaces
{
	public interface IUserInfoService
	{
		IQueryable GetAll();
	}
}
