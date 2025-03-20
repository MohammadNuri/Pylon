using Microsoft.EntityFrameworkCore;
using Pylon.Domain.Entities;

namespace Pylon.Infrastructure.Persistence
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<UserInfo> UsersInfo { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// One to One Relationship in User & UserInfo
			modelBuilder.Entity<User>()
				.HasOne(u => u.UserInfo)
				.WithOne(ui => ui.User)
				.HasForeignKey<UserInfo>(ui => ui.UserId);
		}

		// if you wanna init the database
		// Run the following command in the .Net CLI to address the DBContext Project
		// dotnet ef migrations add init_database --project Pylon.Infrastructure --startup-project Pylon.ApiService
	}
}
