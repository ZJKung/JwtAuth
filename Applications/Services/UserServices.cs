using DAL.Entities;
using Applications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using Applications.ViewModels;

namespace Applications.Services
{
    public class UserServices : IUserService
    {
        private readonly JwtTokenService jwt;
        private readonly IUserRepositoty _repo;
        private readonly IMapper _mapper;

        public UserServices(IUserRepositoty repository, JwtTokenService tokenService, IMapper mapper)
        {
            jwt = tokenService;
            _repo = repository;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(User user)
        {
            return await _repo.AddUser(user);
        }

        public IEnumerable<User> GetAllUser()
        {
            return _repo.GetAll().Where(x => !string.IsNullOrEmpty(x.Username)).ToList();
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _repo.Get(id);
        }

        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel loginModels)
        {

            var user = _repo.GetAll().Where(x => x.Username == loginModels.Username && x.Password == loginModels.Password).FirstOrDefault();
            var token = jwt.GenerateJwtToken(user);
            var refreshToken = jwt.GenerateRefreshToken("");
            user.RefreshToken = refreshToken.Token;
            await _repo.Update(user);
            return new LoginResponseModel
            {
                Token = token,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
