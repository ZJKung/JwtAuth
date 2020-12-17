using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Controllers
{
	[Authorize]
	[Route("[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _service;

		public UserController(IUserService service)
		{
			_service = service;
		}

		[HttpGet(nameof(Get))]
		public IActionResult Get()
		{
			var users = _service.GetAllUser();
			return Ok(users);
		}
		[AllowAnonymous]
		[HttpPost("Login")]
		public async Task<IActionResult> LoginAsync([FromForm] string username, [FromForm] string password)
		{			
			var token = await _service.LoginAsync(username, password);
			return Ok(new
			{
				Token = token.Item1,
				RefreshToken = token.Item2
			});
		}
	}
}
