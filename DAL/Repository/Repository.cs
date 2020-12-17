using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.ApplicationDbContext;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
	public class Repository<T> : IAsyncRepository<T> where T : class
	{
		protected readonly AppDbContext _context;
		protected readonly DbSet<T> _db;

		public Repository(AppDbContext context)
		{
			_context = context;
			_db = context.Set<T>();
		}
		public virtual async Task Add(T entity)
		{
			await _db.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public virtual async Task Delete(T entity)
		{
			_db.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
			GC.SuppressFinalize(this);
		}

		public virtual async Task<T> Get(Guid id)
		{
			return await _db.FindAsync(id);
		}

		public virtual IQueryable<T> GetAll()
		{
			return _db;
		}

		public async Task Update(T entity)
		{
			_db.Update(entity);
			await _context.SaveChangesAsync();
		}
	}
}
