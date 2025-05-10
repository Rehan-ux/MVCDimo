using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data.Repositries.Interfacies
{
    public interface IUnitOfWork
    {
        //يكون عندي prop لكل Roposatily
        public IEmployeeRepository EmployeeRepository { get;  } //signtiour for proparty
        public IDepartmentRepository DepartmentRepository { get;  }
        int SaveChanges();
    }
}
