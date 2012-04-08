using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Construct
{
    public static class LambdaGenerator
    {
        public static Action<TConstruct, TArg> CreateAssignmentFunction<TConstruct, TArg>(PropertyInfo propInfo)
        {
            propInfo.RequireNotNull("propInfo");
            var constructParameter = Expression.Parameter(typeof (TConstruct), "construct");
            var argParameter = Expression.Parameter(typeof (TArg), "arg");

            var assignmentExpression = Expression.Lambda<Action<TConstruct, TArg>>(
                                            Expression.Assign(Expression.Property(constructParameter, propInfo),
                                                         argParameter),
                                            new[] {constructParameter, argParameter});
            return assignmentExpression.Compile();
        }

        public static Action<TConstruct, TArg> CreateAssignmentFunctionWithCast<TConstruct, TArg>(PropertyInfo propInfo)
        {
            propInfo.RequireNotNull("propInfo");
            var constructParameter = Expression.Parameter(typeof(TConstruct), "construct");
            var argParameter = Expression.Parameter(typeof(TArg), "arg");

            var assignmentExpression = Expression.Lambda<Action<TConstruct, TArg>>(
                                            Expression.Assign(Expression.Property(constructParameter, propInfo),
                                                         Expression.Convert(argParameter, propInfo.PropertyType)),
                                            new[] { constructParameter, argParameter });
            return assignmentExpression.Compile();
        }
    }
}
