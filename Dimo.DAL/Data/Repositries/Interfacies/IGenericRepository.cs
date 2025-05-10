using Dimo.DAL.Models;
using Dimo.DAL.Models.DepartmentModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data.Repositries.Interfacies
{
	public interface IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		//GetAll بتجيب كل Department   Reterntype  بيكون   list or arr
		IEnumerable<TEntity> GetAll(bool withTracking = false);
		//Get By Id  هترجع حاجه من نوعdepartment 
		TEntity GetById(int id);
		//Update هترجع ليا int  وبتاخد مني department تعمله update
		void Update(TEntity Entity);
		//Delete
		void Delete(TEntity Entity);//لو اكبر 
									  //insert
		void Add(TEntity Entity);
	}
}
