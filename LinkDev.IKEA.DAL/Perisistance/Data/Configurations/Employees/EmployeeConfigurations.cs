using LinkDev.IKEA.DAL.Models.Common.Enums;
using LinkDev.IKEA.DAL.Models.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.IKEA.DAL.Perisistance.Data.Configurations.Employees
{
	internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{

			builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
			builder.Property(E => E.Address).HasColumnType("varchar(100)");
			builder.Property(E => E.Salary).HasColumnType("decimal(8, 2)");
			builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()");


			builder.Property(E => E.Gender).HasConversion(
				gender => gender.ToString(),
				gender => (Gender) Enum.Parse(typeof(Gender), gender)
			);

			builder.Property(E => E.EmployeeType).HasConversion
				(
					E => E.ToString(),
					E => (EmpType) Enum.Parse(typeof(EmpType), E)
				);

		}
	}
}
