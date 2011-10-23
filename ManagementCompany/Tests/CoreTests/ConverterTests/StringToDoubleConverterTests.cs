using System;
using Core.Converters;
using NUnit.Framework;

namespace Tests.CoreTests.ConverterTests
{
    [TestFixture]
    public class StringToDoubleConverterTests
    {
        [Test]
        public void Convert_PassValidValue_MustReturnConvertedValue()
        {
            const string convertingValue = "3,45";
            const double expextedValue = 3.45;
            var converter = new StringToDoubleConveter();
            var result = converter.Convert(convertingValue, typeof(Double), null, null);

            Assert.AreEqual(expextedValue, result);
        }

        [Test]
        public void Convert_PassInvalidValue_ThrowsArgumentException()
        {
            const string convertingValue = "3,45a";
            var converter = new StringToDoubleConveter();
            Assert.Throws<ArgumentException>(() => converter.Convert(convertingValue, typeof(Double), null, null));
        }
    }
}
