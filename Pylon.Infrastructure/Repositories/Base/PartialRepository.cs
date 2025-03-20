using Pylon.Domain.Entities;
using Pylon.Infrastructure.Persistence;
using Pylon.Infrastructure.Repositories.Base;

namespace Pylon.Infrastructure.Repositories
{
	public partial class UserRepository : CoreRepository<User>
	{
		public UserRepository(AppDbContext context,
			IServiceProvider serviceProvider)
			: base(context, serviceProvider)
		{
		}
	}

	public partial class UserInfoRepository : CoreRepository<UserInfo>
	{
		public UserInfoRepository(AppDbContext context, 
			IServiceProvider serviceProvider)
			: base(context, serviceProvider)
		{
		}
	}
}
