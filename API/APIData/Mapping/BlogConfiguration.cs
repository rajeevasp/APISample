using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API.Domain.Blog;
using System.Data.Entity.ModelConfiguration;

namespace API.Data.Mapping
{
    /// <summary>
    /// Custom mapping configuration
    /// </summary>
    public class BlogConfiguration : EntityTypeConfiguration<Blog>
    {
        /// <summary>
        /// Set mapping values on initialization
        /// </summary>
        public BlogConfiguration()
        {
            ToTable("be_Blogs");
            HasKey(p => p.BlogId);
        }
    }
}
