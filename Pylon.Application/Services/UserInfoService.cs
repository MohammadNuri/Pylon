using Pylon.Application.CustomAttributes;

namespace Pylon.Application.Services
{
	public partial class UserInfoService
	{
		public IQueryable GetAll()
		{
			return GetRepository().GetAll();
		}
	}
}
