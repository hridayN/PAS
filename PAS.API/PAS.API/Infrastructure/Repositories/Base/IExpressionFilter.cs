using System;
using System.Linq;
using System.Linq.Expressions;

namespace PAS.API.Infrastructure.Repositories.Base
{
    /// <summary>
    /// ExpressionFilter interface
    /// </summary>
    public interface IExpressionFilter
    {
        /// <summary>
        /// GetExpression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FilterExpression"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        Expression<Func<T, bool>> GetExpression<T>(string FilterExpression, out bool isValid);

        /// <summary>
        /// OrderByDynamic
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="sortColumn"></param>
        /// <param name="isValid"></param>
        /// <param name="sortOrderAsc"></param>
        /// <returns></returns>
        IQueryable<T> OrderByDynamic<T>(IQueryable<T> query, string sortColumn, out bool isValid, bool sortOrderAsc = true);
    }
}