using System;
using System.Collections.Generic;
using System.Text;
using Quack.Validation.Contracts;

namespace Quack.Validation
{
    public static class ValidatableObjectExtensions
    {
        /// <summary>
        ///  Wrap T into ValidatableObject<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="ruleFactory"></param>
        public static ValidatableObject<T> ToValidatableObject<T>(this T input, params Func<IValidationRule<T>>[] ruleFactory)
        {
            var validatableObject = new ValidatableObject<T>();
            validatableObject.Value = input;

            if (ruleFactory != null)
            {
                foreach (var rule in ruleFactory)
                {
                    validatableObject.AddValidation(() => rule());
                }
            }

            return validatableObject;
        }
    }
}

