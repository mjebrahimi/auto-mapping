using AutoMapping.Pattern2.Data.Entities;
using AutoMapping.Pattern2.Infrastructure;

namespace AutoMapping.Pattern2.Models
{
    public class CategoryDto : BaseDto<CategoryDto, Category>
    {
        public int Name { get; set; }
        public string Description { get; set; }
    }
}
