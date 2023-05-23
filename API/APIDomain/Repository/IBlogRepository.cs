using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain.Blog;

namespace API.Domain.Repository
{
    public interface IBlogRepository : IRepository<Blog.Blog>
    {
        Task<int> AddBlog(Blog.Blog blog);
    }
}
