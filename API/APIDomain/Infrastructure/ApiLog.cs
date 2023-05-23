using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Infrastructure
{
    public class ApiLog
    {
        public int LogId { get; set; }
        public long ResponseTime { get; set; } //In Milliseonds
        public int StatusCode { get; set; }
        public string HttpMethod { get; set; }
        public string HttpVersion { get; set; }
        public string RequestUri { get; set; }
        public DateTime Timestamp { get; set; }
        public string IpAddress { get; set; }
        public string MachineName { get; set; }
    }
}
