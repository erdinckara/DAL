using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dal.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Dal.Repository.Concretes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> exp)
        {
            return await _dbSet.FirstOrDefaultAsync(exp);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> exp)
        {
            return await _dbSet.Where(exp).ToListAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsModel<TModel>()
        {
            return await _dbSet.ProjectTo<TModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsModel<TModel>(Expression<Func<TEntity, bool>> exp)
        {
            return await _dbSet.Where(exp).ProjectTo<TModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            Delete(entity);
        }


        public async Task<int> Count()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> exp)
        {
            return await _dbSet.CountAsync(exp);
        }

        public async Task<bool> Exist(Expression<Func<TEntity, bool>> exp)
        {
            return await _dbSet.AnyAsync(exp);
        }


    }
}