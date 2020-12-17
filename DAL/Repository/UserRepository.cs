using DAL.ApplicationDbContext;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
	public class UserRepository : Repository<User>, IUserRepositoty
	{

		public UserRepository(AppDbContext context) : base(context) { }

		public async Task<User> AddUser(User user)
		{
			await _db.AddAsync(user);
			await _context.SaveChangesAsync();
			return user;
		}
	}
}
