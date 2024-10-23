using LinkDev.IKEA.DAL.Models.Employee;
using LinkDev.IKEA.DAL.Perisistance.Data;
using LinkDev.IKEA.DAL.Perisistance.Repositories._Generic;

namespace LinkDev.IKEA.DAL.Perisistance.Repositories.Employees
{
	public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
