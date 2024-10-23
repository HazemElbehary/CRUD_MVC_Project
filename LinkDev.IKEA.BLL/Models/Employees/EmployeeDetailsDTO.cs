using LinkDev.IKEA.DAL.Models.Common.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public class EmployeeDetailsDTO
    {
        public int Id { get; set; }
		public string Name { get; set; } = null!;

		public int? Age { get; set; }

		public string? Address { get; set; }

		public decimal Salary { get; set; }

		public bool IsActive { get; set; }

		[EmailAddress]
		public string? Email { get; set; }

		[Display(Name = "Phone Number")]
		public string? PhoneNumber { get; set; }

		[Display(Name = "Hiring Date")]
		public DateOnly HiringDate { get; set; }

		public Gender Gender { get; set; }

		public EmpType EmployeeType { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }

		public int? Department { get; set; }

        public string? Image { get; set; }
    }
}
