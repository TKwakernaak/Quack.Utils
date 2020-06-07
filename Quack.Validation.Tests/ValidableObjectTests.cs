using FluentAssertions;
using Quack.Validation.Rules;
using Xunit;

namespace Quack.Validation.Tests
{

    public class ValidableObjectTests
    {
        [Fact]
        public void ValidatableObject_IsNotNullOrEmpty_Success()
        {
            //arrange
            ValidatableObject<string> validatableObject = new ValidatableObject<string>();
            validatableObject.Validations.Add(new IsNotNullOrEmptyRule().WithValidationMessage("string cannot be null or empty"));

            validatableObject.Value = "Hello";

            var result = validatableObject.Validate();

            result.Should().BeTrue();
        }

        [Fact]
        public void ValidatableObject_IsNotNullOrEmpty_Fail()
        {
            //arrange
            ValidatableObject<string> validatableObject = new ValidatableObject<string>();
            validatableObject.Validations.Add(new IsNotNullOrEmptyRule()
                                         .WithValidationMessage("string cannot be null or empty"));

            validatableObject.Value = "";

            var result = validatableObject.Validate();

            result.Should().BeFalse();
        }

        [Fact]
        public void ValidatableObject_ExcludeConditionShouldPass_Success()
        {
            //arrange
            ValidatableObject<string> validatableObject = new ValidatableObject<string>();
            validatableObject.Validations.Add(new IsValidConditionRule<string>()
                                         .Ensure(str => !string.IsNullOrWhiteSpace(str))
                                         .SetValidationMessage("string cannot be null or empty"));

            validatableObject.Value = "abc";

            var isValid = validatableObject.Validate();

            isValid.Should().BeTrue();
        }


        [Fact]
        public void ValidatableObject_ExcludeConditionShouldntPass_Fail()
        {
            //arrange
            ValidatableObject<string> validatableObject = new ValidatableObject<string>();
            validatableObject.Validations.Add(new IsValidConditionRule<string>()
                                         .Ensure(str => !string.IsNullOrWhiteSpace(str))
                                         .SetValidationMessage("string cannot be null or empty"));

            validatableObject.Value = null;

            var isValid = validatableObject.Validate();

            isValid.Should().BeFalse();
        }

        [Fact]
        public void ValidatableObject_ExcludeConditionShouldFail_Fail()
        {
            //arrange
            ValidatableObject<string> validatableObject = new ValidatableObject<string>();
            validatableObject.Validations.Add(new IsValidConditionRule<string>()
                                         .Ensure(e => e != null)
                                         .SetValidationMessage("string cannot be null or empty"));

            validatableObject.Value = null;

            var isValid = validatableObject.Validate();

            isValid.Should().BeFalse();
        }
    }
}
