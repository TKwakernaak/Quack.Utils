using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Quack.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfAttribute : ValidationAttribute
    {
        private readonly string _conditionalProperty;
        private readonly object _conditionalPropertyValue;

        public override bool RequiresValidationContext => true;

        public RequiredIfAttribute(string otherProperty, object otherPropertyValue)
          : base($"Error when creating instance of {nameof(RequiredIfAttribute)}")
        {
            _conditionalProperty = otherProperty;
            _conditionalPropertyValue = otherPropertyValue;
        }

        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException("validationContext");
            }

            PropertyInfo otherProperty = validationContext.ObjectType.GetProperty(_conditionalProperty);
            if (otherProperty == null)
            {
                string message = string.Format("Could not find a property named '{0}'.", _conditionalProperty);
                return new System.ComponentModel.DataAnnotations.ValidationResult(message);
            }

            object actualPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance);

            // check if this value is actually required and validate it
            if (Equals(actualPropertyValue, _conditionalPropertyValue))
            {
                if (value == null)
                    return new System.ComponentModel.DataAnnotations.ValidationResult(GetErrorMessage(validationContext.DisplayName));

                // additional check for strings so they're not empty
                if (value is string valueAsString && string.IsNullOrWhiteSpace(valueAsString))
                    return new System.ComponentModel.DataAnnotations.ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return System.ComponentModel.DataAnnotations.ValidationResult.Success;
        }

        private string GetErrorMessage(string displayName)
        {
            return $"Property {displayName} must have a value!";
        }
    }
}
