using Dimo.DAL.Models.EmployeeModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data.Configurations
{
	public class EmployeeConfiguration :BaseEntityConfigurations<Employee>, IEntityTypeConfiguration<Employee>
	{
		public new void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.Property(E => E.Name).HasColumnType("varchar(50)");
			builder.Property(E => E.Address).HasColumnType("varchar(150)");
			builder.Property(E => E.Salary).HasColumnType("decimal(10,2)");
			builder.Property(E => E.Gender)
				.HasConversion((empGender) => empGender.ToString(),
				(returnedEmpGender) => (Gender)Enum.Parse(typeof(Gender), returnedEmpGender));
			builder.Property(E => E.EmployeeType)
				.HasConversion((empType) => empType.ToString(),
				(returnedEmpType) =>(EmployeeType) Enum.Parse(typeof(EmployeeType), returnedEmpType));

			base.Configure(builder);
			//builder.Property(D => D.CreatedOn).HasDefaultValue("GETDATE()");
			//builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");
		}
	}
}
