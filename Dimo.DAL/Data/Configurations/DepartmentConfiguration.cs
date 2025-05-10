using Dimo.DAL.Models.DepartmentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data.Configurations
{
    public class DepartmentConfiguration :BaseEntityConfigurations<Department>, IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D=>D.Id).UseIdentityColumn(10,10);
            builder.Property(D => D.Name).HasColumnType("varchar(20)");
            builder.Property(D => D.Code).HasColumnType("varchar(20)");

            builder.HasMany(D => D.Employees)
                .WithOne(E => E.Department).OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);    

        }
    }
}
