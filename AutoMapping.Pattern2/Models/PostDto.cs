using AutoMapper;
using AutoMapping.Pattern2.Data.Entities;
using AutoMapping.Pattern2.Infrastructure;

namespace AutoMapping.Pattern2.Models
{
public class PostDto : BaseDto<PostDto, Post, long>
{
    public string Title { get; set; }
    public string Text { get; set; }
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } //=> Category.Name
    public string FullTitle { get; set; } //=> custom mapping for "Title (Category.Name)"
        
    public override void CustomMappings(IMappingExpression<Post, PostDto> mapping)
    {
        mapping.ForMember(
                dest => dest.FullTitle,
                config => config.MapFrom(src => $"{src.Title} ({src.Category.Name})"));
    }
}
}
