using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.BLL.Models.Departments
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        [Display(Name = "Date Of Creation")]
        public DateOnly CreationDate { get; set; }

    }
}
