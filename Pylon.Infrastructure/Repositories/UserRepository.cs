using Pylon.Domain.Entities;

namespace Pylon.Infrastructure.Repositories
{
	public partial class UserRepository
	{
		public IQueryable<User> GetAll()
		{
			var q = GetQueryable();

			return q;
		}
	}
}
