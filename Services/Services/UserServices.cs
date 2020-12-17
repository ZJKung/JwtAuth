using DAL.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Interfaces;
using System.Threading.Tasks;

namespace Services.Services
{
	public class UserServices : IUserService
	{
		private readonly JwtTokenService jwt;
		private readonly IUserRepositoty _repo;

		public UserServices(IUserRepositoty repository,JwtTokenService tokenService)
		{
			jwt = tokenService;
			_repo = repository;
		}

		public async Task<User> CreateUser(User user)
		{
			return await _repo.AddUser(user);
		}

		public IEnumerable<User> GetAllUser()
		{
			return _repo.GetAll().Where(x=>!string.IsNullOrEmpty(x.Username)).ToList();
		}

		public async Task<User> GetUserById(Guid id)
		{
			return await _repo.Get(id);
		}

		public async Task<(string, string)> LoginAsync(string name, string password)
		{
			CreateUser(new User { Id = Guid.NewGuid(), Username = "ryan", Password = "12345" }).Wait();
			var user = _repo.GetAll().Where(x => x.Username == name && x.Password == password).FirstOrDefault();			
			var token = jwt.GenerateJwtToken(user);
			var refreshToken = jwt.GenerateRefreshToken("");
			user.RefreshToken = refreshToken;
			await _repo.Update(user);
			return (token, refreshToken.Token);
		}
		//public 
	}
}
