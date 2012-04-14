using System;
using System.Collections.Generic;
using System.Reflection;

namespace Construct
{
    public class CachedLambdaGenerator : ILambdaGenerator
    {
        private readonly ILambdaGenerator _lambdaGenerator;
        private readonly Dictionary<FunctionCacheKey, object> _assignmentFunctionCache;
        private readonly Dictionary<FunctionCacheKey, object> _assignmentCastFunctionCache;
        private readonly Dictionary<FunctionCacheKey, object> _readerFunctionCache;
        private readonly Dictionary<Type, Func<PropertyInfo, ByteOrder, ILambdaGenerator, object>> _instantiatorCache;

        public CachedLambdaGenerator()
        {
            _assignmentFunctionCache = new Dictionary<FunctionCacheKey, object>();
            _assignmentCastFunctionCache = new Dictionary<FunctionCacheKey, object>();
            _readerFunctionCache = new Dictionary<FunctionCacheKey, object>();
            _instantiatorCache = new Dictionary<Type, Func<PropertyInfo, ByteOrder, ILambdaGenerator, object>>();
            _lambdaGenerator = new LambdaGenerator();
        }

        public Action<TConstruct, TArg> CreateAssignmentFunction<TConstruct, TArg>(PropertyInfo propertyInfo)
        {
            var key = new FunctionCacheKey(typeof (TConstruct), typeof (TArg), propertyInfo);
            object cachedFunction;
            if(_assignmentFunctionCache.TryGetValue(key, out cachedFunction))
            {
                return cachedFunction as Action<TConstruct, TArg>;
            }
            var function = _lambdaGenerator.CreateAssignmentFunction<TConstruct, TArg>(propertyInfo);
            _assignmentFunctionCache.Add(key, function);
            return function;
        }

        public Action<TConstruct, TArg> CreateAssignmentFunctionWithCast<TConstruct, TArg>(PropertyInfo propertyInfo)
        {
            var key = new FunctionCacheKey(typeof(TConstruct), typeof(TArg), propertyInfo);
            object cachedFunction;
            if (_assignmentCastFunctionCache.TryGetValue(key, out cachedFunction))
            {
                return cachedFunction as Action<TConstruct, TArg>;
            }
            var function = _lambdaGenerator.CreateAssignmentFunctionWithCast<TConstruct, TArg>(propertyInfo);
            _assignmentCastFunctionCache.Add(key, function);
            return function;
        }

        public Func<TConstruct, TResult> CreateReaderFunction<TConstruct, TResult>(PropertyInfo propertyInfo)
        {
            var key = new FunctionCacheKey(typeof(TConstruct), typeof(TResult), propertyInfo);
            object cachedFunction;
            if (_readerFunctionCache.TryGetValue(key, out cachedFunction))
            {
                return cachedFunction as Func<TConstruct, TResult>;
            }
            var function = _lambdaGenerator.CreateReaderFunction<TConstruct, TResult>(propertyInfo);
            _readerFunctionCache.Add(key, function);
            return function;
        }

        public Func<PropertyInfo, ByteOrder, ILambdaGenerator, object> CreateComplexActionInstantiationFunction(Type complexActionType)
        {
            Func<PropertyInfo, ByteOrder, ILambdaGenerator, object> cachedFunction;
            if (_instantiatorCache.TryGetValue(complexActionType, out cachedFunction))
            {
                return cachedFunction;
            }
            var function = _lambdaGenerator.CreateComplexActionInstantiationFunction(complexActionType);
            _instantiatorCache.Add(complexActionType, function);
            return function;
        }
    }
}