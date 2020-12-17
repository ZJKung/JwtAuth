using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
	public interface IUserService
	{
		Task<User> CreateUser(User user);

		IEnumerable<User> GetAllUser();

		Task<User> GetUserById(Guid id);
		Task<(string, string)> LoginAsync(string name,string password);
	}
}
