namespace LinkDev.IKEA.DAL.Models.Department
{
    public class Department : ModelBase
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly CreationDate { get; set; }

        // Navigation Property
        public virtual ICollection<Employee.Employee> Employees { get; set; } = new HashSet<Employee.Employee>();
    }
}
