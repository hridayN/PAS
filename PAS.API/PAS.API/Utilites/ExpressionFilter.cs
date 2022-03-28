using PAS.API.Infrastructure.Repositories.Base;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using static PAS.API.Enums.Enum;
using System;

namespace PAS.API.Utilites
{
    public class ExpressionFilter : IExpressionFilter
    {
        private static readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[1]
       {
            typeof(string)
       });

        private static readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[1]
        {
            typeof(string)
        });

        private static readonly MethodInfo containsMethod = typeof(string).GetMethod("Contains", new Type[1]
        {
            typeof(string)
        });
        public Expression<Func<T, bool>> GetExpression<T>(string FilterExpression, out bool isValid)
        {
            isValid = true;
            Expression expression = null;
            try
            {
                List<FilterQuery> list = EvaluateFiltersFromString(FilterExpression);
                if (list == null)
                {
                    return null;
                }

                ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "t");
                foreach (FilterQuery item in list)
                {
                    expression = ((expression != null) ? ((item.OperatorType != 0) ? Expression.AndAlso(expression, GetExpression(parameterExpression, item)) : Expression.Or(expression, GetExpression(parameterExpression, item))) : GetExpression(parameterExpression, item));
                }

                return Expression.Lambda<Func<T, bool>>(expression, new ParameterExpression[1]
                {
                    parameterExpression
                });
            }
            catch (Exception exception)
            {
                isValid = false;
            }

            return null;
        }

        public IQueryable<T> OrderByDynamic<T>(IQueryable<T> query, string sortColumn, out bool isValid, bool sortOrderAsc = true)
        {
            isValid = true;
            try
            {
                int num = 0;
                string[] array = sortColumn.Split(",");
                foreach (string propertyName in array)
                {
                    ParameterExpression parameterExpression = Expression.Parameter(query.ElementType, "");
                    MemberExpression memberExpression = Expression.Property(parameterExpression, propertyName);
                    LambdaExpression expression = Expression.Lambda(memberExpression, parameterExpression);
                    string methodName = sortOrderAsc ? "OrderBy" : "OrderByDescending";
                    if (num > 0)
                    {
                        methodName = (sortOrderAsc ? "ThenBy" : "ThenByDescending");
                    }

                    Expression expression2 = Expression.Call(typeof(Queryable), methodName, new Type[2]
                    {
                        query.ElementType,
                        memberExpression.Type
                    }, query.Expression, Expression.Quote(expression));
                    query = query.Provider.CreateQuery<T>(expression2);
                    num++;
                }

                return query;
            }
            catch (Exception exception)
            {
                isValid = false;
            }

            return null;
        }

        private List<FilterQuery> EvaluateFiltersFromString(string predicate)
        {
            string pattern = "(\\p{P}?)([\\w-.]+?)(:|!|>|<|~)(\\p{P}?)([\\w-. |\\W-. ]+?)(\\p{P}?),";
            MatchCollection matchCollection = Regex.Matches(predicate + ",", pattern);
            List<FilterQuery> list = new List<FilterQuery>();
            foreach (Match item in matchCollection)
            {
                list.Add(BuildFilterQuery(item.Groups[1].Value, item.Groups[2].Value, item.Groups[3].Value, item.Groups[4].Value, item.Groups[5].Value, item.Groups[6].Value));
            }

            return list;
        }

        private FilterQuery BuildFilterQuery(string condition, string propertyName, string operation, string prefix, string propertyValue, string suffix)
        {
            if (prefix == "-")
            {
                propertyValue = prefix + propertyValue;
            }

            FilterQuery filterQuery = new FilterQuery
            {
                OperatorType = ((!(condition == "'")) ? OperatorType.And : OperatorType.Or),
                PropertyName = propertyName,
                PropertyValue = propertyValue,
                FilterType = GetFilterType(operation)
            };
            if (prefix == "*" && suffix == "*")
            {
                filterQuery.FilterType = FilterType.Contains;
            }
            else if (prefix == "*")
            {
                filterQuery.FilterType = FilterType.EndsWith;
            }
            else if (suffix == "*")
            {
                filterQuery.FilterType = FilterType.StartsWith;
            }

            return filterQuery;
        }

        private FilterType GetFilterType(string filterType)
        {
            FilterType result = FilterType.Equal;
            switch (filterType)
            {
                case ":":
                    result = FilterType.Equal;
                    break;
                case "!":
                    result = FilterType.NotEqual;
                    break;
                case "<":
                    result = FilterType.LessThan;
                    break;
                case ">":
                    result = FilterType.GreaterThan;
                    break;
                case "~":
                    result = FilterType.Range;
                    break;
            }

            return result;
        }

        private Expression GetExpression(ParameterExpression param, FilterQuery filter)
        {
            MemberExpression memberExpression = null;
            if (filter.PropertyName.Split('.').Length > 1)
            {
                int num = 0;
                string[] array = filter.PropertyName.Split('.');
                foreach (string propertyName in array)
                {
                    memberExpression = ((num != 0) ? Expression.Property(memberExpression, propertyName) : Expression.Property(param, propertyName));
                    num++;
                }
            }
            else
            {
                memberExpression = Expression.Property(param, filter.PropertyName);
            }

            UnaryExpression unaryExpression = null;
            if (filter.FilterType != FilterType.Range)
            {
                unaryExpression = ConvertValueToType(memberExpression, filter.PropertyValue);
            }

            MakeStringCaseInsesitive(memberExpression, unaryExpression, out Expression exp, out Expression exp2);
            switch (filter.FilterType)
            {
                case FilterType.Equal:
                    return Expression.Equal(exp, exp2);
                case FilterType.NotEqual:
                    return Expression.NotEqual(exp, exp2);
                case FilterType.StartsWith:
                    return Expression.Call(exp, startsWithMethod, exp2);
                case FilterType.EndsWith:
                    return Expression.Call(exp, endsWithMethod, exp2);
                case FilterType.Contains:
                    return Expression.Call(exp, containsMethod, exp2);
                case FilterType.GreaterThan:
                    return Expression.GreaterThan(memberExpression, unaryExpression);
                case FilterType.LessThan:
                    return Expression.LessThan(memberExpression, unaryExpression);
                case FilterType.Range:
                    {
                        string[] array2 = filter.PropertyValue.ToString()!.Split("-");
                        Expression expression = null;
                        string[] array = array2;
                        foreach (string value in array)
                        {
                            UnaryExpression valueExpression = ConvertValueToType(memberExpression, value);
                            MakeStringCaseInsesitive(memberExpression, valueExpression, out Expression exp3, out Expression exp4);
                            expression = ((expression != null) ? Expression.Or(expression, Expression.Equal(exp3, exp4)) : Expression.Equal(exp3, exp4));
                        }

                        return expression;
                    }
                default:
                    return null;
            }
        }

        private UnaryExpression ConvertValueToType(MemberExpression member, object value)
        {
            Type propertyType = ((PropertyInfo)member.Member).PropertyType;
            object obj = TypeDescriptor.GetConverter(propertyType).ConvertFrom(value);
            if ((propertyType == typeof(DateTime) || propertyType == typeof(DateTime?)) && value != null)
            {
                obj = ((DateTime)obj).ToFileTimeUtc();
            }

            return Expression.Convert(Expression.Constant(obj), propertyType);
        }

        private void MakeStringCaseInsesitive(Expression member, Expression valueExpression, out Expression exp1, out Expression exp2)
        {
            string name = member.Type.Name;
            exp1 = member;
            exp2 = valueExpression;
            if (name == "String" && member != null && valueExpression != null)
            {
                MethodInfo method = typeof(string).GetMethod("ToLower", new Type[0]);
                exp1 = Expression.Call(member, method);
                exp2 = Expression.Call(valueExpression, method);
            }
        }
    }
}
