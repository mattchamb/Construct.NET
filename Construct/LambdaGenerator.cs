using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Construct.Exceptions;
using Construct.Infrastructure;

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

        public Func<PropertyInfo, ByteOrder, ILambdaGenerator, object> CreateComplexActionInstantiationFunction(Type complexActionType)
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

        public Func<TConstruct, bool> CreateConditionCheckingFunction<TConstruct>(string conditionalFunctionName)
        {
            Require.NotNullOrEmpty(conditionalFunctionName, "conditionalFunctionName");
            var constructType = typeof (TConstruct);
            Require.That("TConstruct", constructType.IsConstructable());

            var conditionalMethodInfo = constructType.GetMethod(conditionalFunctionName, Type.EmptyTypes);
            if(conditionalMethodInfo == null)
            {
                throw new ConditionalBindingException(
                    string.Format(CultureInfo.InvariantCulture,
                                  "Could not find appropriate member for conditional function name \"{0}\"",
                                  conditionalFunctionName));
            }
            if(conditionalMethodInfo.ReturnType != typeof(bool))
            {
                throw new ConditionalBindingException(
                    string.Format(CultureInfo.InvariantCulture,
                                  "The method found for conditional name \"{0}\" did not return a boolean value. " +
                                  "The method found was \"{1}\"", conditionalFunctionName, conditionalMethodInfo));
            }
            var constructArg = Expression.Parameter(constructType, "construct");

            var invokeExpression = Expression.Call(constructArg, conditionalMethodInfo);

            var invokeLambda = Expression.Lambda<Func<TConstruct, bool>>(invokeExpression, constructArg);
            return invokeLambda.Compile();
        }
    }
}