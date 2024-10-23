using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

		public int? Age { get; set; }

		[DataType(DataType.Currency)]
		public decimal Salary { get; set; }

		public bool IsActive { get; set; }

		[EmailAddress]
		public string? Email { get; set; }

		public string Gender { get; set; } = null!;

		public string EmployeeType { get; set; } = null!;

		public string? Department { get; set; } = null!;

    }
}
