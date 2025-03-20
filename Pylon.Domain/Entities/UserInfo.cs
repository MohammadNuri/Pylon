using Pylon.Domain.Entities.Base;

namespace Pylon.Domain.Entities
{
	public class UserInfo : BaseEntity
	{
		public long UserInfoId { get; set; }
		public string Email { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string MobileNo { get; set; } = string.Empty;

		// Forigen Key To User
		public long UserId { get; set; }
		public User User { get; set; } = null!;
	}
}
