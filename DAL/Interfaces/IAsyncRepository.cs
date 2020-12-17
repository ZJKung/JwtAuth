using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
	public interface IAsyncRepository<T>:IDisposable where T : class 
	{
		IQueryable<T> GetAll();
		Task Add(T entity);
		Task Delete(T entity);
		Task<T> Get(Guid id);

		Task Update(T entity);
	}
}
