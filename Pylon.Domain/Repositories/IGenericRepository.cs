using Pylon.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pylon.Domain.Repositories
{
	public interface IGenericRepository<T> where T : class
	{
		Task<T?> GetByIdAsync(long id);
		Task<List<T>> GetAllAsync(int top = 10);
		IQueryable<T> GetQueryable();	
		Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
		Task<ServiceResult> SaveChangesAsync(T entity);
		Task<ServiceResult> SaveChangesAsync(IEnumerable<T> entities);
	}
}
