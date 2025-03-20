using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Pylon.Application.CustomAttributes;
using Pylon.Domain.Entities;
using Pylon.Shared.Helpers;

namespace Pylon.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public partial class UserController
	{
		[HttpGet("GetAll")]
		[PaginatedQueryAttribute]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await Task.Run(() => GetService().GetAll()));
		}

		[HttpGet("GetById")]
		[PaginatedQueryAttribute]
		public async Task<IActionResult> GetById(long id)
		{
			return Ok(await Task.Run(() => GetService().GetById(id)));
		}

		[HttpPost("Save")]
		public async Task<ServiceResult> Save([FromBody]User user)
		{
			if (user == null)
				return ServiceResult.Failure(ResponseMessage.NO_CLIENT_DATA);

			return await GetService().Save(user);
		}
	}
}
