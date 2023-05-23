using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain.Infrastructure;

namespace API.Domain.Services
{
    public interface IApiService
    {
        /// <summary>
        /// Validates an api key
        /// </summary>
        /// <param name="apiKey">Unique API key</param>
        /// <param name="apiName">API name</param>
        /// <returns>Tuple of result and message</returns>
        Task<Tuple<bool, string>> ValidateApiKey(string apiKey, string apiName);

        /// <summary>
        /// Add a log item for a request/response action to
        /// the api
        /// </summary>
        /// <param name="item"><see cref="EndpointLogItem"/></param>
        void AddLog(EndpointLogItem item);

        /// <summary>
        /// Creates a signature from a base string
        /// </summary>
        /// <param name="apikey">Users private key</param>
        /// <param name="toSign">String to sign</param>
        /// <returns>String signature</returns>
        Task<string> CreateSignature(string toSign, string apikey);
    }
}
