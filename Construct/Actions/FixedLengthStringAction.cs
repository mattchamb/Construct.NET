using System;
using System.Reflection;
using System.Text;
using Construct.Exceptions;
using Construct.Infrastructure;

namespace Construct.Actions
{
    public class FixedLengthStringAction<TConstructable> : PlanAction<TConstructable>
    {
        private readonly int _length;
        private readonly Encoding _encoding;
        private readonly Action<TConstructable, string> _assignmentFunction;
        private readonly Func<TConstructable, string> _readerFunction;

        public FixedLengthStringAction(PropertyInfo property, ByteOrder inputByteOrder, string conditionFunction, int length, Encoding encoding,
                                       ILambdaGenerator lambdaGenerator)
            : base(property, inputByteOrder, conditionFunction, lambdaGenerator)
        {
            Require.That("length", length > 0);
            _length = length;
            _encoding = encoding;
            _assignmentFunction = LambdaGenerator.CreateAssignmentFunction<TConstructable, string>(Property);
            _readerFunction = LambdaGenerator.CreateReaderFunction<TConstructable, string>(Property);
        }

        protected override void Read(TConstructable obj, ConstructReaderStream inputStream, IConstructPlanner constructPlanner)
        {
            var data = inputStream.ReadBytes(_length);
            var strValue = _encoding.GetString(data);
            _assignmentFunction(obj, strValue);
        }

        protected override void Write(TConstructable obj, ConstructWriterStream outputStream, IConstructPlanner constructPlanner)
        {
            var strValue = _readerFunction(obj);
            var data = _encoding.GetBytes(strValue);
            if(data.Length != _length)
            {
                throw new FixedLengthStringException("The length of the string field (in bytes) is different from the expected field length.");
            }
            outputStream.WriteBytes(data);
        }
    }
}
