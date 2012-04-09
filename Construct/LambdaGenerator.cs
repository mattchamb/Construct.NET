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
                                            constructParameter, argParameter);
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
                                            constructParameter, argParameter);
            return assignmentExpression.Compile();
        }

        public static Func<TConstruct, TResult> CreateReaderFunction<TConstruct, TResult>(PropertyInfo propertyInfo)
        {
            propertyInfo.RequireNotNull("propertyInfo");

            var constructParameter = Expression.Parameter(typeof (TConstruct), "construct");

            var readerExpression = Expression.Lambda<Func<TConstruct, TResult>>(
                                        Expression.MakeMemberAccess(constructParameter, propertyInfo),
                                        constructParameter);
            return readerExpression.Compile();
        }

        public static Func<PropertyInfo, ByteOrder, object> CreateComplexActionInstantiator(Type complexActionType)
        {
            complexActionType.RequireNotNull("complexActionType");

            var propertyInfoType = typeof (PropertyInfo);
            var byteOrderType = typeof (ByteOrder);

            var actionConstructor = complexActionType.GetConstructor(new[] { propertyInfoType, byteOrderType });

            if(actionConstructor == null)
            {
                throw new ArgumentException(
                    "The class represented by the complexActionType Type does not have the required constructor.",
                    "complexActionType");
            }

            var propArg = Expression.Parameter(propertyInfoType, "propArg");
            var byteOrderArg = Expression.Parameter(byteOrderType, "byteOrderArg");

            var creatorExpression = Expression.Convert(Expression.New(actionConstructor), typeof(object));

            var creatorLambda = Expression.Lambda<Func<PropertyInfo, ByteOrder, object>>(creatorExpression, propArg,
                                                                                         byteOrderArg);
            return creatorLambda.Compile();
        }

    }
}
