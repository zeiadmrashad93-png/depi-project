using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using befit.core.Entities;

namespace befit.core.ExpressionHelpers
{
    static class ExpressionExtensions
    {
        internal static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expressionLeft,
            Expression<Func<T, bool>> expressionRight)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "m");

            ParameterReplacer leftReplacer = new ParameterReplacer(expressionLeft.Parameters[0], parameter);
            ParameterReplacer rightReplacer = new ParameterReplacer(expressionRight.Parameters[0], parameter);

            var left = leftReplacer.Visit(expressionLeft);
            var right = rightReplacer.Visit(expressionRight);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }
    }
}
