using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dal.Domain.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> exp);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> exp);
        Task<IEnumerable<TModel>> GetAllAsModel<TModel>();
        Task<IEnumerable<TModel>> GetAllAsModel<TModel>(Expression<Func<TEntity, bool>> exp);
        Task<int> Count();
        Task<int> Count(Expression<Func<TEntity, bool>> exp);
        Task<bool> Exist(Expression<Func<TEntity, bool>> exp);
        Task Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}