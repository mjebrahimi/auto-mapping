using AutoMapper;
using AutoMapping.Pattern2.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace AutoMapping.Pattern2.Infrastructure
{
    public abstract class BaseDto<TDto, TEntity, TKey> : IHaveCustomMapping
         where TDto : class, new()
         where TEntity : BaseEntity<TKey>, new()
    {
        [Display(Name = "ردیف")]
        public TKey Id { get; set; }

        /// <summary>
        /// Maps this dto to a new entity object.
        /// </summary>
        public TEntity ToEntity()
        {
            return Mapper.Map<TEntity>(CastToDerivedClass(this));
        }

        /// <summary>
        /// Maps this dto to an exist entity object.
        /// </summary>
        public TEntity ToEntity(TEntity entity)
        {
            return Mapper.Map(CastToDerivedClass(this), entity);
        }

        /// <summary>
        /// Maps the specified entity to a new dto object.
        /// </summary>
        public static TDto FromEntity(TEntity model)
        {
            return Mapper.Map<TDto>(model);
        }

        protected TDto CastToDerivedClass(BaseDto<TDto, TEntity, TKey> baseInstance)
        {
            return Mapper.Map<TDto>(baseInstance);
        }

        //Get automapper Profile then create mapping and ignore unmapped properties
        public void CreateMappings(Profile profile)
        {
            var mappingExpression = profile.CreateMap<TDto, TEntity>();

            var dtoType = typeof(TDto);
            var entityType = typeof(TEntity);

            //Ignore mapping to any property of source (like Post.Categroy) that dose not contains in destination (like PostDto)
            //To prevent from wrong mapping. for example in mapping of "PostDto -> Post", automapper create a new instance for Category (with null catgeoryName) because we have CategoryName property that has null value
            foreach (var property in entityType.GetProperties())
            {
                if (dtoType.GetProperty(property.Name) == null)
                    mappingExpression.ForMember(property.Name, opt => opt.Ignore());
            }

            //Pass mapping expressin to customize mapping in concrete class
            CustomMappings(mappingExpression.ReverseMap());
        }

        //Concrete class can override this method to customize mapping
        public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
        {
        }
    }

    public abstract class BaseDto<TDto, TEntity> : BaseDto<TDto, TEntity, int>
        where TDto : class, new()
        where TEntity : BaseEntity<int>, new()
    {

    }
}
