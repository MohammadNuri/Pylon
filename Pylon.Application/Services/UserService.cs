using Pylon.Domain.Entities;
using Pylon.Shared.Helpers;

namespace Pylon.Application.Services
{
	public partial class UserService
	{
		public IQueryable GetAll()
		{

			var q = GetRepository().GetAll();

			return q
				.Select(c => new
				{
					c.UserId,
					c.UserName,
				});
		}

		public IQueryable GetById(long id)
		{
			var result = GetRepository().GetQueryable()
				.Where(c => c.UserId == id);

			if (result == null)
				return null;

			return result;
		}		

		public async Task<ServiceResult> Save(User user)
		{
			if (user == null)
				return ServiceResult.Failure(ResponseMessage.WRONG_CLIENT_DATA);

			return await GetRepository().SaveChangesAsync(user);
		}
	}
}
