using AutoMapper;
using AutoMapping.Pattern1.Data.Entities;
using AutoMapping.Pattern1.Infrastructure;

namespace AutoMapping.Pattern1.Models
{
    public class ComplexPostDto : BaseDto<ComplexPostDto, Post, long>
    {
        public string Title { get; set; }

        //Ignore property from any mapping (even from ComplexPostDto to Post or inverse) and could be set value manually
        [IgnoreMap]
        public string Text { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } //=> mapped from Category.Name

        public CategoryDto Category { get; set; } //=> mapped from Post.Category
    }
}
