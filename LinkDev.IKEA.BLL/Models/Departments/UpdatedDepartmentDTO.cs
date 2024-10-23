namespace LinkDev.IKEA.BLL.Models.Departments
{
    public class UpdatedDepartmentDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly CreationDate { get; set; }
    }
}
