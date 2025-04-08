using Dimo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data.Repositries.Interfacies
{
    public interface IDepartmentRepository
    {
        //GetAll بتجيب كل Department   Reterntype  بيكون   list or arr
        IEnumerable<Department> GetAll(bool withTracking);
        //Get By Id  هترجع حاجه من نوعdepartment 
        Department GetById(int id);
        //Update هترجع ليا int  وبتاخد مني department تعمله update
        int Update (Department Entity); 
        //Delete
        int Delete (Department Entity);//لو اكبر 
        //insert
        int Add (Department Entity);



    }
}
