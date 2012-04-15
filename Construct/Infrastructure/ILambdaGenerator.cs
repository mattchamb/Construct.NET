using System;
using System.Reflection;

namespace Construct.Infrastructure
{
    public interface ILambdaGenerator
    {
        Action<TConstruct, TArg> CreateAssignmentFunction<TConstruct, TArg>(PropertyInfo propertyInfo);
        Action<TConstruct, TArg> CreateAssignmentFunctionWithCast<TConstruct, TArg>(PropertyInfo propertyInfo);
        Func<TConstruct, TResult> CreateReaderFunction<TConstruct, TResult>(PropertyInfo propertyInfo);
        Func<PropertyInfo, ByteOrder, ILambdaGenerator, object> CreateComplexActionInstantiationFunction(Type complexActionType);
        Func<TConstruct, bool> CreateConditionCheckingFunction<TConstruct>(string conditionalFunctionName);
    }
}