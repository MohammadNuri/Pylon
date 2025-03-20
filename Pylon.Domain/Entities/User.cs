using Pylon.Domain.Entities.Base;

namespace Pylon.Domain.Entities
{
	public class User : BaseEntity
	{
		public long UserId { get; set; }
		public string UserName { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;

		// Forigen Key To UserInfo
		public UserInfo UserInfo { get; set; } = new();
	}
}
