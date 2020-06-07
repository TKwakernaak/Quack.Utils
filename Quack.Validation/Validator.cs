using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Quack.Validation.Contracts;

namespace Quack.Validation
{
    public class Validator : IValidator
    {
        /// <summary>
        /// Validates all public validatableObjects in the supplied object.
        /// </summary>
        /// <param name="checkableObject"></param>
        public ValidationResult Validate(object checkableObject)
        {
            var types = checkableObject.GetType()
                                       .GetProperties(BindingFlags.DeclaredOnly |
                                                      BindingFlags.Instance |
                                                      BindingFlags.Public);

            foreach (var item in types)
            {
                var type = item.PropertyType;
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ValidatableObject<>))
                {
                    //we use nameof to ensure that refactoring won't break the code, the type <string> obviously has no meaning.
                    MethodInfo validateMethodRef = type.GetMethod(nameof(ValidatableObject<string>.Validate));
                    object objectInstance = item.GetValue(checkableObject);

                    var result = validateMethodRef?.Invoke(objectInstance, null);

                    if (result is bool isValid && isValid)
                        continue;

                    //retieve the data in the errors list
                    PropertyInfo errorProperty = type.GetProperty(nameof(ValidatableObject<string>.Errors));
                    var errors = errorProperty?.GetValue(objectInstance);

                    if (errors is List<string> errorList && errorList.Any())
                        return new ValidationResult(false, errorList);
                }
            }
            //return emtpy array instead of null, following microsofts guidelines to never return null for an collection
            return new ValidationResult(true, new List<string>());
        }
    }
}

public struct ValidationResult
{
    public ValidationResult(bool isValid, List<string> errors)
    {
        IsValid = isValid;
        ValidationMessages = errors;
    }
    public bool IsValid { get; private set; }

    public bool IsInvalid => !IsValid;

    public List<string> ValidationMessages { get; private set; }
}