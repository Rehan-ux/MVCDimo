using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Models
{
    public class BaseEntity
    {
        public int Id { get; set; } //pk لوعوزه اظبطه بيكون f fluentAPI
        public int CreatedBy { get; set; } //UserId
        public DateTime? CreatedOn { get; set; } //time of create
        public int LastModifiedBy { get; set; } //UserId
        public DateTime? LastModifiedOn { get; set; } //Time of create[Automatically Calculated]
        public bool IsDeleted { get; set; } //soft Delete

    }
}
