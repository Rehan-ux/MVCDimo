using Dimo.DAL.Data.Repositries.Interfacies;
using Dimo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data.Repositries.Classes
{
    
    public class DepartmentRepositry(AppDbContext dpContext) : IDepartmentRepository
    {
        //المستودع اللي بكلم منه الداتا بيس سواء عايز اضيف وامسح
        private readonly AppDbContext _dpContext=dpContext; //null
        //public DepartmentRepositry(AppDbContext dpContext)//Ask clr creating  object from app dbcontext
        //{
        //    //دا غلط علشان مش انا اللي اعمله 
        //    // _dpContext = new AppDpContext(); //كل request هفتح conection مع الداتا بيس ف هخلي   clr هوا اللي يعمل    creat للobgect

        //    _dpContext= dpContext; //خليت atrbute   يشاور علي   ref
        //}
        public int Add(Department Entity) //obgect member method
        {
            _dpContext.Department.Add(Entity); //add
            return _dpContext.SaveChanges(); //Update database 
        }

        public int Delete(Department Entity)
        {
            _dpContext.Department.Remove(Entity); //Remove locally[deleted]
            return _dpContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll(bool withtracking=false)
        {
            if (withtracking)
            {
                return _dpContext.Department.ToList();
            }
            else
                return _dpContext.Department.AsNoTracking().ToList();
        }

        public Department GetById(int id)
        {
            return _dpContext.Department.Find(id);
        }

        public int Update(Department Entity)
        {
            _dpContext.Department.Update(Entity); // Update localy[modified]
           return _dpContext.SaveChanges();
        }
    }
}
