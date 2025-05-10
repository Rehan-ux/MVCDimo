using Dimo.DAL.Data.Repositries.Interfacies;
using Dimo.DAL.Models;
using Dimo.DAL.Models.DepartmentModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimo.DAL.Data.Repositries.Classes
{
	public class GenericRepository<TEntity>(AppDbContext _dpContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
	{

		public void Add(TEntity Entity) //obgect member method
		{
			_dpContext.Set<TEntity>().Add(Entity); //add
			//_dpContext.Add(Entity);
			//return _dpContext.SaveChanges(); //Update database 
		}

		public void Delete(TEntity Entity)
		{
			_dpContext.Set<TEntity>().Remove(Entity); //Remove locally[deleted]
			//return _dpContext.SaveChanges();
		}

		public IEnumerable<TEntity> GetAll(bool withtracking = false)
		{
			if (withtracking)
			{
				return _dpContext.Set<TEntity>().Where(E =>E.IsDeleted != true).ToList();
			}
			else
				return _dpContext.Set<TEntity>().Where(E =>E.IsDeleted != true).AsNoTracking().ToList();
		}

		public TEntity GetById(int id)
		{
			return _dpContext.Set<TEntity>().Find(id);
		}

		public void Update(TEntity Entity)
		{
			_dpContext.Set<TEntity>().Update(Entity); // Update localy[modified]
			//return _dpContext.SaveChanges();
		}
	}
}
