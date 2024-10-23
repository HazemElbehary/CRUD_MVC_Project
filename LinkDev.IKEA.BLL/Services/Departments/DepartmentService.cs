using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Models.Department;
using LinkDev.IKEA.DAL.Perisistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.BLL.Services.Departments
{
	public class DepartmentService : IDepartmentService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DepartmentService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		
		public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync(string name)
		{
			var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();

			LinkedList<DepartmentDTO> depts = new LinkedList<DepartmentDTO>();

            foreach (var department in departments)
            {
				if((string.IsNullOrEmpty(name) || department.Name.ToLower().Contains(name.ToLower())))
					depts.AddFirst( new DepartmentDTO()
				{
					Id = department.Id,
					Name = department.Name,
					Code = department.Code,
					CreationDate = department.CreationDate
				});
			}
			

			return depts;
		}


		public async Task<List<DepartmentDTO>> GetAllAsIQueryableAsync(string name)
		{
			return await _unitOfWork.DepartmentRepository
				.GetIQueryable()
				.Where(D => !D.IsDeleted && (string.IsNullOrEmpty(name) || D.Name.ToLower().Contains(name.ToLower())))
				.Select(
						department =>
							new DepartmentDTO()
							{
								Id = department.Id,
								Name = department.Name,
								Code = department.Code,
								CreationDate = department.CreationDate
							}
						).AsNoTracking().ToListAsync();
		}

		public async Task<DepartmentDetailsDTO?> GetDepartmentByIdAsync(int Id)
		{
			var dept = await _unitOfWork.DepartmentRepository.GetByIdAsync(Id);

			if (dept is { })
			{
				return new DepartmentDetailsDTO()
				{
					Id = dept.Id,
					Code = dept.Code,
					Name = dept.Name,
					Description = dept.Description,
					CreationDate = dept.CreationDate,
					CreatedBy = dept.CreatedBy,
					CreatedOn = dept.CreatedOn,
					LastModifiedBy = dept.LastModifiedBy,
					LastModifiedOn = dept.LastModifiedOn
				};
			}

			return null;
		}

		public async Task<int> CreateDepartmentAsync(CreatedDepartmentDTO departmentDTO)
		{
			var department = new Department()
			{
				Code = departmentDTO.Code,
				Name = departmentDTO.Name,
				Description = departmentDTO.Description,
				CreationDate = departmentDTO.CreationDate,
				CreatedBy = 1,
				CreatedOn = DateTime.UtcNow,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.UtcNow,
			};

			_unitOfWork.DepartmentRepository.Add(department);

			return await _unitOfWork.CompleteAsync();
		}

		public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentDTO departmentDTO)
		{
			var department = new Department()
			{
				Id = departmentDTO.Id,
				Code = departmentDTO.Code,
				Name = departmentDTO.Name,
				Description = departmentDTO.Description,
				CreationDate = departmentDTO.CreationDate,
				CreatedBy = 1,
				CreatedOn = DateTime.UtcNow,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.UtcNow,
			};

			_unitOfWork.DepartmentRepository.Update(department);
			return await _unitOfWork.CompleteAsync();
		}

		public async Task<bool> DeleteDepartmentAsync(int Id)
		{
			var _DepartmentRepsoitory = _unitOfWork.DepartmentRepository;

			var department = await _DepartmentRepsoitory.GetByIdAsync(Id);

			if (department is { })
				_DepartmentRepsoitory.Delete(department);

			return await _unitOfWork.CompleteAsync() > 0;

		}
	}
}
