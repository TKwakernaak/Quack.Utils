using System;
using System.Collections.Generic;
using System.Text;

namespace Kwaklibs.Utils.UriBuilder.Contracts
{
    public interface IUrlParameterBuilder
    {
        /// <summary>
        /// add the parametername and parameterValue
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        IUrlParameterBuilder AddParameter(string parameterName, string parameterValue);


        /// <summary>
        /// adds an parameter if it the parametername doesnt exist, overrides it if it does
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        IUrlParameterBuilder AddOrUpdateParameter(string parameterName, string parameterValue);

        /// <summary>
        /// Add paging
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageValue"></param>
        /// <returns></returns>
        IUrlParameterBuilder AddPaging(int page, int pageSize, string PageParameter = "Page", string PageSizeParameter = "PageSize");

        /// <summary>
        /// make sure the Tostring of T gives the correct value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        IUrlParameterBuilder AddParameters<T>(string parameterName, params T[] parameterValues) where T : struct;

        IUrlParameterBuilder AddParameters(string parameterName, params string[] parameterValues);

    }
}
