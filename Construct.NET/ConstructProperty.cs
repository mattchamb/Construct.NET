using System;
using System.Reflection;

namespace Construct.NET
{
    internal class ConstructProperty
    {
        public int SerializationOrder { get; private set; }
        public PropertyInfo Property { get; private set; }
        public NestingType Nesting { get; private set; }

        /// <summary>
        /// Used for fixed length strings.
        /// </summary>
        public int StringLength { get; set; }

        /// <summary>
        /// The name of the property to pass to the Condition function.
        /// </summary>
        public string ConditionArgument { get; set; }
        /// <summary>
        /// The function that says whether to include the conditional field to which it is attached.
        /// Requires the ConditionArgument property to be set.
        /// </summary>
        public Func<object, bool> Condition { get; set; }

        public bool IsArray
        {
            get { return Property.PropertyType.IsArray; }
        }

        public bool IsEnum
        {
            get { return Property.PropertyType.IsEnum; }
        }

        public bool IsConstruct
        {
            get { return Property.PropertyType.IsConstruct(); }
        }

        public ConstructProperty(PropertyInfo property)
        {
            Property = property;
            var constructAttribute = property.GetConstructFieldAttribute();
            SerializationOrder = constructAttribute.SerializationOrder;
            Nesting = constructAttribute.Nesting;
            StringLength = constructAttribute.StringLength;
            ConditionArgument = constructAttribute.ConditionArgument;
            Condition = constructAttribute.Condition;
        }

    }
}