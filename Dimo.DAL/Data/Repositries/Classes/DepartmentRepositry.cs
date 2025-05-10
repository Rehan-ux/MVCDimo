using Dimo.DAL.Data.Repositries.Interfacies;
using Dimo.DAL.Models.DepartmentModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data.Repositries.Classes
{
    //primaryConstroctor
    public class DepartmentRepositry(AppDbContext dpContext) : GenericRepository<Department>(dpContext), IDepartmentRepository
    {
        //المستودع اللي بكلم منه الداتا بيس سواء عايز اضيف وامسح

    }
}
