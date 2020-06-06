using System;
using Kwaklibs.Utils.UriBuilder.Contracts;

namespace Kwaklibs.Utils.UriBuilder
{
    /// <summary>
    /// provides an abstraction over various Httputilities to easily build uri's
    /// </summary>
    public class UrlBuilder : IUrlBuilderStarter, IUrlParameterBuilder, IRelativeUrlBuilder, IBuildUrl
    {
        Uri _baseUri;

        private UrlBuilder()
        {
        }

        /// <summary>
        /// Creates a new instance of the Urlbuilder class;
        /// </summary>
        public static IUrlBuilderStarter Create => new UrlBuilder();

        public IRelativeUrlBuilder SetBaseUri(string baseuri)
        {
            if (string.IsNullOrWhiteSpace(baseuri))
                throw new ArgumentException($"no base url supplied: {baseuri}");

            var baseUri = baseuri.Trim().TrimEnd('/');

            _baseUri = new Uri(baseUri);
            return this;
        }

        public IUrlParameterBuilder SetRelativeUri(string relativeUri)
        {
            _baseUri = _baseUri.Append(relativeUri);
            return this;
        }
        public IUrlParameterBuilder AddOrUpdateParameter(string parameterName, string parameterValue)
        {
            _baseUri = _baseUri.AddOrUpdateParameter(parameterName, parameterValue);
            return this;
        }
        public IUrlParameterBuilder AddParameter(string parameterName, string parameterValue)
        {
            _baseUri = _baseUri.AddParameter(parameterName, parameterValue);
            return this;
        }

        public IUrlParameterBuilder AddParameters<T>(string parameterName, params T[] parameterValues) where T : struct
        {
            foreach (var parameterValue in parameterValues)
                _baseUri = _baseUri.AddParameter(parameterName, parameterValue.ToString());

            return this;
        }

        public IUrlParameterBuilder AddParameters(string parameterName, params string[] parameterValues)
        {
            foreach (var parameterValue in parameterValues)
                _baseUri = _baseUri.AddParameter(parameterName, parameterValue.ToString());

            return this;
        }

        public string Build()
        {
            return _baseUri.ToString();
        }

        public IUrlParameterBuilder AddPaging(int page, int pageSize, string PageParameter = "Page", string PageSizeParameter = "PageSize")
        {
            _baseUri = _baseUri.AddParameter(PageParameter, page.ToString());
            _baseUri = _baseUri.AddParameter(PageSizeParameter, pageSize.ToString());

            return this;
        }
    }
}
