using LinkDev.IKEA.DAL.Perisistance.Data;
using LinkDev.IKEA.DAL.Perisistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Perisistance.Repositories.Employees;

namespace LinkDev.IKEA.DAL.Perisistance.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork, IAsyncDisposable
	{
		private readonly ApplicationDbContext _dbContext;

		public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_dbContext);
        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_dbContext);

        public UnitOfWork(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
		}

		public async Task<int> CompleteAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}
		public async ValueTask DisposeAsync() 
		{
			await _dbContext.DisposeAsync();
		}
    }
}