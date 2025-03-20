using Pylon.API.Controllers.Base;
using Pylon.Application.Interfaces;

namespace Pylon.Controllers
{
	public partial class UserController : CoreController<IUserService>
	{
		public UserController(IServiceProvider serviceProvider)
		: base(serviceProvider)
		{
		}
	}

	public partial class UserInfoController : CoreController<IUserInfoService>
	{
		public UserInfoController(IServiceProvider serviceProvider)
		: base(serviceProvider)
		{
		}
	}
}
