using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API.Domain.Infrastructure
{
    public class EndpointLogItem
    {
        public int LogId { get; set; }
        public string Application { get; set; }
        public long ResponseTime { get; set; }
        public int StatusCode { get; set; }
        public string HttpMethod { get; set; }
        public string HttpVersion { get; set; }
        public string RequestUri { get; set; }
        public DateTime Timestamp { get; set; }
        public string IpAddress { get; set; }
        public string MachineName { get; set; }
        public string ApiKey { get; set; }
        public long BytesReceived { get; set; }
        public long BytesSent { get; set; }
        public string Exception { get; set; }
        public string JsonRecord { get; set; }

        public JObject FillJsonRecord()
        {
            return new JObject(
                new JProperty("Application", Application),
                new JProperty("ResponseTime", ResponseTime),
                new JProperty("StatusCode", StatusCode),
                new JProperty("HttpMethod", HttpMethod),
                new JProperty("HttpVersion", HttpVersion),
                new JProperty("RequestUri", RequestUri),
                new JProperty("Timestamp", Timestamp),
                new JProperty("IpAddress", IpAddress),
                new JProperty("MachineName", MachineName),
                new JProperty("ApiKey", ApiKey),
                new JProperty("BytesReceived", BytesReceived),
                new JProperty("BytesSent", BytesSent),
                new JProperty("Exception", Exception)
           );
        }
    }
}
