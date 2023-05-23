using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Domain.Infrastructure;
using API.Domain.Repository;
using API.Domain.Services;
using API.Utilities;

namespace APIApplication.Services
{
    public class ApiService : IApiService
    {
        private readonly IApiUserRecords apiUserRecords;
        private readonly IApiLogRecords apiLogRecords;

        /// <summary>
        /// Initialize the <see cref="ApiService"/> class
        /// </summary>
        /// <param name="apiUserRecords">Access to the API users table</param>
        /// <param name="apiLogRecords">Access to the API logs table</param>
        public ApiService(IApiUserRecords apiUserRecords, IApiLogRecords apiLogRecords)
        {
            this.apiUserRecords = apiUserRecords;
            this.apiLogRecords = apiLogRecords;
        }

        /// <summary>
        /// Validates an api key
        /// </summary>
        /// <param name="apiKey">Unique API key</param>
        /// <param name="reason">If validation fails supply a reason</param>
        /// <param name="apiName">API name</param>
        /// <returns>bool valid or not</returns>
        public async Task<Tuple<bool, string>> ValidateApiKey(string apiKey, string apiName)
        {
            string reason;
            try
            {
                var guid = GuidShortener.Decode(apiKey);
                var apiUser = apiUserRecords.Get(guid);

                if (apiUser == null)
                {
                    reason = "Invalid API key.";
                    return new Tuple<bool, string>(false, reason);
                }
                if (!apiUser.HasAccess(apiName))
                {
                    reason = "You do not have access to this API.";
                    return new Tuple<bool, string>(false, reason);
                }

                reason = string.Empty;
                return new Tuple<bool, string>(true, reason);
            }
            catch (ArgumentException)
            {
                reason = "Invalid API Key.";
                return new Tuple<bool, string>(false, reason);
            }
            catch (FormatException)
            {
                reason = "Invalid format API key.";
                return new Tuple<bool, string>(false, reason);
            }

        }

        /// <summary>
        /// Add a log item for a request/response action to
        /// the api
        /// </summary>
        /// <param name="item"><see cref="EndpointLogItem"/></param>
        public void AddLog(EndpointLogItem item)
        {
            apiLogRecords.AddLog(item);
        }

        /// <summary>
        /// Creates a signature from a base string
        /// </summary>
        /// <param name="apikey">Users private key</param>
        /// <param name="toSign">String to sign</param>
        /// <returns>String signature</returns>
        public async Task<string> CreateSignature(string toSign, string apikey)
        {
            try
            {
                if (string.IsNullOrEmpty(apikey))
                {
                    return string.Empty; //empty api key    
                }

                var guid = GuidShortener.Decode(apikey);
                var user = apiUserRecords.Get(guid);

                if (user == null)
                {
                    return await Task.FromResult(string.Empty);
                }

                return await Task.FromResult(SignString(user.PrivateKey, toSign));
            }
            catch (ArgumentException) //Invalid API key
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Creates a signed string using Hmac SHA256.
        /// </summary>
        /// <param name="privateKey">User private key</param>
        /// <param name="stringToSign">String to sign</param>
        /// <returns>Signature</returns>
        private static string SignString(string privateKey, string stringToSign)
        {
            byte[] key = Encoding.UTF8.GetBytes(privateKey);
            byte[] message = Encoding.UTF8.GetBytes(stringToSign);

            using (var hmac = new HMACSHA256(key))
            {
                byte[] hash = hmac.ComputeHash(message);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
