using LinkDev.IKEA.DAL.Models.Department;
using LinkDev.IKEA.DAL.Perisistance.Data;
using LinkDev.IKEA.DAL.Perisistance.Repositories._Generic;

namespace LinkDev.IKEA.DAL.Perisistance.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
