using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Utilities;

namespace API.Domain.Infrastructure
{
    /// <summary>
    /// Represents an api user in the central UV_APIs database
    /// </summary>
    public class ApiUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public DateTime SignupDate { get; set; }

        /// <summary>
        /// Comma separated list of API's the user has access to
        /// </summary>
        public string AuthorizedAPIs { get; set; }

        /// <summary>
        /// Checks if the user has access to a given API
        /// </summary>
        /// <param name="apiName"></param>
        /// <returns></returns>
        public bool HasAccess(string apiName)
        {
            return AuthorizedAPIs.Split(',')
                .Contains(apiName);    
        }

        /// <summary>
        /// Gets the API key from the users Unique Id (GUID)
        /// </summary>
        /// <returns></returns>
        public string GetApiKey()
        {
            return GuidShortener.Encode(Id);
        }
    }
}
