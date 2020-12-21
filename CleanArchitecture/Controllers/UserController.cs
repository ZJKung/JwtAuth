using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Applications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applications.ViewModels;
using CleanArchitecture.Extensions;

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
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _service.GetAllUser();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var users = await _service.GetUserById(id);
            if (users == null) return BadRequest("user not found");

            return Ok(users);
        }
        //todo chango user to usercreate viewmodel
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateModel user)
        {
            var newuser = await _service.CreateUser(user);

            return Ok(newuser);
        }



        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestModel login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorMessage());
                }
                var loginResponse = await _service.LoginAsync(login);
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                return BadRequest(ex);
            }
        }
    }
}
