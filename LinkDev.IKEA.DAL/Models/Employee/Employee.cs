using LinkDev.IKEA.DAL.Models.Common.Enums;
using LinkDev.IKEA.DAL.Models.Department;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.DAL.Models.Employee
{
	public class Employee : ModelBase
	{
		public string Name { get; set; } = null!;

        public int? Age { get; set; }

		public string? Address { get; set; }

		[DataType(DataType.Currency)]
		public decimal Salary { get; set; }

		[Display(Name = "IsActive")]
		public bool IsActive { get; set; }

		public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmpType EmployeeType { get; set; }

        public int? DepartmentId { get; set; }


        public string? Image { get; set; }

        // Navigation Property
        public virtual Department.Department? Department { get; set; }
    }
}
