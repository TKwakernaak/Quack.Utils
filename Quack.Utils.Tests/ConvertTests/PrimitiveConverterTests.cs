using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Quack.Utils.Convert;
using Xunit;

namespace Quack.Utils.Tests.ConvertTests
{
    public class PrimitiveConverterTests
    {
        [Fact]
        public void WhenConvertingStringToInt_ThenConversionIsSuccess()
        {
            string input = "5";

            var result = input.To<int>();

            result.Should().BeOfType(typeof(int));
        }

        [Fact]
        public void WhenConvertingStringToDouble_ThenConversionIsSuccess()
        {
            string input = "5,00";

            var result = input.To<double>();

            result.Should().BeOfType(typeof(double));
        }
    }
}
