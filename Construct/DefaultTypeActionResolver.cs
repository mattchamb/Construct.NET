using System;
using System.Globalization;
using System.Reflection;
using Construct.Actions;
using Construct.Attributes;
using Construct.Infrastructure;

namespace Construct
{
    public class DefaultTypeActionResolver : ITypeActionResolver
    {
        private readonly ILambdaGenerator _lambdaGenerator;

        public DefaultTypeActionResolver(ILambdaGenerator lambdaGenerator)
        {
            _lambdaGenerator = lambdaGenerator;
        }

        public PlanAction<TConstructable> ResolveActionForType<TConstructable>(PropertyConstructDescriptor descriptor)
        {
            PlanAction<TConstructable> result = null;
            if (descriptor.Descriptor is PrimitiveElementAttribute)
            {
                result = ResolveForPrimitiveElement<TConstructable>(descriptor.Property,
                                                                    descriptor.Descriptor as PrimitiveElementAttribute);
            }
            else if (descriptor.Descriptor is ComplexElementAttribute)
            {
                result = ResolveForComplexElement<TConstructable>(descriptor.Property,
                                                                  descriptor.Descriptor as ComplexElementAttribute);
            }
            else if (descriptor.Descriptor is FixedLengthStringElementAttribute)
            {
                result = ResolveForFixedLengthStringElement<TConstructable>(descriptor.Property,
                                                                            descriptor.Descriptor as
                                                                            FixedLengthStringElementAttribute);
            }
            else
            {
                throw new ArgumentException(string.Format("Unknown constructable element type \"{0}\"",
                                                          descriptor.Descriptor.GetType().FullName));
            }
            return result;
        }

        private PlanAction<T> ResolveForFixedLengthStringElement<T>(PropertyInfo propertyInfo,
                                                                    FixedLengthStringElementAttribute stringElement)
        {
            return new FixedLengthStringAction<T>(propertyInfo, ByteOrder.None,
                                                  stringElement.Condition,
                                                  stringElement.Length,
                                                  stringElement.TextEncoding, _lambdaGenerator);
        }

        protected virtual PlanAction<T> ResolveForComplexElement<T>(PropertyInfo propertyInfo,
                                                                    ComplexElementAttribute complexElementAttribute)
        {
            var complexActionType = typeof (ComplexAction<,>);
            var genericComplexAction = complexActionType.MakeGenericType(typeof (T), propertyInfo.PropertyType);
            var instantiatorFunction = _lambdaGenerator.CreateComplexActionInstantiationFunction(genericComplexAction);
            var result = instantiatorFunction(propertyInfo, complexElementAttribute.DataByteOrder, _lambdaGenerator);
            return (PlanAction<T>) result;
        }

        protected virtual PlanAction<T> ResolveForPrimitiveElement<T>(PropertyInfo propertyInfo,
                                                                      PrimitiveElementAttribute
                                                                          primitiveElementAttribute)
        {
            var propertyType = propertyInfo.PropertyType;

            if (propertyType == typeof (int))
            {
                return new Int32Action<T>(propertyInfo, primitiveElementAttribute.DataByteOrder,
                                          primitiveElementAttribute.Condition, _lambdaGenerator);
            }
            if (propertyType == typeof (uint))
            {
                return new UInt32Action<T>(propertyInfo, primitiveElementAttribute.DataByteOrder,
                                           primitiveElementAttribute.Condition, _lambdaGenerator);
            }
            if (propertyType == typeof (byte))
            {
                return new ByteAction<T>(propertyInfo, primitiveElementAttribute.DataByteOrder,
                                         primitiveElementAttribute.Condition, _lambdaGenerator);
            }
            if (propertyType == typeof (sbyte))
            {
                return new SByteAction<T>(propertyInfo, primitiveElementAttribute.DataByteOrder,
                                          primitiveElementAttribute.Condition, _lambdaGenerator);
            }
            if (propertyType == typeof (short))
            {
                return new Int16Action<T>(propertyInfo, primitiveElementAttribute.DataByteOrder,
                                          primitiveElementAttribute.Condition, _lambdaGenerator);
            }
            if (propertyType == typeof (ushort))
            {
                return new UInt16Action<T>(propertyInfo, primitiveElementAttribute.DataByteOrder,
                                           primitiveElementAttribute.Condition, _lambdaGenerator);
            }
            if (propertyType == typeof (long))
            {
                return new Int64Action<T>(propertyInfo, primitiveElementAttribute.DataByteOrder,
                                          primitiveElementAttribute.Condition, _lambdaGenerator);
            }
            if (propertyType == typeof (ulong))
            {
                return new UInt64Action<T>(propertyInfo, primitiveElementAttribute.DataByteOrder,
                                           primitiveElementAttribute.Condition, _lambdaGenerator);
            }
            if (propertyType == typeof (float))
            {
                return new FloatAction<T>(propertyInfo, primitiveElementAttribute.DataByteOrder,
                                          primitiveElementAttribute.Condition, _lambdaGenerator);
            }
            if (propertyType == typeof (double))
            {
                return new DoubleAction<T>(propertyInfo, primitiveElementAttribute.DataByteOrder,
                                           primitiveElementAttribute.Condition, _lambdaGenerator);
            }
            if (propertyType.IsEnum)
            {
                return new EnumAction<T>(propertyInfo, primitiveElementAttribute.DataByteOrder,
                                         primitiveElementAttribute.Condition, _lambdaGenerator);
            }
            throw new ArgumentOutOfRangeException("propertyInfo",
                                                  string.Format(CultureInfo.CurrentCulture,
                                                                "The property \"{0}\" did not match any of the expected types for a primitive field.",
                                                                propertyInfo.Name));
        }
    }
}