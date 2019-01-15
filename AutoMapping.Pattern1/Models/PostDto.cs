using AutoMapping.Pattern1.Data.Entities;
using AutoMapping.Pattern1.Infrastructure;

namespace AutoMapping.Pattern1.Models
{
    public class PostDto : BaseDto<PostDto, Post, long>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } //=> Category.Name
    }
}
