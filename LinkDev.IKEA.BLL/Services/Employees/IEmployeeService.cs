using LinkDev.IKEA.BLL.Models.Employees;

namespace LinkDev.IKEA.BLL.Services.Employees
{
	public interface IEmployeeService
	{
		Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync(string name);

        Task<EmployeeDetailsDTO?> GetEmployeeByIdAsync(int Id);

		Task<int> CreateEmployeeAsync(CreatedEmployeeDTO employeeDTO);

        Task<int> UpdateEmployeeAsync(UpdatedEmployeeDTO employeeDTO);

        Task<bool> DeleteEmployeeAsync(int Id);
	}
}
