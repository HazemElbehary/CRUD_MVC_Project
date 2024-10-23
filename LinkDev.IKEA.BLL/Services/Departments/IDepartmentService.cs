using LinkDev.IKEA.BLL.Models.Departments;

namespace LinkDev.IKEA.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync(string name);

        Task<List<DepartmentDTO>> GetAllAsIQueryableAsync(string name);

        Task<DepartmentDetailsDTO?> GetDepartmentByIdAsync(int Id);

        Task<int> CreateDepartmentAsync(CreatedDepartmentDTO departmentDTO);

        Task<int> UpdateDepartmentAsync(UpdatedDepartmentDTO departmentDTO);

        Task<bool> DeleteDepartmentAsync(int Id);
    }
}
