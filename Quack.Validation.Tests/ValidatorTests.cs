using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Quack.Validation.Rules;
using Quack.Validation.Tests.Studs;
using Xunit;

namespace Quack.Validation.Tests
{
    public class ValidatorTests
    {
        [Fact]
        public void ValidatorTests_WithTag_Success()
        {
            var sut = new ValidatorStud();
            sut.validatableObject.AddValidation(() => new IsNotNullOrEmptyRule().WithValidationMessage("cannot be null or empty"))
                                 .AddValidation(() => new IsValidConditionRule<string>().Ensure(e => e != null).SetValidationMessage("message2"));

            var validationResult = new Validator().Validate(sut);

            validationResult.IsValid.Should().BeFalse();
            validationResult.IsInvalid.Should().BeTrue();
        }
    }
}
