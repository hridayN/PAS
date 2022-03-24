using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PAS.API.Infrastructure.Contracts.Base
{
    /// <summary>
    /// Specification interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Criteria
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; set; }

        /// <summary>
        /// Includes
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; set; }

        /// <summary>
        /// Include strings
        /// </summary>
        List<string> IncludeStrings { get; set; }

        /// <summary>
        /// Order By
        /// </summary>
        Expression<Func<T, object>> OrderBy { get; set; }
    }
}
