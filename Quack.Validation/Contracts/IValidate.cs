using System;
using System.Collections.Generic;
using System.Text;

namespace Quack.Validation.ValidatableObject.Contracts
{
    /// <summary>
    /// Detemine if an rule is valid
    /// </summary>
    public interface IValidity
    {
        bool IsValid { get; }
    }
}

