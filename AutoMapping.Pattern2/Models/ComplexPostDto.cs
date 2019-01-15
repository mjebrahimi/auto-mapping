using AutoMapper;
using AutoMapping.Pattern2.Data.Entities;
using AutoMapping.Pattern2.Infrastructure;

namespace AutoMapping.Pattern2.Models
{
    public class ComplexPostDto : BaseDto<ComplexPostDto, Post, long>
    {
        public string Title { get; set; }

        //Ignore property from any mapping (even from ComplexPostDto to Post or inverse) and could be set value manually
        [IgnoreMap]
        public string Text { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } //=> mapped from Category.Name
        public string FullTitle { get; set; } //=> custom mapping for "Title (Category.Name)"

        public CategoryDto Category { get; set; } //=> mapped from Post.Category

        public override void CustomMappings(IMappingExpression<Post, ComplexPostDto> mapping)
        {
            mapping.ForMember(
                    dest => dest.FullTitle,
                    config => config.MapFrom(src => $"{src.Title} ({src.Category.Name})"));
        }
    }
}
