using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Pylon.Domain.Entities;
using Pylon.Domain.Repositories;
using Pylon.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pylon.Infrastructure.Repositories
{
	public partial class UserInfoRepository
	{
		public IQueryable<UserInfo> GetAll()
		{
			return GetQueryable();
		}
	}
}
