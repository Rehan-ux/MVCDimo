using Dimo.DAL.Data.Repositries.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data.Repositries.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private Lazy<IDepartmentRepository> _DepartmentRepository;
        private Lazy<IEmployeeRepository> _EmployeeRepository;
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _DepartmentRepository= new Lazy<IDepartmentRepository>(() => new DepartmentRepositry(dbContext));
            _EmployeeRepository= new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
            _dbContext = dbContext;
        }
        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return _EmployeeRepository.Value;
            }
          
        }  //Atomatic prop
        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                return _DepartmentRepository.Value;
            }
            
        } //دول يعترو null   هروح اعملهم  intialize في constructor بتاع unit of work

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
