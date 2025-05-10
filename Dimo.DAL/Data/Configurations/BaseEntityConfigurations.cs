using Dimo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data.Configurations
{
	public class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
	{
		public void Configure(EntityTypeBuilder<T> builder)
		{
			builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATe()");//فقطinsertبتكون علي مش بتتغير بعده 
			builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GEtDATE()");//بتتغير مع كل run انا بعملها 
		}
	}
}
