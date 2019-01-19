using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapping.Pattern1.Data;
using AutoMapping.Pattern1.Data.Entities;
using AutoMapping.Pattern1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMapping.Pattern1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PostsController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        // GET api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> Get()
        {
            var list =
                //We use AsNotracking because this is a read only select
                //ProjectTo method select only needed properties (of PostDto) not all properties
                //Also select only needed property of navigations (like Post.Category.Name) not all unlike Include
                //This ability called "Projection"
                await _applicationDbContext.Posts.AsNoTracking().ProjectTo<PostDto>()
                //We can also use Where on IQuerable<PostDto>
                .Where(p => p.Title.Contains("test") || p.CategoryName.Contains("test"))
                .ToListAsync();

            return list;
        }

        // GET api/posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> Get(long id)
        {
            var postDto = await _applicationDbContext.Posts.AsNoTracking().ProjectTo<PostDto>()
                .SingleOrDefaultAsync(p => p.Id == id);

            ////Another example : without using ProjectTo, use BaseDto.FromEntity instead
            //var post = await _applicationDbContext.Posts.AsNoTracking().Include(p => p.Category)
            //    .SingleOrDefaultAsync(p => p.Id == id);
            ////Create a PostDto from Post with mapping properties
            //var postDto = PostDto.FromEntity(post);

            return postDto;
        }

        // GET api/posts/GetById/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<PostDto>> GetById(long id)
        {
            //Another example : without using ProjectTo, use BaseDto.FromEntity instead
            var post = await _applicationDbContext.Posts.AsNoTracking().Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.Id == id);

            //Create a PostDto from Post with mapping properties
            var postDto = PostDto.FromEntity(post);

            return postDto;
        }

        // POST api/posts
        [HttpPost]
        public async Task<ActionResult> Post(PostDto postDto)
        {
            //Create a new Post with mapped properties from PostDto
            var post = postDto.ToEntity();

            await _applicationDbContext.Posts.AddAsync(post);
            await _applicationDbContext.SaveChangesAsync();

            return Ok();
        }

        // PUT api/posts/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, PostDto postDto)
        {
            var post = await _applicationDbContext.Posts.FindAsync(id);


            //Change properties values of a finded Post by id, from PostDto
            var updatePost = postDto.ToEntity(post);

            _applicationDbContext.Posts.Update(updatePost);
            await _applicationDbContext.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var post = await _applicationDbContext.Posts.FindAsync(id);

            _applicationDbContext.Posts.Remove(post);
            await _applicationDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
