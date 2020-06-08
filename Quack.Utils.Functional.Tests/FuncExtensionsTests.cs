using Xunit;
using Quack.Utils.Functional;
using System;
using FluentAssertions;

namespace Quack.Utils.Functional.Tests
{
    public class Tests
    {
        [Fact]
        public void WhenWhenExtensionsIsCalled_ThenCurryIsApplied()
        {
            int input = 5;
            Func<int, int> multiplyFunc = ab => (ab * 2);

            var result = multiplyFunc.Then(multiplyFunc).Invoke(5);

            //5 * 2 * 2
            result.Should().Be(20);
        }
    }
}