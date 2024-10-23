using LinkDev.IKEA.DAL.Models;
using LinkDev.IKEA.DAL.Models.Department;

namespace LinkDev.IKEA.DAL.Perisistance.Repositories._Generic
{
	public interface IGenericRepository<T> where T : ModelBase
	{
		Task<IEnumerable<T>> GetAllAsync(bool WithAsNoTracking = true);
		IQueryable<T> GetIQueryable();

		Task<T?> GetByIdAsync(int id);

		void Add(T T);

		void Update(T T);

		void Delete(T T);
	}
}
