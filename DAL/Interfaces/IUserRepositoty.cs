using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
	public interface IUserRepositoty: IAsyncRepository<User>
	{
		Task<User> AddUser(User user);
	}
}
