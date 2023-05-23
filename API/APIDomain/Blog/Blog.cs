using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API.Domain.Common;

namespace API.Domain.Blog
{
    /// <summary>
    /// Represents a Blog
    /// </summary>
    public class Blog : IEntity
    {
        public Guid BlogId { get; set; }
        public string BlogName { get; set; }
        public string HostName { get; set; }
        public bool IsAnyTextBeforeHostnameAccepted { get; set; }
        public string StorageContainerName { get; set; }
        public string VirtualPath { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public bool IsSiteAggregation { get; set; }
    }
}
