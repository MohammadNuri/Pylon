using Pylon.Domain.Entities;
using Pylon.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pylon.Application.Interfaces
{
	public interface IUserService
	{
		IQueryable GetAll();
		IQueryable GetById(long id);
		Task<ServiceResult> Save(User user);
	}
}
