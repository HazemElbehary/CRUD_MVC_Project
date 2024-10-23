using LinkDev.IKEA.DAL.Models.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Employees
{
	public class CreateEditViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;

		public string? Email { get; set; }

		public int? Age { get; set; }

		public string? Address { get; set; }

		public decimal Salary { get; set; }

		public string? PhoneNumber { get; set; }

		public EmpType EmployeeType { get; set; }

		public Gender Gender { get; set; }

		public bool IsActive { get; set; }

		[Display(Name = "Department")]
        public int? DepartmentId { get; set; }

        public DateOnly HiringDate { get; set; }

        public IFormFile? Image { get; set; }
    }
}
