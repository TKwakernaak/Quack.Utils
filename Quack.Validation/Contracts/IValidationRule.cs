using System;
using System.Collections.Generic;
using System.Text;

namespace Quack.Validation.Contracts
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; }
        bool Check(T value);
    }

}
