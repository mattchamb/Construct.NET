using System;
using System.Globalization;
using System.Reflection;
using Construct.Actions;

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
                result = ResolveForPrimitiveElement<TConstructable>(descriptor.Property, descriptor.Descriptor as PrimitiveElementAttribute);
            }
            else if (descriptor.Descriptor is ComplexElementAttribute)
            {
                result = ResolveForComplexElement<TConstructable>(descriptor.Property, descriptor.Descriptor as ComplexElementAttribute);
            }
            return result;
        }

        protected virtual PlanAction<T> ResolveForComplexElement<T>(PropertyInfo property, ComplexElementAttribute complexElementAttribute)
        {
            var complexActionType = typeof (ComplexAction<,>);
            var genericComplexAction = complexActionType.MakeGenericType(typeof(T), property.PropertyType);
            var instantiatorFunction = _lambdaGenerator.CreateComplexActionInstantiator(genericComplexAction);
            var result = instantiatorFunction(property, complexElementAttribute.DataByteOrder, _lambdaGenerator);
            return (PlanAction<T>) result;
        }

        protected virtual PlanAction<T> ResolveForPrimitiveElement<T>(PropertyInfo property, PrimitiveElementAttribute primitiveElementAttribute)
        {
            var propertyType = property.PropertyType;

            if (propertyType == typeof(int))
            {
                return new Int32Action<T>(property, primitiveElementAttribute.DataByteOrder, _lambdaGenerator);
            }
            if (propertyType == typeof(uint))
            {
                return new UInt32Action<T>(property, primitiveElementAttribute.DataByteOrder, _lambdaGenerator);
            }
            if (propertyType == typeof(byte))
            {
                return new ByteAction<T>(property, primitiveElementAttribute.DataByteOrder, _lambdaGenerator);
            }
            if (propertyType == typeof(sbyte))
            {
                return new SByteAction<T>(property, primitiveElementAttribute.DataByteOrder, _lambdaGenerator);
            }
            if (propertyType == typeof(short))
            {
                return new Int16Action<T>(property, primitiveElementAttribute.DataByteOrder, _lambdaGenerator);
            }
            if (propertyType == typeof(ushort))
            {
                return new UInt16Action<T>(property, primitiveElementAttribute.DataByteOrder, _lambdaGenerator);
            }
            if (propertyType == typeof(long))
            {
                return new Int64Action<T>(property, primitiveElementAttribute.DataByteOrder, _lambdaGenerator);
            }
            if (propertyType == typeof(ulong))
            {
                return new UInt64Action<T>(property, primitiveElementAttribute.DataByteOrder, _lambdaGenerator);
            }
            if (propertyType == typeof(float))
            {
                return new FloatAction<T>(property, primitiveElementAttribute.DataByteOrder, _lambdaGenerator);
            }
            if (propertyType == typeof(double))
            {
                return new DoubleAction<T>(property, primitiveElementAttribute.DataByteOrder, _lambdaGenerator);
            }
            if (propertyType.IsEnum)
            {
                return new EnumAction<T>(property, primitiveElementAttribute.DataByteOrder, _lambdaGenerator);
            }
            throw new ArgumentOutOfRangeException("property",
                                                  string.Format(CultureInfo.CurrentCulture,
                                                                "The property \"{0}\" did not match any of the expected types for a primitive field.",
                                                                property.Name));
        }
    }
}