using LinkDev.IKEA.DAL.Perisistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Perisistance.Repositories.Employees;

namespace LinkDev.IKEA.DAL.Perisistance.UnitOfWork
{
	public interface IUnitOfWork
	{
		public IEmployeeRepository EmployeeRepository { get; }
		public IDepartmentRepository DepartmentRepository { get; }

		public Task<int> CompleteAsync();

		public ValueTask DisposeAsync();
	}
}