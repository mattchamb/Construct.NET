using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Construct
{
    public class LambdaGenerator : ILambdaGenerator
    {
        public Action<TConstruct, TArg> CreateAssignmentFunction<TConstruct, TArg>(PropertyInfo propertyInfo)
        {
            Require.NotNull(propertyInfo, "propertyInfo");
            var constructParameter = Expression.Parameter(typeof (TConstruct), "construct");
            var argParameter = Expression.Parameter(typeof (TArg), "arg");

            var assignmentExpression = Expression.Lambda<Action<TConstruct, TArg>>(
                                            Expression.Assign(Expression.Property(constructParameter, propertyInfo),
                                                         argParameter),
                                            constructParameter, argParameter);
            return assignmentExpression.Compile();
        }

        public Action<TConstruct, TArg> CreateAssignmentFunctionWithCast<TConstruct, TArg>(PropertyInfo propertyInfo)
        {
            Require.NotNull(propertyInfo, "propertyInfo");
            var constructParameter = Expression.Parameter(typeof (TConstruct), "construct");
            var argParameter = Expression.Parameter(typeof(TArg), "arg");

            var assignmentExpression = Expression.Lambda<Action<TConstruct, TArg>>(
                                            Expression.Assign(Expression.Property(constructParameter, propertyInfo),
                                                         Expression.Convert(argParameter, propertyInfo.PropertyType)),
                                            constructParameter, argParameter);
            return assignmentExpression.Compile();
        }

        public Func<TConstruct, TResult> CreateReaderFunction<TConstruct, TResult>(PropertyInfo propertyInfo)
        {
            Require.NotNull(propertyInfo, "propertyInfo");

            var constructParameter = Expression.Parameter(typeof (TConstruct), "construct");

            var readerExpression = Expression.Lambda<Func<TConstruct, TResult>>(
                                        Expression.MakeMemberAccess(constructParameter, propertyInfo),
                                        constructParameter);
            return readerExpression.Compile();
        }

        public Func<PropertyInfo, ByteOrder, ILambdaGenerator, object> CreateComplexActionInstantiator(Type complexActionType)
        {
            Require.NotNull(complexActionType, "complexActionType");

            var propertyInfoType = typeof (PropertyInfo);
            var byteOrderType = typeof (ByteOrder);
            var lambdaGeneratorType = typeof (ILambdaGenerator);

            var actionConstructor = complexActionType.GetConstructor(new[] { propertyInfoType, byteOrderType, lambdaGeneratorType });

            if(actionConstructor == null)
            {
                throw new ArgumentException(
                    "The class represented by the complexActionType Type does not have the required constructor.",
                    "complexActionType");
            }

            var propArg = Expression.Parameter(propertyInfoType, "propArg");
            var byteOrderArg = Expression.Parameter(byteOrderType, "byteOrderArg");
            var lambdaGeneratorArg = Expression.Parameter(lambdaGeneratorType, "lambdaGeneratorArg");

            var creatorExpression = Expression.Convert(Expression.New(actionConstructor, propArg, byteOrderArg, lambdaGeneratorArg), typeof(object));

            var creatorLambda = Expression.Lambda<Func<PropertyInfo, ByteOrder, ILambdaGenerator, object>>(creatorExpression, propArg, byteOrderArg, lambdaGeneratorArg);
            return creatorLambda.Compile();
        }

    }
}
