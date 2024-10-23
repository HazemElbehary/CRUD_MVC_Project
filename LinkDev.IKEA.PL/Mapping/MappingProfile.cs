using AutoMapper;
using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.PL.ViewModels.Departments;
using LinkDev.IKEA.PL.ViewModels.Employees;

namespace LinkDev.IKEA.PL.Mapping
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            #region Department
            
            CreateMap<DepartmentEditViewModel, CreatedDepartmentDTO>();
            CreateMap<DepartmentDetailsDTO, DepartmentEditViewModel>();
            CreateMap<DepartmentEditViewModel, UpdatedDepartmentDTO>();

			#endregion

			#region Employee

			CreateMap<CreateEditViewModel, CreatedEmployeeDTO>();
			CreateMap<EmployeeDetailsDTO, CreateEditViewModel>()
			.ForMember(
				dist => dist.Image,
				config => config.MapFrom(src => MapImageNameToIFormFile(src.Image)));
			CreateMap<CreateEditViewModel, UpdatedEmployeeDTO>();

			#endregion
		}

		public IFormFile? MapImageNameToIFormFile(string? ImageName)
		{
			if (ImageName is null)
				return null;

			string filePath = Path.Combine("wwwroot/files", "image", $"{ImageName}");
			using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			return new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
			{
				Headers = new HeaderDictionary(),
				ContentType = "image/*"
			};
		}
    }
}
