using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Utilities
{
    /// <summary>
    /// Shortens guids
    /// </summary>
    public static class GuidShortener
    {
        private static readonly int keyLength = 22;

        /// <summary>
        /// Encodes a guid string 
        /// </summary>
        /// <param name="guidValue">String guid representation</param>
        /// <returns>string guid</returns>
        public static string Encode(string guidValue)
        {
            Guid guid = new Guid(guidValue);
            return Encode(guid);
        }

        /// <summary>
        /// Encodes a guid object and returns the string equivalent of a length
        /// </summary>
        /// <param name="guid"><see cref="Guid"/></param>
        /// <returns>Shortened guid string</returns>
        public static string Encode(Guid guid)
        {
            string encodedValue = Convert.ToBase64String(guid.ToByteArray());
            encodedValue = encodedValue.Replace("/", "_").Replace("+", "-");
            return encodedValue.Substring(0, keyLength);
        }

        /// <summary>
        /// Decodes a guid from the shortened encoded value. The input parameter
        /// is case sensitive
        /// </summary>
        /// <param name="encodedValue">Shortened guid string</param>
        /// <returns><see cref="Guid"/></returns>
        public static Guid Decode(string encodedValue) 
        {
            encodedValue = encodedValue.Replace("_", "/").Replace("-", "+");
            byte[] buffer = Convert.FromBase64String(encodedValue + "==");
            return new Guid(buffer);
        }
    }
}
