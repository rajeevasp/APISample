using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Domain;

namespace API.Domain.Services
{
    public interface IBlogService
    {
        Task<Blog.Blog> GetBlog(Guid id);
        Task<IQueryable<Blog.Blog>> GetAll();
        Task<int> DeleteBlog(Guid id);
        Task<int> AddBlog(Blog.Blog Blog);
        Task<int> UpdateBlog(Blog.Blog Blog);
    }
}
