using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Infrastructure.Results
{
    /// <summary>
    /// Used when we cant find a record
    /// </summary>
    public class RecordNotFoundResult : IHttpActionResult
    {
        private HttpRequestMessage request;
        private string message;
        private Encoding encoding = Encoding.UTF8;
        private string mediaType;

        public RecordNotFoundResult(HttpRequestMessage request, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException("Empty message");
            }

            if (request == null)
            {
                throw new ArgumentNullException("Null request");
            }

            this.request = request;
            this.message = message;
            this.mediaType = "application/json"; //send back messages as json
        }

        /// <summary>
        /// Send the response
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = request.CreateResponse(HttpStatusCode.NotFound);

            if (string.IsNullOrEmpty(message))
            {
                response.Content = new StringContent("Could not find record/s", encoding, mediaType);
            } 
            else
            {
                response.Content = new StringContent(message, encoding, mediaType);
            }

            return await Task.FromResult(response);
        }

    }
}