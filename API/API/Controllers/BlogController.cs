using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using API.Domain.Blog;
using API.Domain.Services;
using API.Infrastructure.Extensions;
using API.Infrastructure.Attributes;
using API.Infrastructure.Handlers;
using API.Infrastructure;

namespace API.Controllers
{
    /// <summary>
    /// This controller exposes endpoints to access the blogs in the database
    /// </summary>
    public class BlogController : ApiController
    {
        private readonly IBlogService _blogService;
        private string newLocation = string.Empty;
        /// <summary>
        /// Set the blog service
        /// </summary>
        /// <param name="service">The blog interface</param>
        public BlogController(IBlogService service)
        {
            _blogService = service;
        }

        /// <summary>
        /// This gets the Blog associated with the Guid parameters
        /// </summary>
        /// <param name="id">The guid representing the Blog</param>
        /// <returns>A blog to retrieve</returns>
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var blog = await _blogService.GetBlog(id);

            if (blog == null)
                return this.RecordNotFound(string.Format("Blog not found for BlogId: {0}", id));

            return Ok(blog);
        }

        /// <summary>
        /// This gets all the Blogs in a list. This call supports OData conventions.
        /// </summary>
        /// <returns>A list of blogs</returns>
        [Queryable]
        public async Task<IHttpActionResult> GetAll()
        {
            var blogs = await _blogService.GetAll();

            if (blogs == null || blogs.Count() == 0)
                return this.RecordNotFound(string.Format("Blogs not found."));

            return Ok(blogs);
        }

        /// <summary>
        /// This saves a Blog into the database. The content-type request body supports json and xml.
        /// </summary>
        /// <param name="blog">The blog to save</param>
        [HttpPost]
        public async Task<IHttpActionResult> Save([FromBody]Blog blog)
        {
            //Blog with same BlogID(Guid) exists
            if (await _blogService.AddBlog(blog) == 0)
                return Ok("Already Exist: Blog Id=" + blog.BlogId);

            newLocation = Url.Link("DefaultApi", new { controller = "blog", action = "get", id = blog.BlogId });

            return Created(newLocation.ToString(), string.Format("{0} created.", blog.BlogName));
        }

        /// <summary>
        /// This deletes a Blog by its existing Guid  
        /// </summary>
        /// <param name="id">The guid of the blog</param>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id = new Guid())
        {
            if (await _blogService.DeleteBlog(id) == 0)
                return this.RecordNotFound(string.Format("Blog not found for id: {0}", id));

            return Ok("Record deleted successfully.");
        }

        /// <summary>
        /// This updates a blog from a complete Blog object. The content-type request body supports json and xml.
        /// </summary>
        /// <param name="blog">The blog to update</param>
        [HttpPut]
        public async Task<IHttpActionResult> Update([FromBody]Blog blog)
        {
            if (await _blogService.UpdateBlog(blog) == 0)
                return this.RecordNotFound(string.Format("Blog not found for id: {0}", blog.BlogId));

            return Ok("Record updated successfully.");
        }
    }
}
