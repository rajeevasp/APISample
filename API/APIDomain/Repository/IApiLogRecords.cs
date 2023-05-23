using System.Threading.Tasks;
using API.Domain.Infrastructure;

namespace API.Domain.Repository
{
    public interface IApiLogRecords 
    {
        /// <summary>
        /// Add a endpoint log to the logging database representing
        /// a request and a response from the API endpoint.
        /// </summary>
        /// <param name="endpointLog"><see cref="EndpointLogItem"/></param>
        void AddLog(EndpointLogItem endpointLogItem);
    }
}
