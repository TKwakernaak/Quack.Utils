using System;
using System.Collections.Generic;
using System.Text;

namespace Quack.Utils.UriBuilder.Contracts
{
    public interface IRelativeUrlBuilder : IBuildUrl
    {
        IUrlParameterBuilder SetRelativeUri(string relativeUri);
    }
}
