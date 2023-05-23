using API.Data.Repository;
using API.Domain.Blog;
using API.Domain.Repository;

using API.Data.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace API.Data.Repository
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        private IDbSet<Blog> _dbBlogSet;

        public BlogRepository(DomainNameDbContext dataContexts)
            : base(dataContexts)
        {
            _dbBlogSet = dataContexts.Blogs;
        }

        public async Task<int> AddBlog(Blog blog)
        { 
            var getBlog=await _dbContext.Blogs.Where(x => x.BlogId == blog.BlogId).FirstOrDefaultAsync();
            if (getBlog != null)
                return 0;

            return await AddAsync(blog);
            
        }
    }
}
