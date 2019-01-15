using AutoMapping.Pattern1.Data.Entities;
using AutoMapping.Pattern1.Infrastructure;

namespace AutoMapping.Pattern1.Models
{
    public class CategoryDto : BaseDto<CategoryDto, Category>
    {
        public int Name { get; set; }
        public string Description { get; set; }
    }
}
