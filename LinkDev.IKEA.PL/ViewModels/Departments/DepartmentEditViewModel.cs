namespace LinkDev.IKEA.PL.ViewModels.Departments
{
	public class DepartmentEditViewModel
	{
        public int Id { get; set; }
        public string Code { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public DateOnly CreationDate { get; set; }
	}
}
