using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Construct
{
    public static class LambdaGenerator
    {
        public static Action<TConstruct, TArg> CreateAssignmentFunction<TConstruct, TArg>(PropertyInfo propertyInfo)
        {
            propertyInfo.RequireNotNull("propertyInfo");
            var constructParameter = Expression.Parameter(typeof (TConstruct), "construct");
            var argParameter = Expression.Parameter(typeof (TArg), "arg");

            var assignmentExpression = Expression.Lambda<Action<TConstruct, TArg>>(
                                            Expression.Assign(Expression.Property(constructParameter, propertyInfo),
                                                         argParameter),
                                            new[] {constructParameter, argParameter});
            return assignmentExpression.Compile();
        }

        public static Action<TConstruct, TArg> CreateAssignmentFunctionWithCast<TConstruct, TArg>(PropertyInfo propertyInfo)
        {
            propertyInfo.RequireNotNull("propertyInfo");
            var constructParameter = Expression.Parameter(typeof (TConstruct), "construct");
            var argParameter = Expression.Parameter(typeof(TArg), "arg");

            var assignmentExpression = Expression.Lambda<Action<TConstruct, TArg>>(
                                            Expression.Assign(Expression.Property(constructParameter, propertyInfo),
                                                         Expression.Convert(argParameter, propertyInfo.PropertyType)),
                                            new[] { constructParameter, argParameter });
            return assignmentExpression.Compile();
        }

        public static Func<TConstruct, TResult> CreateReaderFunction<TConstruct, TResult>(PropertyInfo propertyInfo)
        {
            propertyInfo.RequireNotNull("propertyInfo");

            var constructParameter = Expression.Parameter(typeof (TConstruct), "construct");

            var readerExpression = Expression.Lambda<Func<TConstruct, TResult>>(
                                        Expression.MakeMemberAccess(constructParameter, propertyInfo),
                                        new[] { constructParameter });
            return readerExpression.Compile();
        }
    }
}
