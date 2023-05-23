using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalWeather.CMS.API.Domain;

namespace UniversalWeatherCMS.API.Data.Mapping.EF
{
    public class BlogMapping : EntityTypeConfiguration<Blog>
    {
       public BlogMapping()
        {
            ToTable("Blog");
            HasKey(k => k.BlogId);
            Property(p => p.BlogName).HasColumnName("BLOG_NAME");
            Property(p => p.HostName).HasColumnName("HOST_NAME");
            Property(p => p.IsAnyTextBeforeHostnameAccepted).HasColumnName("IS_ANY_TEXT_BEFORE_HOST_NAME_ACCEPTED");
            Property(p => p.StorageContainerName).HasColumnName("STORAGE_CONTAINER_NAME");
            Property(p => p.VirtualPath).HasColumnName("VIRTUAL_PATH");
            Property(p => p.IsPrimary).HasColumnName("IS_PRIMARY");
            Property(p => p.IsActive).HasColumnName("IS_ACTIVE");
            Property(p => p.IsSiteAggregation).HasColumnName("IS_SITE_AGGREGATION");
        }
    }
}
