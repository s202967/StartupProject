using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace StartupProject.Core.Security.UserActivity
{
    public class HttpContextClientInfoProvider : IClientInfoProvider
    {
        public string BrowserInfo
        {
            get
            {
                var httpContext = _httpContextAccessor.HttpContext;
                return httpContext?.Request?.Headers?["User-Agent"];
            }
        }

        /// <summary>
        /// Gets IP address of client
        /// </summary>
        /// <returns></returns>
        public string ClientIpAddress
        {
            get
            {
                if (!IsRequestAvailable())
                    return string.Empty;

                var result = string.Empty;
                try
                {
                    //first try to get IP address from the forwarded header
                    if (_httpContextAccessor.HttpContext.Request.Headers != null)
                    {
                        //the X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a client
                        //connecting to a web server through an HTTP proxy or load balancer
                        //var forwardedHttpHeaderKey = "X-FORWARDED-FOR";
                        //if (!string.IsNullOrEmpty(_hostingConfig.ForwardedHttpHeader))
                        //{
                        //    //but in some cases server use other HTTP header
                        //    //in these cases an administrator can specify a custom Forwarded HTTP header (e.g. CF-Connecting-IP, X-FORWARDED-PROTO, etc)
                        //    forwardedHttpHeaderKey = _hostingConfig.ForwardedHttpHeader;
                        //}

                        //var forwardedHeader = _httpContextAccessor.HttpContext.Request.Headers[forwardedHttpHeaderKey];
                        //if (!StringValues.ImsNullOrEmpty(forwardedHeader))
                        //    result = forwardedHeader.FirstOrDefault();
                    }

                    //if this header not exists try get connection remote IP address
                    if (_httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
                        result = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                }
                catch
                {
                    return string.Empty;
                }

                //some of the validation
                if (result != null && result.Equals("::1", StringComparison.InvariantCultureIgnoreCase))
                    result = "127.0.0.1";

                //remove port
                if (!string.IsNullOrEmpty(result))
                    result = result.Split(':').FirstOrDefault();

                return result;
            }
        }

        public string ComputerName
        {
            get
            {
                return null; //TODO: Implement!
            }
        }

        public string Uri
        {
            get
            {
                var request = _httpContextAccessor.HttpContext.Request;
                var builder = new UriBuilder
                {
                    Scheme = request.Scheme,
                    Host = request.Host.Host,
                    Path = request.Path.ToString(),
                    Query = request.QueryString.ToString()
                };
                return builder.Uri.ToString();
            }
        }

        public string BaseUri
        {
            get
            {
                var request = _httpContextAccessor.HttpContext.Request;
                return $"{request.Scheme}://{request.Host.Value}{request.PathBase.Value}";
            }
        }

        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Creates a new <see cref="HttpContextClientInfoProvider"/>.
        /// </summary>
        public HttpContextClientInfoProvider(IHttpContextAccessor httpContextAccessor, ILogger<HttpContextClientInfoProvider> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        /// <summary>
        /// Check whether current HTTP request is available
        /// </summary>
        /// <returns>True if available; otherwise false</returns>
        protected virtual bool IsRequestAvailable()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
                return false;

            try
            {
                if (_httpContextAccessor.HttpContext.Request == null)
                    return false;
            }
            catch (Exception)
            {
                //ignore
                return false;
            }

            return true;
        }

        public bool IsLocal
        {
            get
            {
                if (!IsRequestAvailable())
                    return false;

                var result = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                if (result != null && result.Equals("::1", StringComparison.InvariantCultureIgnoreCase))
                    return true;

                return false;
            }
        }
    }
}
