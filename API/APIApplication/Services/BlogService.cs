using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API.Domain.Services;
using API.Domain.Repository;
using API.Domain.Blog;
using API.Utilities.Extensions;
using APIApplication.Exceptions;
using APIApplication.Common;
using API.Domain;
using System.Threading.Tasks;

namespace APIApplication.Service
{
    public class BlogService : BaseService, IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository repository)
        {
            _blogRepository = repository;
        }

        /// <summary>
        /// Gets a Blog via id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Blog> GetBlog(Guid id)
        {
            var blog = await _blogRepository.Get(id);
            return blog;
        }

        /// <summary>
        /// Gets a blog via a complete object
        /// </summary>
        /// <param name="blog">The blog object to get</param>
        /// <returns>A blog</returns>
        public async Task<Blog> GetBlog(Blog blog)
        {
            var getblog = await _blogRepository.Get(blog);
            return getblog;
        }

        /// <summary>
        /// Saves a blog to the database
        /// </summary>
        /// <param name="blog">A blog to save</param>
        public async Task<int> AddBlog(Blog blog)
        {
            return await _blogRepository.AddBlog(blog);
        }

        /// <summary>
        /// Deletes a blog via the unique guid
        /// </summary>
        /// <param name="id">The guid of the blog</param>
        public async Task<int> DeleteBlog(Guid id)
        {
            return await _blogRepository.Delete(id);
        }

        /// <summary>
        /// Gets all the blogs in the table
        /// </summary>
        /// <returns>A list of blogs</returns>
        public async Task<IQueryable<Blog>> GetAll()
        {
            var blogs = await _blogRepository.GetAllAync();
            return blogs;
        }

        /// <summary>
        /// Updates a blog
        /// </summary>
        /// <param name="blog">The blog to update</param>
        public async Task<int> UpdateBlog(Blog blog)
        {
            return await _blogRepository.UpdateAsync(blog);
        }
    }
}
