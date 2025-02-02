using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ConexusContext _conexusContext;

        public GenericRepository(ConexusContext conexusContext)
        {
            _conexusContext = conexusContext;
        }

        public virtual void Add(T entity)
        {
            _conexusContext.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _conexusContext.Set<T>().AddRange(entities);
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _conexusContext.Set<T>().Where(expression);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(string miProcedimiento)
        {
            return await _conexusContext.Set<T>()
                .FromSqlInterpolated($"EXEC {miProcedimiento}")
                .ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id, string miProcedimiento)
        {
            var result = await _conexusContext.Set<T>()
                .FromSqlInterpolated($"EXEC {miProcedimiento} @Id = {id} ")
                .ToListAsync();
            return result.FirstOrDefault();
        }

        public virtual void Remove(T entity)
        {
            _conexusContext.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _conexusContext.Set<T>().RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            _conexusContext.Set<T>().Update(entity);
        }
        public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(
            int pageIndex,
            int pageSize,
            string _search
        )
        {
            var totalRegistros = await _conexusContext.Set<T>().CountAsync();
            var registros = await _conexusContext
                .Set<T>()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (totalRegistros, registros);
        }
    }
}
