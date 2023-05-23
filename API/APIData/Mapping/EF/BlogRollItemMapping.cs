using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalWeather.CMS.API.Domain;


namespace UniversalWeather.FSS.API.Data.Mapping.EF
{
    public class BlogRollItemMapping : EntityTypeConfiguration<BlogRollItem>
    {
       public BlogRollItemMapping()
       {
           ToTable("BlogRollItem");
           HasKey(k => k.BlogId);
           Property(p => p.BlogRollId).HasColumnName("BLOG_ROLL_ID");
           Property(p => p.Title).HasColumnName("TITLE");
           Property(p => p.Description).HasColumnName("DESCRIPTION");
           Property(p => p.FeedUrl).HasColumnName("FEED_URL");
           Property(p => p.Xfn).HasColumnName("XFN");
           Property(p => p.SortIndex).HasColumnName("SORT_INDEX");
       }
    }
}
