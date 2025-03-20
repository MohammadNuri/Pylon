using Pylon.Application.Interfaces;
using Pylon.Application.Services.Base;
using Pylon.Infrastructure.Repositories;

namespace Pylon.Application.Services
{
	public partial class UserService : CoreService<UserRepository>, IUserService
	{
		public UserService(UserRepository repository,
			IServiceProvider serviceProvider) 
			: base(repository, serviceProvider)
		{
		}
	}

	public partial class UserInfoService : CoreService<UserInfoRepository>, IUserInfoService
	{
		public UserInfoService(UserInfoRepository repository,
			IServiceProvider serviceProvider)
			: base(repository, serviceProvider)
		{
		}
	}
}
