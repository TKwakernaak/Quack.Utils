using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Quack.Validation.Rules;
using Quack.Validation.Tests.Studs;
using Xunit;

namespace Quack.Validation.Tests
{
    public class WrapExtensionTests
    {

        [Fact]
        public void ValidatableObject_IsNotNullOrEmpty_Success()
        {
            //arrange
            var stud = new TestWrapperStud();
            stud.Name = "Name1";
            stud.Age = 15;

            //act
            var result = stud.ToValidatableObject(null);

            result.Should().BeOfType(typeof(ValidatableObject<TestWrapperStud>));
            result.Value.Age.Should().Be(stud.Age);
            result.Value.Name.Should().Be(stud.Name);
        }

        [Fact]
        public void ValidatableObject_AddingRulesAddsRules_Success()
        {
            //arrange
            var stud = new TestWrapperStud();
            stud.Name = "Name1";
            stud.Age = 15;
            var rule = new IsValidConditionRule<TestWrapperStud>().Ensure(e => e.Age != 100);

            //act
            var result = stud.ToValidatableObject(() => rule);

            result.Should().BeOfType(typeof(ValidatableObject<TestWrapperStud>));
            result.Value.Age.Should().Be(stud.Age);
            result.Value.Name.Should().Be(stud.Name);
        }
    }
}
