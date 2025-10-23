using System.Linq.Expressions;

namespace befit.application.ExpressionHelpers
{
    class ParameterReplacer : ExpressionVisitor
    {
        private ParameterExpression _oldParameter;
        private ParameterExpression _newParameter;
        public ParameterReplacer(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node == _oldParameter)
                return _newParameter;

            return base.VisitParameter(node);
        }
    }
}
