using PAS.API.Infrastructure.Contracts.Base;
using PAS.API.Infrastructure.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PAS.API.Infrastructure.Contracts
{
    /// <summary>
    /// Repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : Entity
    {
        #region Get methods
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetAllAsync();

        /// <summary>
        /// Return one record with order by
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="orderByColumn"></param>
        /// <param name="isOrderBy"></param>
        /// <returns></returns>
        Task<T> GetOneAsyncWithOrder(Expression<Func<T, bool>> spec, string orderByColumn, bool isOrderBy);

        /// <summary>
        /// Get records
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
        #endregion


        /// <summary>
        /// Add records to table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);
        
        /// <summary>
        /// Update table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);


    }
}
