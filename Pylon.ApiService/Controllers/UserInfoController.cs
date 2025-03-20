using Microsoft.AspNetCore.Mvc;
using Pylon.Application.CustomAttributes;

namespace Pylon.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public partial class UserInfoController
	{
		[HttpGet("GetAll")]
		[PaginatedQueryAttribute]
		public async Task<IActionResult> GetAll()
		{
			var q = await Task.Run(() => GetService().GetAll());

			return Ok(q);
		}
	}
}
