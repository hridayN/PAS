using Microsoft.EntityFrameworkCore;
using PAS.API.Infrastructure.Contracts;
using PAS.API.Infrastructure.Contracts.Base;
using PAS.API.Infrastructure.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PAS.API.Infrastructure.Repositories.Base
{
    /// <summary>
    /// Repository class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : Entity
    {
        /// <summary>
        /// PASDbContext variable
        /// </summary>
        protected readonly PASDbContext _dbContext;

        /// <summary>
        /// ExpressionFilter variable
        /// </summary>
        protected readonly IExpressionFilter _expressionFilter;

        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="expressionFilter"></param>
        public Repository(PASDbContext dbContext, IExpressionFilter expressionFilter)
        {
            _dbContext = dbContext;
            _expressionFilter = expressionFilter;
        }

        /// <summary>
        /// Add records to table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get records
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        /// <summary>
        /// Return one record with order by
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="orderByColumn"></param>
        /// <param name="isOrderBy"></param>
        /// <returns></returns>
        public async Task<T> GetOneAsyncWithOrder(Expression<Func<T, bool>> spec, string orderByColumn, bool isOrderBy)
        {
            IQueryable<T> record = _dbContext.Set<T>().Where(spec);
            if (!string.IsNullOrEmpty(orderByColumn))
            {
                record = _expressionFilter.OrderByDynamic<T>(record, orderByColumn, out bool isValid, isOrderBy);
            }
            return await record.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Update table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Apply specification to the query
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
