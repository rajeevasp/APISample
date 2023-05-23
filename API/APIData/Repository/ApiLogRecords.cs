using Dapper;
using System.Data;
using System.Threading.Tasks;
using API.Domain.Infrastructure;
using API.Domain.Repository;

namespace API.Data.Repository
{
    public class ApiLogRecords : IApiLogRecords
    {
        /// <summary>
        /// Add a endpoint log to the logging database representing
        /// a request and a response from the API endpoint.
        /// </summary>
        /// <param name="endpointLog"><see cref="EndpointLogItem"/></param>
        public void AddLog(EndpointLogItem endpointLogItem)
        {
            using (IDbConnection connection = Connection.GetConnection("UV_APIs"))
            {
                string sql = @"INSERT INTO [dbo].[Logs]
                                    ([Application]
                                    ,[RequestUri]
                                    ,[StatusCode]
                                    ,[HttpMethod]
                                    ,[HttpVersion]
                                    ,[Timestamp]
                                    ,[IpAddress]
                                    ,[MachineName]
                                    ,[ApiKey]
                                    ,[BytesSent]
                                    ,[BytesReceived]
                                    ,[Exception]
                                    ,[JsonRecord])
                               VALUES
                                    (@Application
                                    ,@RequestUri
                                    ,@StatusCode
                                    ,@HttpMethod
                                    ,@HttpVersion
                                    ,@Timestamp
                                    ,@IpAddress
                                    ,@MachineName
                                    ,@ApiKey
                                    ,@BytesSent
                                    ,@BytesReceived
                                    ,@Exception
                                    ,@JsonRecord)";
                connection.Execute(sql, endpointLogItem);
            }
        }
    }
}
