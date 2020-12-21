using Applications.ViewModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(UserCreateModel user);

        IEnumerable<User> GetAllUser();

        Task<User> GetUserById(Guid id);
        Task<LoginResponseModel> LoginAsync(LoginRequestModel loginRequest);
    }
}
