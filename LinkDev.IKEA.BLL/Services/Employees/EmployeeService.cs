using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Models.Employee;
using LinkDev.IKEA.DAL.Perisistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
	{
		#region Service Configurations
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAttachmentService _attachmentService;

		public EmployeeService(
			IUnitOfWork unitOfWork,
			IAttachmentService attachmentService)
		{
			_unitOfWork = unitOfWork;
			_attachmentService = attachmentService;
		} 
		#endregion


		public async Task<int> CreateEmployeeAsync(CreatedEmployeeDTO employeeDto)
		{
			string? Image = string.Empty;

			if (employeeDto.Image is not null)
				Image = await _attachmentService.UploadAsync(employeeDto.Image, "Image");

			var employee = new Employee()
			{
				Name = employeeDto.Name,
				Age = employeeDto.Age,
				Address = employeeDto.Address,
				Salary = employeeDto.Salary,
				IsActive = employeeDto.IsActive,
				Email = employeeDto.Email,
				PhoneNumber = employeeDto.PhoneNumber,
				HiringDate = employeeDto.HiringDate,
				Gender = employeeDto.Gender,
				EmployeeType = employeeDto.EmployeeType,
				CreatedBy = 1,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.UtcNow,
				DepartmentId = employeeDto.DepartmentId,
				Image = Image
			};

			_unitOfWork.EmployeeRepository.Add(employee);
			return await _unitOfWork.CompleteAsync();
		}

		public async Task<int> UpdateEmployeeAsync(UpdatedEmployeeDTO employeeDto)
		{
			var employee = new Employee()
			{
				Id = employeeDto.Id,
				Name = employeeDto.Name,
				Age = employeeDto.Age,
				Address = employeeDto.Address,
				Salary = employeeDto.Salary,
				IsActive = employeeDto.IsActive,
				Email = employeeDto.Email,
				PhoneNumber = employeeDto.PhoneNumber,
				HiringDate = employeeDto.HiringDate,
				Gender = employeeDto.Gender,
				EmployeeType = employeeDto.EmployeeType,
				CreatedBy = 1,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.UtcNow,
				DepartmentId = employeeDto.DepartmentId
			};

			_unitOfWork.EmployeeRepository.Update(employee);
			
			return await _unitOfWork.CompleteAsync();
		}

		public async Task<bool> DeleteEmployeeAsync(int Id)
		{
			var EmployeeRepository = _unitOfWork.EmployeeRepository;

			var employee = await EmployeeRepository.GetByIdAsync(Id);

			if (employee is { })
				EmployeeRepository.Delete(employee);

			return await _unitOfWork.CompleteAsync() > 0;
		}

		public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync(string name)
		{
			return await _unitOfWork.EmployeeRepository
				.GetIQueryable()
				.Where(E => !E.IsDeleted && (string.IsNullOrEmpty(name) || E.Name.ToLower().Contains(name.ToLower())))
				.Include(E => E.Department)
				.Select(employee => new EmployeeDTO()
				{
					Id = employee.Id,
					Name = employee.Name,
					Age = employee.Age,
					IsActive = employee.IsActive,
					Salary = employee.Salary,
					Email = employee.Email,
					Gender = employee.Gender.ToString(),
					EmployeeType = employee.EmployeeType.ToString(),
					Department = (employee.Department != null && !employee.Department.IsDeleted ? employee.Department.Name : "" )
				}).ToListAsync();
		}

		public async Task<EmployeeDetailsDTO?> GetEmployeeByIdAsync(int Id)
		{
			var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(Id);

			if (employee is { })
			{
				return new EmployeeDetailsDTO()
				{
					Id = employee.Id,
					Name = employee.Name,
					Age = employee.Age,
					Address = employee.Address,
					IsActive = employee.IsActive,
					Salary = employee.Salary,
					Email = employee.Email,
					PhoneNumber = employee.PhoneNumber,
					HiringDate = employee.HiringDate,
					Gender = employee.Gender,
					EmployeeType = employee.EmployeeType,
					Department = employee.Department?.Id,
					Image = employee.Image
				};
			} 

			return null;
		}
	}
}